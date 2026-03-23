# Fifth SDK & Compiler: Multi-Project Build Fixes

## Context

When building a multi-project Fifth solution (e.g., `samples/FullProjectExample/`), two systems fail to cooperate: the SDK's MSBuild plumbing doesn't pass reference paths to the compiler, and the compiler doesn't resolve symbols from referenced assemblies even when it receives them. The result is that a simple `.5thproj` with `<ProjectReference>` requires obscure MSBuild target overrides to even partially work, and cross-project function calls fail at compile time regardless.

This document describes the exact changes needed to make a project like this compile out of the box:

```
App (Exe) → MathLib (Library) → CoreLib (Library)
App (Exe) → CoreLib (Library)
```

Where `main.5th` calls `square()` from CoreLib and `add()` from MathLib with no special MSBuild boilerplate.

---

## Problem 1: SDK doesn't populate `ReferencePath` after building dependencies

### Current behavior

The `ResolveFifthProjectReferences` target in `src/Fifth.Sdk/Sdk/Sdk.targets` builds all `ProjectReference` dependencies but never queries their output paths:

```xml
<Target Name="ResolveFifthProjectReferences" Condition="'@(ProjectReference)' != ''">
  <ItemGroup>
    <_FifthMissingProjectReference Include="@(ProjectReference)" Condition="!Exists('%(Identity)')" />
  </ItemGroup>

  <Error Condition="'@(_FifthMissingProjectReference)' != ''"
         Text="Missing project references: @(_FifthMissingProjectReference, ', ')" />

  <MSBuild Projects="@(ProjectReference)"
           Targets="Build"
           Properties="Configuration=$(Configuration);TargetFramework=$(TargetFramework)"
           BuildInParallel="$(BuildInParallel)" />
</Target>
```

The `FifthCompile` target later constructs `--reference` args from `@(ReferencePath)`:

```xml
<ItemGroup>
  <_FifthReference Include="@(ReferencePath)" Condition="'@(ReferencePath)' != ''" />
</ItemGroup>
<PropertyGroup>
  <_FifthReferenceArgs>@(_FifthReference->'--reference &quot;%(FullPath)&quot;', ' ')</_FifthReferenceArgs>
</PropertyGroup>
```

But `@(ReferencePath)` is always empty because nothing populated it.

### What the workaround looks like today

Every `.5thproj` that can be referenced needs a custom `GetTargetPath` target:

```xml
<Target Name="GetTargetPath" Returns="$(MSBuildProjectDirectory)\$(FifthOutputPath)" />
```

And every `.5thproj` that references other projects needs a `PopulateFifthReferencePaths` target:

```xml
<Target Name="PopulateFifthReferencePaths" AfterTargets="ResolveFifthProjectReferences"
        Condition="'@(ProjectReference)' != ''">
  <MSBuild Projects="@(ProjectReference)" Targets="GetTargetPath"
           Properties="Configuration=$(Configuration);TargetFramework=$(TargetFramework)">
    <Output TaskParameter="TargetOutputs" ItemName="ReferencePath" />
  </MSBuild>
</Target>
```

Plus `DefaultTargets="Build"` on every `<Project>` element to prevent `GetTargetPath` from becoming the default target. This is unacceptable boilerplate for end users.

### Required SDK fix

Two changes to `src/Fifth.Sdk/Sdk/Sdk.targets`:

**1. Add a `GetTargetPath` target that returns the absolute output path:**

```xml
<Target Name="GetTargetPath" Returns="$(MSBuildProjectDirectory)\$(FifthOutputPath)" />
```

This must be in `Sdk.targets` so every Fifth project automatically exposes its output path when queried. No per-project override needed.

**2. Update `ResolveFifthProjectReferences` to populate `ReferencePath`:**

```xml
<Target Name="ResolveFifthProjectReferences" Condition="'@(ProjectReference)' != ''">
  <ItemGroup>
    <_FifthMissingProjectReference Include="@(ProjectReference)" Condition="!Exists('%(Identity)')" />
  </ItemGroup>

  <Error Condition="'@(_FifthMissingProjectReference)' != ''"
         Text="Missing project references: @(_FifthMissingProjectReference, ', ')" />

  <!-- Build dependencies first -->
  <MSBuild Projects="@(ProjectReference)"
           Targets="Build"
           Properties="Configuration=$(Configuration);TargetFramework=$(TargetFramework)"
           BuildInParallel="$(BuildInParallel)" />

  <!-- Query their output paths and populate ReferencePath -->
  <MSBuild Projects="@(ProjectReference)"
           Targets="GetTargetPath"
           Properties="Configuration=$(Configuration);TargetFramework=$(TargetFramework)">
    <Output TaskParameter="TargetOutputs" ItemName="ReferencePath" />
  </MSBuild>
</Target>
```

### Result after fix

A library project becomes:

```xml
<Project Sdk="Fifth.Sdk">
  <PropertyGroup>
    <OutputType>Library</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <FifthCompilerCommand>fifthc</FifthCompilerCommand>
  </PropertyGroup>
</Project>
```

A referencing project becomes:

```xml
<Project Sdk="Fifth.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <FifthCompilerCommand>fifthc</FifthCompilerCommand>
  </PropertyGroup>
  <ItemGroup>
    <ProjectReference Include="..\MathLib\MathLib.5thproj" />
  </ItemGroup>
</Project>
```

No custom targets. No `DefaultTargets`. No `GetTargetPath` override. No `PopulateFifthReferencePaths`.

---

## Problem 2: Compiler doesn't resolve symbols from referenced assemblies

### Current behavior

Even when `--reference` DLL paths are correctly passed to the compiler, the generated C# emits bare function names without qualifying them. Given this Fifth source:

```
main(): void {
    sq: int = square(7);
    sum: int = add(sq, 3);
}
```

The compiler generates:

```csharp
public static partial class Program
{
    public static void Main()
    {
        object __discard = default(object);
        int sq = square(7);     // CS0103: 'square' does not exist in the current context
        int sum = add(sq, 3);   // CS0103: 'add' does not exist in the current context
    }
}
```

Roslyn compilation fails because `square` and `add` are defined in the referenced assemblies (CoreLib.dll, MathLib.dll) inside their own `Program` classes, but the generated C# has no `using static` directives to bring them into scope.

### How the compiler already handles built-in types

The code generator already emits `using static` for built-in Fifth.System types:

```csharp
using static Fifth.System.Functional;
using static Fifth.System.List;
using static Fifth.System.IO;
using static Fifth.System.Math;
```

The same pattern needs to extend to user-provided `--reference` assemblies.

### Required compiler fix

In the Roslyn backend code generation phase:

1. **Inspect referenced assemblies**: When `--reference` paths are provided, use Roslyn metadata or reflection to discover public static types in each referenced assembly and their public static methods.

2. **Emit `using static` directives**: For each referenced assembly's public static class that contains methods, emit a `using static` directive. For example, if CoreLib.dll contains `public static class Program` with `square()`, emit:

   ```csharp
   using static CoreLib.Program;
   ```

3. **Alternative approach**: Instead of `using static`, the compiler could fully qualify cross-assembly calls during code generation (e.g., `CoreLib.Program.square(7)`). However, `using static` is simpler and consistent with the existing pattern for built-in types.

### Key code path

The modification point is in the Roslyn backend — specifically:
- Where `using` directives are emitted (add directives for referenced assembly types)
- The compiler already receives `--reference` args and passes them to Roslyn for compilation; it just doesn't use them during C# source generation

### Result after fix

The compiler generates:

```csharp
using static Fifth.System.Functional;
using static Fifth.System.List;
using static Fifth.System.IO;
using static Fifth.System.Math;
using static CoreLib.Program;    // NEW: from --reference CoreLib.dll
using static MathLib.Program;    // NEW: from --reference MathLib.dll

public static partial class Program
{
    public static void Main()
    {
        int sq = square(7);     // resolves via CoreLib.Program
        int sum = add(sq, 3);   // resolves via MathLib.Program
    }
}
```

Roslyn compilation succeeds.

---

## Problem 3: `GetTargetPath` must return an absolute path

### Current behavior

`FifthOutputPath` defaults to `$(OutputPath)$(TargetFileName)` which evaluates to a relative path like `bin\Debug\net10.0\CoreLib.dll`. When a referencing project queries `GetTargetPath`, this relative path is interpreted relative to the *referencing* project's directory, not the *referenced* project's directory. The path doesn't resolve.

### Required fix

The `GetTargetPath` target in `Sdk.targets` must return an absolute path by prepending `$(MSBuildProjectDirectory)`:

```xml
<Target Name="GetTargetPath" Returns="$(MSBuildProjectDirectory)\$(FifthOutputPath)" />
```

This is already shown in the Problem 1 fix above. The key point is that `$(MSBuildProjectDirectory)` evaluates in the context of the *referenced* project, producing the correct absolute path.

---

## Summary of all changes

| Component | File | Change |
|-----------|------|--------|
| SDK | `src/Fifth.Sdk/Sdk/Sdk.targets` | Add `GetTargetPath` target returning absolute output path |
| SDK | `src/Fifth.Sdk/Sdk/Sdk.targets` | Update `ResolveFifthProjectReferences` to call `GetTargetPath` on dependencies and populate `ReferencePath` |
| Compiler | Roslyn backend (code generation) | Inspect `--reference` assemblies for public static types |
| Compiler | Roslyn backend (code generation) | Emit `using static` directives for referenced assembly types |

### What this enables

After both fixes, the `samples/FullProjectExample/` project files become minimal:
- No `DefaultTargets="Build"` needed
- No `GetTargetPath` target override in any `.5thproj`
- No `PopulateFifthReferencePaths` target in any `.5thproj`
- Cross-project function calls (`square()`, `add()`) compile and link correctly
- `dotnet build src/App/App.5thproj` just works

### Verification

Build the FullProjectExample with the clean `.5thproj` files (no workarounds) and confirm:
1. `dotnet build src/App/App.5thproj` succeeds — dependencies build in order, `--reference` args are passed, Roslyn compilation succeeds
2. `dotnet build FullProjectExample.slnx` succeeds — solution-level build works
3. `dotnet run --project src/App/App.5thproj` executes — `square(7)` returns 49, `add(49, 3)` returns 52
