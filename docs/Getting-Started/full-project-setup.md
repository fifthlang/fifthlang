# Full Project Setup

This guide walks through setting up a multi-project Fifth solution using the SLNX solution format, `ProjectReference` wiring, and the Fifth.Sdk from NuGet.

The completed sample lives at `samples/FullProjectExample/` in the repository.

## SLNX Solution Format

SLNX (`.slnx`) is the XML-based solution format introduced in .NET 9 and Visual Studio 17.10+. It replaces the legacy `.sln` text format with clean, human-readable XML:

```xml
<Solution>
  <Project Path="src/App/App.5thproj" Type="{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}" />
  <Project Path="src/MathLib/MathLib.5thproj" Type="{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}" />
  <Project Path="src/CoreLib/CoreLib.5thproj" Type="{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}" />
</Solution>
```

SLNX files are diff-friendly and easier to edit by hand than the older format. Visual Studio 2026 (17.10+) is required to open `.slnx` files.

Because `.5thproj` is not a standard project extension, each `<Project>` element needs an explicit `Type` attribute with the C# project type GUID. This tells the SLNX parser how to handle the project files. Standard extensions like `.csproj` don't need this — the parser infers the type automatically.

## Prerequisites

Before building, you need two things: the Fifth.Sdk and the Fifth compiler tool. Both are version **0.7.1** — the SDK and compiler versions must match.

**Fifth.Sdk** resolves automatically from NuGet when you build. The `global.json` file in the project root pins the version:

```json
{
  "msbuild-sdks": {
    "Fifth.Sdk": "0.7.1"
  }
}
```

**Fifth compiler tool** is installed as a global .NET tool:

```bash
dotnet tool install --global Fifth.Compiler.Tool --version 0.7.1
```

Or via a local tool manifest. Create `.config/dotnet-tools.json` and run `dotnet tool restore`:

```json
{
  "version": 1,
  "isRoot": true,
  "tools": {
    "fifth.compiler.tool": {
      "version": "0.7.1",
      "commands": ["fifthc"]
    }
  }
}
```

## Step-by-Step Setup

### 1. Create the Directory Structure

```
FullProjectExample/
├── .config/
│   └── dotnet-tools.json
├── src/
│   ├── App/
│   │   ├── App.5thproj
│   │   └── main.5th
│   ├── MathLib/
│   │   ├── MathLib.5thproj
│   │   └── math.5th
│   └── CoreLib/
│       ├── CoreLib.5thproj
│       └── core.5th
├── FullProjectExample.slnx
├── global.json
└── .gitignore
```

### 2. Create `global.json`

Pin the Fifth.Sdk version so MSBuild resolves it from NuGet:

```json
{
  "msbuild-sdks": {
    "Fifth.Sdk": "0.7.1"
  }
}
```

### 3. Create the Tool Manifest

Create `.config/dotnet-tools.json` to pin the Fifth compiler:

```json
{
  "version": 1,
  "isRoot": true,
  "tools": {
    "fifth.compiler.tool": {
      "version": "0.7.1",
      "commands": ["fifthc"]
    }
  }
}
```

### 4. Create the CoreLib Class Library

`src/CoreLib/CoreLib.5thproj` — a leaf library with no dependencies:

```xml
<Project Sdk="Fifth.Sdk">
  <PropertyGroup>
    <OutputType>Library</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <FifthCompilerCommand>fifthc</FifthCompilerCommand>
  </PropertyGroup>
</Project>
```

`src/CoreLib/core.5th`:

```
square(x: int): int {
    return x * x;
}
```

### 5. Create the MathLib Class Library

`src/MathLib/MathLib.5thproj` — depends on CoreLib via `ProjectReference`:

```xml
<Project Sdk="Fifth.Sdk">
  <PropertyGroup>
    <OutputType>Library</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <FifthCompilerCommand>fifthc</FifthCompilerCommand>
  </PropertyGroup>
  <ItemGroup>
    <ProjectReference Include="..\CoreLib\CoreLib.5thproj" />
  </ItemGroup>
</Project>
```

The `ProjectReference` tells MSBuild to build CoreLib first and makes its types available to MathLib. This also demonstrates transitive dependency resolution through `ResolveFifthProjectReferences`.

`src/MathLib/math.5th`:

```
add(a: int, b: int): int {
    return a + b;
}
```

### 6. Create the App Console Application

`src/App/App.5thproj` — the executable entry point, referencing both libraries:

```xml
<Project Sdk="Fifth.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <FifthCompilerCommand>fifthc</FifthCompilerCommand>
  </PropertyGroup>
  <ItemGroup>
    <ProjectReference Include="..\MathLib\MathLib.5thproj" />
    <ProjectReference Include="..\CoreLib\CoreLib.5thproj" />
  </ItemGroup>
</Project>
```

`src/App/main.5th` — calls functions from both libraries:

```
main(): void {
    sq: int = square(7);
    sum: int = add(sq, 3);
}
```

The `square` function comes from CoreLib and `add` comes from MathLib. The `ProjectReference` elements ensure both libraries are built first and their assemblies are passed to the compiler via `--reference`.

> **Note:** The Fifth compiler v0.7.1 does not yet resolve symbols from referenced assemblies at compile time. The source above shows the intended usage pattern. The MSBuild dependency chain and `--reference` plumbing work correctly — once cross-assembly symbol resolution is added to the compiler, these calls will compile and link without changes.

### 7. Create the SLNX Solution File

`FullProjectExample.slnx` — lists all three projects. The `Type` attribute is required because `.5thproj` is a custom extension that the SLNX parser cannot infer automatically:

```xml
<Solution>
  <Project Path="src/App/App.5thproj" Type="{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}" />
  <Project Path="src/MathLib/MathLib.5thproj" Type="{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}" />
  <Project Path="src/CoreLib/CoreLib.5thproj" Type="{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}" />
</Solution>
```

### 8. Create `.gitignore`

Exclude build outputs and IDE artifacts:

```
bin/
obj/
.vs/
*.user
```

## Build and Run

Restore the compiler tool first, then build. Building the App project automatically builds its dependencies (CoreLib and MathLib) in the correct order:

```bash
dotnet tool restore
dotnet build src/App/App.5thproj
```

You can also build the entire solution. Use `/m:1` to ensure correct build ordering for Fifth projects:

```bash
dotnet build FullProjectExample.slnx /m:1
```

Run the console application:

```bash
dotnet run --project src/App/App.5thproj
```

## Known Limitations

The Fifth compiler v0.7.1 has a known limitation: it does not resolve symbols from referenced assemblies. The `--reference` arguments are passed correctly by the SDK, but the generated C# emits bare function names (e.g., `add(3, 4)`) without qualifying them with the referenced assembly's namespace or type.

This means:
- Building individual library projects (CoreLib, MathLib) works correctly
- The MSBuild dependency chain resolves and builds projects in the right order
- The `--reference` DLL paths are passed to the compiler
- But App cannot call functions defined in MathLib or CoreLib until the compiler adds cross-assembly symbol resolution

To verify the build pipeline works, build the libraries individually or build App with self-contained code only.

## Visual Studio Workflow

1. Open `FullProjectExample.slnx` in Visual Studio 2026 (17.10+).
2. Solution Explorer shows all three projects with their dependency relationships.
3. Right-click the `App` project and select **Set as Startup Project**.
4. Build the solution with **Build → Build Solution** (or `Ctrl+Shift+B`). MSBuild resolves `ProjectReference` dependencies and builds CoreLib and MathLib before App.
5. Press **F5** to run the App project.
