# Requirements Document

## Introduction

This feature fixes cross-project references in the Fifth language SDK and compiler so that multi-project solutions (e.g., `samples/FullProjectExample/`) compile out of the box without requiring MSBuild target overrides in individual `.5thproj` files. Two systems must be fixed: (1) the SDK's MSBuild plumbing in `Sdk.targets` must populate `ReferencePath` after building dependencies, and (2) the compiler's Roslyn backend must generate the necessary C# `using static` directives for public static types discovered in referenced assemblies, so that bare Fifth function calls like `square(7)` resolve correctly in the emitted C# code. Fifth itself has no `using static` syntax — this is purely an internal code generation concern. After both fixes, a project graph like `App (Exe) → MathLib (Library) → CoreLib (Library)` builds and runs with minimal `.5thproj` files and no workarounds.

## Glossary

- **Fifth_Sdk**: The MSBuild SDK (`Fifth.Sdk`) that provides build targets for `.5thproj` files, defined in `src/Fifth.Sdk/Sdk/Sdk.targets`.
- **Fifth_Compiler**: The Fifth language compiler (`fifthc`), whose Roslyn backend translates Fifth AST to C# and compiles it via Roslyn.
- **Roslyn_Backend**: The code generation phase in `src/compiler/LoweredToRoslyn/LoweredAstToRoslynTranslator.cs` that produces C# syntax trees from the lowered Fifth AST.
- **Sdk_Targets**: The MSBuild targets file at `src/Fifth.Sdk/Sdk/Sdk.targets` that orchestrates Fifth project builds.
- **ReferencePath**: An MSBuild item group populated with absolute paths to dependency assemblies; consumed by the `FifthCompile` target to construct `--reference` arguments for the compiler.
- **GetTargetPath**: An MSBuild target that returns the absolute output assembly path of a project, used by referencing projects to discover dependency outputs.
- **ResolveFifthProjectReferences**: An MSBuild target in Sdk_Targets that builds all `ProjectReference` dependencies before the current project compiles.
- **FifthOutputPath**: An MSBuild property holding the output assembly path for a Fifth project; defaults to a relative path.
- **Referenced_Assembly**: A `.dll` file produced by a dependency project and passed to the compiler via `--reference`.
- **Cross_Project_Function_Call**: A call in Fifth source code to a function defined in a different project (e.g., calling `square(7)` from CoreLib in App's `main.5th`). Fifth has no import or `using` syntax — cross-project resolution is handled entirely by the compiler's Roslyn backend during C# code generation.

## Requirements

### Requirement 1: GetTargetPath Target in SDK

**User Story:** As a Fifth developer with multi-project solutions, I want the SDK to provide a `GetTargetPath` target that returns the absolute output path of a project, so that referencing projects can discover dependency assembly locations without manual MSBuild overrides.

#### Acceptance Criteria

1. THE Sdk_Targets SHALL define a `GetTargetPath` target that returns the absolute output assembly path of the current project.
2. THE GetTargetPath target SHALL prepend `$(MSBuildProjectDirectory)` to `$(FifthOutputPath)` when FifthOutputPath is a relative path, so that the returned path is correct regardless of which project queries it.
3. WHEN a referencing project invokes `GetTargetPath` on a dependency, THE GetTargetPath target SHALL return a path that resolves to the dependency's output assembly, not a path relative to the referencing project's directory.

### Requirement 2: Automatic ReferencePath Population in SDK

**User Story:** As a Fifth developer, I want the SDK to automatically populate `ReferencePath` after building dependencies, so that the compiler receives `--reference` arguments without requiring a `PopulateFifthReferencePaths` target override in each `.5thproj` file.

#### Acceptance Criteria

1. THE ResolveFifthProjectReferences target SHALL invoke `GetTargetPath` on each `ProjectReference` dependency after building the dependencies.
2. THE ResolveFifthProjectReferences target SHALL populate the `ReferencePath` item group with the output paths returned by `GetTargetPath`.
3. WHEN a Fifth project has one or more ProjectReference dependencies, THE FifthCompile target SHALL receive non-empty `--reference` arguments corresponding to each dependency's output assembly.
4. WHEN a Fifth project has no ProjectReference dependencies, THE ResolveFifthProjectReferences target SHALL leave `ReferencePath` empty.

### Requirement 3: Compiler Discovers Public Static Types in Referenced Assemblies

**User Story:** As a Fifth developer, I want the compiler's Roslyn backend to inspect referenced assemblies for public static types, so that functions defined in dependency projects are available for cross-project calls without any Fifth-side import syntax.

#### Acceptance Criteria

1. WHEN `--reference` paths are provided to the Fifth_Compiler, THE Roslyn_Backend SHALL load metadata from each Referenced_Assembly.
2. THE Roslyn_Backend SHALL discover all public static types in each Referenced_Assembly.
3. THE Roslyn_Backend SHALL discover all public static methods within each discovered public static type.
4. IF a Referenced_Assembly path does not exist or cannot be loaded, THEN THE Fifth_Compiler SHALL emit a warning diagnostic and continue compilation.

### Requirement 4: Compiler Generates C# Using Static Directives for Referenced Types

**User Story:** As a Fifth developer, I want the compiler's Roslyn backend to automatically generate C# `using static` directives for public static types in referenced assemblies, so that cross-project function calls in my Fifth code (e.g., `square(7)`) resolve correctly in the emitted C# without any Fifth-side import syntax.

#### Acceptance Criteria

1. WHEN the Roslyn_Backend discovers public static types with public static methods in a Referenced_Assembly, THE Roslyn_Backend SHALL generate a C# `using static` directive for each such type in the emitted C# compilation unit.
2. THE Roslyn_Backend SHALL generate C# `using static` directives that use the fully qualified type name (namespace and class name).
3. THE Roslyn_Backend SHALL NOT generate duplicate C# `using static` directives for the same type.
4. WHEN no `--reference` paths are provided, THE Roslyn_Backend SHALL not generate any additional C# `using static` directives beyond the existing defaults (e.g., `Fifth.System.Functional`, `Fifth.System.IO`, etc.).

### Requirement 5: Clean .5thproj Files Without Workarounds

**User Story:** As a Fifth developer, I want my `.5thproj` files to contain only standard properties and ProjectReference elements, so that I do not need to add MSBuild target overrides to make cross-project references work.

#### Acceptance Criteria

1. WHEN the SDK fixes are applied, THE sample `.5thproj` files SHALL NOT require a `GetTargetPath` target override.
2. WHEN the SDK fixes are applied, THE sample `.5thproj` files SHALL NOT require a `PopulateFifthReferencePaths` target.
3. WHEN the SDK fixes are applied, THE sample `.5thproj` files SHALL NOT require `DefaultTargets="Build"` to function correctly.
4. Each `.5thproj` file SHALL contain only `<PropertyGroup>` (with OutputType, TargetFramework, FifthCompilerCommand) and `<ItemGroup>` (with ProjectReference elements) as needed.

### Requirement 6: End-to-End Multi-Project Build

**User Story:** As a Fifth developer, I want `dotnet build` on a multi-project Fifth solution to succeed without errors, so that I can develop multi-project solutions with the same ease as single-project builds.

#### Acceptance Criteria

1. WHEN `dotnet build src/App/App.5thproj` is executed in the FullProjectExample sample, THE build SHALL succeed without errors.
2. WHEN `dotnet build FullProjectExample.slnx` is executed, THE build SHALL succeed without errors for all projects in the solution.
3. WHEN `dotnet run --project src/App/App.5thproj` is executed, THE application SHALL execute and produce correct results from cross-project function calls: `square(7)` returns 49 and `add(49, 3)` returns 52, where `square` is defined in `core.5th` and `add` is defined in `math.5th`.

### Requirement 7: Transitive Dependency Resolution

**User Story:** As a Fifth developer, I want transitive project dependencies to resolve correctly, so that a project referencing a library that itself references another library can call functions from both.

#### Acceptance Criteria

1. WHEN App references MathLib and MathLib references CoreLib, THE build system SHALL build CoreLib before MathLib and MathLib before App.
2. WHEN App's `main.5th` calls `square(7)` (defined in CoreLib's `core.5th`) and `add(sq, 3)` (defined in MathLib's `math.5th`), THE Fifth_Compiler SHALL resolve both function calls in App's compilation without any import syntax in the Fifth source.
3. THE ReferencePath for App SHALL include the output assemblies of both MathLib and CoreLib.

### Requirement 8: Absolute Path Resolution for FifthOutputPath

**User Story:** As a Fifth developer, I want the SDK to always return absolute paths for project outputs, so that cross-project path resolution is correct regardless of directory structure.

#### Acceptance Criteria

1. THE GetTargetPath target SHALL return an absolute path by combining `$(MSBuildProjectDirectory)` with `$(FifthOutputPath)`.
2. WHEN FifthOutputPath is a relative path (e.g., `bin/Debug/net8.0/CoreLib.dll`), THE GetTargetPath target SHALL resolve the path relative to the project's own directory, not the caller's directory.
3. WHEN FifthOutputPath is already an absolute path, THE GetTargetPath target SHALL return the path unchanged.

### Requirement 9: Backward Compatibility

**User Story:** As a Fifth developer with existing single-project setups, I want the SDK and compiler changes to be backward compatible, so that projects without ProjectReference dependencies continue to build correctly.

#### Acceptance Criteria

1. WHEN a Fifth project has no ProjectReference elements, THE build SHALL succeed with the same behavior as before the changes.
2. WHEN no `--reference` arguments are passed to the Fifth_Compiler, THE Roslyn_Backend SHALL produce identical C# output to the current behavior (no additional `using static` directives in the generated C#).
3. THE changes SHALL NOT alter the generated C# code for projects that do not use cross-project references.
