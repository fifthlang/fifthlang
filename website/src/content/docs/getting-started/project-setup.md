---
title: "Project Setup"
description: "Set up a multi-project Fifth solution using the SDK and .NET tooling"
category: "getting-started"
order: 3
---

This guide walks through setting up a multi-project Fifth solution with `ProjectReference` wiring and the Fifth.Sdk from NuGet.

The completed sample lives at [`samples/FullProjectExample/`](https://github.com/aabs/fifthlang/tree/master/samples/FullProjectExample) in the repository.

## Prerequisites

You need the Fifth.Sdk and the Fifth compiler tool. Keep their versions in sync.

**Fifth.Sdk** resolves automatically from NuGet when you build. Pin the version in `global.json`:

```json
{
  "msbuild-sdks": {
    "Fifth.Sdk": "0.9.0"
  }
}
```

**Fifth compiler tool** — install globally or via a local tool manifest:

```bash
dotnet tool install --global Fifth.Compiler.Tool --version 0.9.0
```

Or create `.config/dotnet-tools.json` and run `dotnet tool restore`:

```json
{
  "version": 1,
  "isRoot": true,
  "tools": {
    "fifth.compiler.tool": {
      "version": "0.9.0",
      "commands": ["fifthc"]
    }
  }
}
```

## Directory Structure

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
├── FullProjectExample.sln
├── global.json
├── nuget.config
└── .gitignore
```

## Step-by-Step Setup

### 1. Create `global.json`

Pin the Fifth.Sdk version so MSBuild resolves it from NuGet:

```json
{
  "msbuild-sdks": {
    "Fifth.Sdk": "0.9.0"
  }
}
```

### 2. Create the Tool Manifest

Create `.config/dotnet-tools.json` to pin the Fifth compiler:

```json
{
  "version": 1,
  "isRoot": true,
  "tools": {
    "fifth.compiler.tool": {
      "version": "0.9.0",
      "commands": ["fifthc"]
    }
  }
}
```

### 3. Create the CoreLib Class Library

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

```fifth
namespace CoreLib;

square(x: int): int {
    return x * x;
}
```

### 4. Create the MathLib Class Library

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

The `ProjectReference` tells MSBuild to build CoreLib first and makes its types available to MathLib.

`src/MathLib/math.5th`:

```fifth
namespace MathLib;

add(a: int, b: int): int {
    return a + b;
}
```

### 5. Create the App Console Application

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

```fifth
import CoreLib;
import MathLib;

main(): int {
    sq: int;
    sq = square(7);
    result: int;
    result = add(sq, 3);
    std.print(string.Format("square(7) = {0}", sq));
    std.print(string.Format("add(49, 3) = {0}", result));
    return 0;
}
```

### 6. Create the Solution File

Fifth projects use the standard `.sln` format. The `.slnx` format is not compatible with custom MSBuild SDKs.

`FullProjectExample.sln`:

```
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "CoreLib", "src\CoreLib\CoreLib.5thproj", "{A1111111-1111-1111-1111-111111111111}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "MathLib", "src\MathLib\MathLib.5thproj", "{B2222222-2222-2222-2222-222222222222}"
	ProjectSection(ProjectDependencies) = postProject
		{A1111111-1111-1111-1111-111111111111} = {A1111111-1111-1111-1111-111111111111}
	EndProjectSection
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "App", "src\App\App.5thproj", "{C3333333-3333-3333-3333-333333333333}"
	ProjectSection(ProjectDependencies) = postProject
		{A1111111-1111-1111-1111-111111111111} = {A1111111-1111-1111-1111-111111111111}
		{B2222222-2222-2222-2222-222222222222} = {B2222222-2222-2222-2222-222222222222}
	EndProjectSection
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{A1111111-1111-1111-1111-111111111111}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{A1111111-1111-1111-1111-111111111111}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{A1111111-1111-1111-1111-111111111111}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{A1111111-1111-1111-1111-111111111111}.Release|Any CPU.Build.0 = Release|Any CPU
		{B2222222-2222-2222-2222-222222222222}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{B2222222-2222-2222-2222-222222222222}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{B2222222-2222-2222-2222-222222222222}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{B2222222-2222-2222-2222-222222222222}.Release|Any CPU.Build.0 = Release|Any CPU
		{C3333333-3333-3333-3333-333333333333}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{C3333333-3333-3333-3333-333333333333}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{C3333333-3333-3333-3333-333333333333}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{C3333333-3333-3333-3333-333333333333}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
EndGlobal
```

The `ProjectSection(ProjectDependencies)` blocks ensure MSBuild builds projects in the correct order. The GUIDs are arbitrary — generate your own or use the ones above as a starting point.

### 7. Create `.gitignore`

```
bin/
obj/
.vs/
*.user
```

## Build and Run

Restore the compiler tool first, then build:

```bash
dotnet tool restore
dotnet build src/App/App.5thproj
```

Build the entire solution:

```bash
dotnet build FullProjectExample.sln
```

Run the console application:

```bash
dotnet run --project src/App/App.5thproj
```

## Visual Studio Workflow

1. Open `FullProjectExample.sln` in Visual Studio.
2. Solution Explorer shows all three projects with their dependency relationships.
3. Right-click the `App` project and select **Set as Startup Project**.
4. Build with **Build → Build Solution** (or `Ctrl+Shift+B`).
5. Press **F5** to run.

## Next Steps

- [Learn Fifth in Y Minutes](/tutorials/learn-fifth-in-y-minutes) — A rapid tour of Fifth syntax and features
- [Installation Guide](/docs/getting-started/installation) — Install the Fifth compiler and SDK
