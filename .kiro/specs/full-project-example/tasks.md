# Implementation Plan: Full Project Example

## Overview

Create a sample MSBuild solution at `samples/FullProjectExample/` with three `.5thproj` projects (App, MathLib, CoreLib), configuration files, minimal `.5th` sources, and a Getting Started guide. All files are static — no runtime code or SDK changes needed.

## Tasks

- [x] 1. Create solution configuration files
  - [x] 1.1 Create `samples/FullProjectExample/global.json` with `msbuild-sdks` pinning `Fifth.Sdk` to `0.7.1`
    - Must contain only the `msbuild-sdks` section, no `sdk` section
    - _Requirements: 5.1, 5.2_
  - [x] 1.2 Create `samples/FullProjectExample/.config/dotnet-tools.json` tool manifest
    - Pin the Fifth compiler .NET tool (`fifthc`) to version `0.7.1`
    - _Requirements: 5.3_
  - [x] 1.3 Create `samples/FullProjectExample/.gitignore`
    - Exclude `bin/`, `obj/`, `.vs/`, `*.user`
    - _Requirements: 7.1, 7.2, 7.3_
  - [x] 1.4 Create `samples/FullProjectExample/FullProjectExample.slnx`
    - Use `<Solution>` root element with `<Project>` children referencing `src/App/App.5thproj`, `src/MathLib/MathLib.5thproj`, `src/CoreLib/CoreLib.5thproj`
    - _Requirements: 1.1, 1.2, 1.3_

- [x] 2. Create CoreLib class library project
  - [x] 2.1 Create `samples/FullProjectExample/src/CoreLib/CoreLib.5thproj`
    - Set `Sdk="Fifth.Sdk"`, `OutputType=Library`, `TargetFramework=net8.0`
    - Set `FifthCompilerCommand` to `fifthc`
    - No ProjectReference elements (leaf dependency)
    - _Requirements: 3.1, 3.2, 5.4, 9.3_
  - [x] 2.2 Create `samples/FullProjectExample/src/CoreLib/core.5th`
    - Define a minimal `square(x: int): int` function
    - _Requirements: 6.1, 6.3, 6.4, 9.5_
  - [x] 2.3 Write property test verifying CoreLib.5thproj declares required MSBuild properties
    - **Property 1: Project files declare required MSBuild properties**
    - **Validates: Requirements 3.2, 5.4**

- [x] 3. Create MathLib class library project
  - [x] 3.1 Create `samples/FullProjectExample/src/MathLib/MathLib.5thproj`
    - Set `Sdk="Fifth.Sdk"`, `OutputType=Library`, `TargetFramework=net8.0`
    - Set `FifthCompilerCommand` to `fifthc`
    - Add `<ProjectReference Include="..\CoreLib\CoreLib.5thproj" />` to demonstrate transitive dependency
    - _Requirements: 3.1, 3.2, 4.3, 5.4, 9.3_
  - [x] 3.2 Create `samples/FullProjectExample/src/MathLib/math.5th`
    - Define a minimal `add(a: int, b: int): int` function
    - _Requirements: 6.1, 6.3, 6.4, 9.5_
  - [x] 3.3 Write property test verifying MathLib.5thproj declares required MSBuild properties
    - **Property 1: Project files declare required MSBuild properties**
    - **Validates: Requirements 3.2, 5.4**

- [x] 4. Create App console application project
  - [x] 4.1 Create `samples/FullProjectExample/src/App/App.5thproj`
    - Set `Sdk="Fifth.Sdk"`, `OutputType=Exe`, `TargetFramework=net8.0`
    - Set `FifthCompilerCommand` to `fifthc`
    - Add `<ProjectReference>` elements for both `MathLib.5thproj` and `CoreLib.5thproj`
    - _Requirements: 2.1, 2.2, 2.3, 4.1, 5.4, 9.3_
  - [x] 4.2 Create `samples/FullProjectExample/src/App/main.5th`
    - Define a `main(): void` entry point that calls functions from MathLib and CoreLib
    - _Requirements: 2.4, 6.1, 6.2, 6.4, 9.5_
  - [x] 4.3 Write property test verifying App.5thproj declares required MSBuild properties
    - **Property 1: Project files declare required MSBuild properties**
    - **Validates: Requirements 2.2, 5.4**

- [x] 5. Checkpoint - Verify sample file structure
  - Ensure all project files, source files, and configuration files exist at the correct paths. Ensure all tests pass, ask the user if questions arise.

- [x] 6. Create Getting Started documentation
  - [x] 6.1 Create `docs/Getting-Started/full-project-setup.md`
    - Explain what SLNX format is and why it replaces legacy `.sln`
    - Note minimum Visual Studio version (VS 2026 / 17.10+) for SLNX support
    - Describe prerequisite steps: restoring the .NET tool manifest (`dotnet tool restore`) to install the Fifth compiler tool
    - Explain that Fifth.Sdk is resolved automatically from NuGet via `global.json`
    - Note that the compiler tool and SDK versions must match (both 0.7.1)
    - Step-by-step: create directory structure, create each `.5thproj` file, configure ProjectReference elements, set up `global.json` and tool manifest
    - Describe how to build (`dotnet build`) and run (`dotnet run`) the solution
    - Describe Visual Studio workflows: opening `.slnx`, building from IDE, setting startup project
    - Use concise, direct language — no tangential explanations
    - _Requirements: 8.1, 8.2, 8.3, 8.4, 8.5, 8.6, 8.7, 8.8, 9.4, 10.1, 10.2, 10.3, 10.4, 10.5_
  - [x] 6.2 Update `mkdocs.yml` nav to include the new guide under Getting Started
    - Add entry `Full Project Setup: Getting-Started/full-project-setup.md` after the existing Getting Started items
    - _Requirements: 8.9_

- [x] 7. Final checkpoint - Ensure all tests pass
  - Ensure all tests pass, ask the user if questions arise.

## Notes

- Tasks marked with `*` are optional and can be skipped for faster MVP
- Each task references specific requirements for traceability
- Checkpoints ensure incremental validation
- Property tests validate that all `.5thproj` files declare required MSBuild properties (Property 1 from design)
- This is a file-creation feature — no runtime code, SDK changes, or compiler modifications are needed
