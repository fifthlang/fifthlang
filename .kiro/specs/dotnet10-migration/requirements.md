# Requirements Document

## Introduction

Migrate the entire Fifth Language solution (`fifthlang.sln`) from .NET 8 to .NET 10. This involves updating the SDK version, all project target frameworks, all NuGet package dependencies to their latest stable .NET 10-compatible versions, removing the `EnableNet10` multi-targeting conditional compilation infrastructure, and simplifying `#if NET10_0_OR_GREATER` preprocessor directives in source code. After migration, the platform exclusively targets .NET 10 with no .NET 8 support retained.

## Glossary

- **Solution**: The `fifthlang.sln` Visual Studio solution file containing all Fifth Language projects
- **Global_JSON**: The `global.json` file at the repository root that pins the .NET SDK version
- **Directory_Build_Props**: The `Directory.Build.props` file providing centralized MSBuild properties for all projects
- **Project_File**: A `.csproj` MSBuild project file defining build configuration and dependencies for a single project
- **Target_Framework**: The `<TargetFramework>` or `<TargetFrameworks>` MSBuild property specifying which .NET runtime(s) a project compiles against
- **EnableNet10_Flag**: The `$(EnableNet10)` MSBuild property currently used to conditionally enable multi-targeting for `net8.0;net10.0`
- **Conditional_Compilation_Directive**: C# preprocessor directives (`#if NET10_0_OR_GREATER`, `#else`, `#endif`) used to include or exclude code based on target framework
- **Conditional_ItemGroup**: MSBuild `<ItemGroup Condition="...">` elements that include packages or items only for specific target frameworks
- **Roslyn_Toolset**: The pinned `Microsoft.Net.Compilers.Toolset` package and associated `RoslynVersion` property in Directory_Build_Props
- **Compiler_Project**: The `src/compiler/compiler.csproj` project that produces the `fifthc` CLI tool
- **Sample_Project**: Example projects under `samples/` that demonstrate Fifth Language usage (FullProjectExample, TaskListQuadStore)
- **Build_Script**: The `Justfile` and shell scripts under `scripts/` that automate build, test, and release workflows
- **CI_Pipeline**: GitHub Actions workflows under `.github/` that run automated builds and tests
- **AGENTS_MD**: The `AGENTS.md` file providing operational guidance for automated agents

## Requirements

### Requirement 1: Update .NET SDK Version

**User Story:** As a developer, I want the repository to use the .NET 10 SDK, so that all projects build against the .NET 10 runtime and toolchain.

#### Acceptance Criteria

1. WHEN a developer clones the repository, THE Global_JSON SHALL specify a .NET 10.x SDK version in the `sdk.version` field
2. WHEN a developer runs `dotnet --version` with the repository's Global_JSON active, THE SDK SHALL report a 10.x version number
3. THE Global_JSON SHALL set `rollForward` to `latestFeature` to allow patch-level SDK updates within .NET 10
4. THE Global_JSON SHALL set `allowPrerelease` to `false` to ensure only stable SDK releases are used

### Requirement 2: Update All Project Target Frameworks

**User Story:** As a developer, I want every project in the solution to target `net10.0` exclusively, so that the codebase uses a single consistent target framework.

#### Acceptance Criteria

1. THE Solution SHALL contain only projects that target `net10.0` as their sole Target_Framework
2. WHEN a Project_File previously used `<TargetFrameworks>` (plural) with the EnableNet10_Flag conditional, THE Project_File SHALL use `<TargetFramework>net10.0</TargetFramework>` (singular) instead
3. WHEN a Project_File previously used `<TargetFramework>net8.0</TargetFramework>`, THE Project_File SHALL use `<TargetFramework>net10.0</TargetFramework>` instead
4. THE Solution SHALL contain zero references to `net8.0` in any Project_File

### Requirement 3: Remove Multi-Targeting Infrastructure

**User Story:** As a developer, I want the EnableNet10 conditional compilation infrastructure removed, so that the build configuration is simplified and there is a single code path.

#### Acceptance Criteria

1. THE Solution SHALL contain zero references to the `EnableNet10` MSBuild property in any Project_File
2. WHEN a Project_File previously contained `<TargetFrameworks Condition="'$(EnableNet10)' == 'true'">` elements, THE Project_File SHALL replace them with a single `<TargetFramework>net10.0</TargetFramework>` element
3. THE Directory_Build_Props SHALL contain zero references to the `EnableNet10` property

### Requirement 4: Simplify Conditional Compilation Directives in Source Code

**User Story:** As a developer, I want `#if NET10_0_OR_GREATER` preprocessor blocks removed and their contents unconditionally included, so that the source code is cleaner and easier to maintain.

#### Acceptance Criteria

1. THE Solution SHALL contain zero `#if NET10_0_OR_GREATER` Conditional_Compilation_Directives in any C# source file
2. WHEN a source file contained a `#if NET10_0_OR_GREATER` block, THE source file SHALL retain only the code from the `#if` branch (the .NET 10 code path)
3. WHEN a source file contained a `#else` block paired with `#if NET10_0_OR_GREATER`, THE source file SHALL remove the `#else` branch code entirely
4. THE Solution SHALL contain zero `#endif` directives that were paired with `#if NET10_0_OR_GREATER`

### Requirement 5: Simplify Conditional MSBuild ItemGroups

**User Story:** As a developer, I want framework-conditional `<ItemGroup>` elements in project files merged into unconditional item groups, so that all dependencies are straightforward.

#### Acceptance Criteria

1. THE Solution SHALL contain zero `<ItemGroup Condition="'$(TargetFramework)' == 'net10.0'">` elements in any Project_File
2. WHEN a Project_File contained a Conditional_ItemGroup for `net10.0`, THE Project_File SHALL move those package references into an unconditional `<ItemGroup>`
3. THE Solution SHALL contain zero `<ItemGroup>` elements conditioned on any specific `TargetFramework` value

### Requirement 6: Update NuGet Package Dependencies

**User Story:** As a developer, I want all NuGet packages updated to their latest stable versions compatible with .NET 10, so that the project benefits from bug fixes, performance improvements, and .NET 10 support.

#### Acceptance Criteria

1. WHEN a Project_File references a NuGet package, THE Project_File SHALL reference the latest stable version of that package compatible with `net10.0`
2. THE `Microsoft.NET.Test.Sdk` package SHALL be updated to the latest stable version across all test Project_Files
3. THE `xunit` and `xunit.runner.visualstudio` packages SHALL be updated to the latest stable versions across all test Project_Files
4. THE `FluentAssertions` package SHALL be updated to the latest stable version compatible with `net10.0`
5. THE `FsCheck` and `FsCheck.Xunit` packages SHALL be updated to the latest stable versions compatible with `net10.0`
6. THE `Antlr4.Runtime.Standard` package SHALL be updated to the latest stable version compatible with `net10.0`
7. THE `dotNetRdf` package SHALL be updated to the latest stable version compatible with `net10.0`
8. THE `Microsoft.CodeAnalysis.CSharp` package (Roslyn) SHALL be updated to a version compatible with .NET 10
9. THE `System.Reflection.Metadata` and `System.Reflection.MetadataLoadContext` packages SHALL be updated to the latest stable versions for .NET 10
10. THE `Microsoft.Extensions.Logging.Console` package SHALL be updated to the latest stable version for .NET 10
11. THE `BenchmarkDotNet` package SHALL be updated to the latest stable version compatible with `net10.0`
12. THE `coverlet.collector` package SHALL be updated to the latest stable version compatible with `net10.0`
13. THE `OmniSharp.Extensions.LanguageServer` and `OmniSharp.Extensions.LanguageProtocol` packages SHALL be updated to the latest stable versions compatible with `net10.0`
14. THE `RazorLight` package SHALL be updated to the latest stable version compatible with `net10.0`
15. THE `Dunet` and `Vogen` source generator packages SHALL be updated to the latest stable versions compatible with `net10.0`
16. THE `Microsoft.Build` package SHALL be updated to the latest stable version compatible with `net10.0`

### Requirement 7: Update Roslyn Toolset Configuration

**User Story:** As a developer, I want the pinned Roslyn toolset version in Directory.Build.props updated for .NET 10 compatibility, so that CI and Release builds use a compatible C# compiler.

#### Acceptance Criteria

1. THE Directory_Build_Props SHALL set `RoslynVersion` to a version compatible with the .NET 10 SDK
2. THE Directory_Build_Props SHALL remove the comment stating that Roslyn 4.14.0 requires .NET 9 and breaks under .NET 8
3. THE Directory_Build_Props SHALL update `LangVersion` settings to reflect C# language version capabilities of .NET 10
4. THE Directory_Build_Props SHALL update `SystemReflectionMetadataVersion` to the latest stable version for .NET 10

### Requirement 8: Update Build Scripts and Tooling References

**User Story:** As a developer, I want build scripts and tooling references updated to reflect .NET 10, so that developer workflows and CI pipelines function correctly.

#### Acceptance Criteria

1. WHEN the Justfile contains hardcoded references to `net8.0` output paths, THE Justfile SHALL update those references to `net10.0`
2. WHEN the AGENTS_MD references .NET 8 SDK versions or `net8.0` paths, THE AGENTS_MD SHALL update those references to .NET 10
3. WHEN CI_Pipeline workflow files reference .NET 8 SDK versions or `net8.0`, THE CI_Pipeline files SHALL update those references to .NET 10
4. WHEN shell scripts under `scripts/` reference `net8.0`, THE scripts SHALL update those references to `net10.0`

### Requirement 9: Update Sample Projects

**User Story:** As a developer, I want sample projects updated to target .NET 10, so that examples remain consistent with the main solution.

#### Acceptance Criteria

1. WHEN a Sample_Project contains a `global.json`, THE Sample_Project global.json SHALL be updated to reference a .NET 10-compatible SDK version
2. WHEN a Sample_Project contains a `.csproj` with a `net8.0` Target_Framework, THE Sample_Project SHALL update to `net10.0`
3. WHEN a Sample_Project references Fifth.Sdk, THE Sample_Project SHALL remain compatible with the updated SDK

### Requirement 10: Validate Solution Integrity After Migration

**User Story:** As a developer, I want confidence that the migration preserves all existing functionality, so that no regressions are introduced.

#### Acceptance Criteria

1. WHEN `dotnet restore fifthlang.sln` is executed, THE Solution SHALL restore all packages without errors
2. WHEN `dotnet build fifthlang.sln` is executed, THE Solution SHALL compile without errors
3. WHEN `dotnet test fifthlang.sln` is executed, THE Solution SHALL pass all existing tests
4. WHEN the AST code generator is executed, THE generator SHALL produce valid output identical in structure to the pre-migration output
5. WHEN the ANTLR parser generation runs during build, THE parser generation SHALL complete without errors
