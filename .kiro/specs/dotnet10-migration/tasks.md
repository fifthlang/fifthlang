# Implementation Plan: .NET 10 Migration

## Overview

Migrate the Fifth Language solution from .NET 8 to .NET 10 in a single coordinated change. Tasks proceed layer-by-layer: build infrastructure first, then project files, source code cleanup, dependency updates, tooling/docs, samples, and finally validation. Each task builds on the previous so the solution remains coherent at each checkpoint.

## Tasks

- [ ] 1. Update build infrastructure (global.json and Directory.Build.props)
  - [ ] 1.1 Update global.json to .NET 10 SDK
    - Change `sdk.version` from `8.0.414` to the latest stable .NET 10 SDK version (e.g., `10.0.100`)
    - Change `rollForward` from `latestMajor` to `latestFeature`
    - Change `allowPrerelease` from `true` to `false`
    - _Requirements: 1.1, 1.2, 1.3, 1.4_

  - [ ] 1.2 Update Directory.Build.props for .NET 10 compatibility
    - Update `RoslynVersion` from `4.11.0` to a .NET 10-compatible version (e.g., `4.14.0` or later)
    - Remove the comment: `"Note: 4.14.0 requires System.Runtime 9.0 (NET 9) which breaks under our .NET 8 SDK."`
    - Update `SystemReflectionMetadataVersion` from `9.0.0` to `10.0.0`
    - Update the `LangVersion` comment to reference C# 14 / .NET 10 instead of .NET 8/9
    - _Requirements: 7.1, 7.2, 7.3, 7.4_

- [ ] 2. Update project files with EnableNet10 multi-targeting removal (6 projects)
  - [ ] 2.1 Replace conditional TargetFrameworks with single TargetFramework in all 6 projects
    - In each file, replace the `<TargetFrameworks Condition="'$(EnableNet10)' == 'true'">net8.0;net10.0</TargetFrameworks>` and `<TargetFrameworks Condition="'$(EnableNet10)' != 'true'">net8.0</TargetFrameworks>` pair with `<TargetFramework>net10.0</TargetFramework>`
    - Files: `src/ast-model/ast_model.csproj`, `src/ast-generated/ast_generated.csproj`, `src/parser/parser.csproj`, `src/compiler/compiler.csproj`, `src/fifthlang.system/Fifth.System.csproj`, `test/runtime-integration-tests/runtime-integration-tests.csproj`
    - _Requirements: 2.1, 2.2, 3.1, 3.2_

  - [ ] 2.2 Merge conditional ItemGroups in Fifth.System.csproj and runtime-integration-tests.csproj
    - Move QuadStore package references from `<ItemGroup Condition="'$(TargetFramework)' == 'net10.0'">` into the unconditional `<ItemGroup>`
    - Remove the now-empty conditional ItemGroup elements
    - Files: `src/fifthlang.system/Fifth.System.csproj`, `test/runtime-integration-tests/runtime-integration-tests.csproj`
    - _Requirements: 5.1, 5.2, 5.3_


- [ ] 3. Update project files with simple net8.0→net10.0 target change (13 projects)
  - [ ] 3.1 Update TargetFramework in all 13 simple-target projects
    - Change `<TargetFramework>net8.0</TargetFramework>` to `<TargetFramework>net10.0</TargetFramework>` in each file
    - Files: `src/ast_generator/ast_generator.csproj`, `src/Fifth.Sdk/Fifth.Sdk.csproj`, `src/language-server/Fifth.LanguageServer.csproj`, `src/tools/validate-examples/validate-examples.csproj`, `src/tools/pe_inspect/pe_inspect.csproj`, `test/ast-tests/ast_tests.csproj`, `test/syntax-parser-tests/syntax-parser-tests.csproj`, `test/fifth-runtime-tests/fifth-runtime-tests.csproj`, `test/fifth-sdk-tests/fifth_sdk_tests.csproj`, `test/kg-smoke-tests/kg-smoke-tests.csproj`, `test/language-server-smoke/LanguageServerSmoke.csproj`, `test/perf/perf-assertions/perf-assertions.csproj`, `test/perf/guard-validation-perf/guard-validation-perf.csproj`
    - _Requirements: 2.1, 2.3, 2.4_

- [ ] 4. Remove conditional compilation directives from C# source files
  - [ ] 4.1 Remove `#if NET10_0_OR_GREATER` blocks from KnowledgeGraphs.cs and Store.cs
    - In `src/fifthlang.system/KnowledgeGraphs.cs`: remove 2 `#if`/`#else`/`#endif` blocks, keep the `#if` branch (QuadStore path), remove the `#else` branch (in-memory fallback)
    - In `src/fifthlang.system/Store.cs`: remove 2 blocks (using directive + method), keep the `#if` branch code, make `using QuadStoreNs = TripleStore.Core;` unconditional
    - _Requirements: 4.1, 4.2, 4.3, 4.4_

  - [ ] 4.2 Remove `#if NET10_0_OR_GREATER` wrappers from test files
    - In `test/runtime-integration-tests/QuadStore_Integration_Tests.cs`: remove 4 `#if`/`#endif` wrapper blocks, keep enclosed code
    - In `test/runtime-integration-tests/QuadStore_Property_Tests.cs`: remove file-level `#if`/`#endif` wrapper, keep enclosed code
    - _Requirements: 4.1, 4.3, 4.4_

- [ ] 5. Checkpoint — Verify build infrastructure changes
  - Run `dotnet restore fifthlang.sln` and `dotnet build fifthlang.sln` to verify all project file and source code changes compile correctly before proceeding to dependency updates
  - Ensure all tests pass, ask the user if questions arise.

- [ ] 6. Update NuGet package dependencies to .NET 10-compatible versions
  - [ ] 6.1 Update test infrastructure packages across all test projects
    - Update `Microsoft.NET.Test.Sdk` to latest stable 17.x
    - Update `xunit` to latest stable 2.x
    - Update `xunit.runner.visualstudio` to latest stable 2.x
    - Update `coverlet.collector` to latest stable 6.x
    - _Requirements: 6.1, 6.2, 6.3, 6.12_

  - [ ] 6.2 Update assertion and property-testing packages
    - Update `FluentAssertions` to latest stable version across all projects (align `parser.csproj` from `6.0.0-alpha0002` to the same stable version)
    - Update `FsCheck` and `FsCheck.Xunit` to latest stable versions
    - Update `AutoFixture` to latest stable 4.x
    - _Requirements: 6.4, 6.5_

  - [ ] 6.3 Update Roslyn, reflection, and compiler tooling packages
    - Update `Microsoft.CodeAnalysis.CSharp` to match the new `RoslynVersion` in Directory.Build.props
    - Update `System.Reflection.MetadataLoadContext` to `10.0.0` in compiler.csproj
    - Verify `System.Reflection.Metadata` uses `$(SystemReflectionMetadataVersion)` which is now `10.0.0`
    - _Requirements: 6.8, 6.9_

  - [ ] 6.4 Update runtime and framework packages
    - Update `Antlr4.Runtime.Standard` to latest stable 4.13.x
    - Update `dotNetRdf` to latest stable 3.x
    - Update `Microsoft.Extensions.Logging.Console` to `10.0.0`
    - Update `BenchmarkDotNet` to latest stable 0.14.x
    - _Requirements: 6.6, 6.7, 6.10, 6.11_

  - [ ] 6.5 Update remaining packages
    - Update `OmniSharp.Extensions.LanguageServer` and `OmniSharp.Extensions.LanguageProtocol` to latest compatible versions
    - Update `RazorLight` to latest compatible version
    - Update `Dunet` and `Vogen` source generator packages to latest stable versions
    - Update `Microsoft.Build` to latest stable 17.x
    - _Requirements: 6.13, 6.14, 6.15, 6.16_


- [ ] 7. Checkpoint — Restore and build with updated dependencies
  - Run `dotnet restore fifthlang.sln` and `dotnet build fifthlang.sln --configuration Release` to verify all package updates resolve and compile
  - Run `dotnet test fifthlang.sln --configuration Release` to verify existing tests pass with new dependencies
  - Ensure all tests pass, ask the user if questions arise.

- [ ] 8. Update build scripts and tooling references
  - [ ] 8.1 Update Justfile net8.0 output paths
    - Change `net8.0` to `net10.0` in the `install-cli` recipe symlink path (`src/compiler/bin/Release/net8.0/compiler` → `src/compiler/bin/Release/net10.0/compiler`)
    - Change `net8.0` to `net10.0` in the `install-cli-quick` recipe symlink path (`src/compiler/bin/Debug/net8.0/compiler` → `src/compiler/bin/Debug/net10.0/compiler`)
    - _Requirements: 8.1_

  - [ ] 8.2 Update AGENTS.md .NET version references
    - Update prerequisites section: change `8.0.x` and `8.0.118` references to .NET 10 equivalents
    - Update any other `.NET 8` or `net8.0` references throughout the file
    - _Requirements: 8.2_

  - [ ] 8.3 Update CI workflow (.github/workflows/ci.yml)
    - Change step name `Setup .NET 8` to `Setup .NET 10`
    - Change `dotnet-version: '8.0.415'` to the latest stable .NET 10 SDK version
    - _Requirements: 8.3_

  - [ ] 8.4 Update copilot instructions (.github/copilot-instructions.md)
    - Update all references to `.NET 8.0 SDK`, `8.0.118`, `8.0.x`, `.NET 8.0` to .NET 10 equivalents
    - Update the Active Technologies section entries referencing `.NET 8.0`
    - _Requirements: 8.3_

  - [ ] 8.5 Update sample TaskListQuadStore README
    - Remove references to `EnableNet10=true` build flag and `-p:EnableNet10=true` from build instructions
    - Update build instructions to reflect that QuadStore is now unconditionally available on net10.0
    - _Requirements: 9.1_

- [ ] 9. Update sample projects
  - [ ] 9.1 Update sample global.json files
    - Update `samples/FullProjectExample/global.json` if it pins an SDK version — update to .NET 10
    - Update `samples/TaskListQuadStore/global.json` if it pins an SDK version — update to .NET 10
    - _Requirements: 9.1, 9.2, 9.3_

- [ ] 10. Write property-based validation tests
  - [ ]* 10.1 Write property test: Sole net10.0 target with no net8.0 remnants
    - **Property 1: Sole net10.0 target with no net8.0 remnants**
    - Create a test that scans all `.csproj` files in the solution and asserts each contains exactly one `<TargetFramework>net10.0</TargetFramework>` and zero occurrences of `net8.0`
    - Place in an appropriate test project (e.g., `test/fifth-sdk-tests/` or a new migration validation test file)
    - **Validates: Requirements 2.1, 2.2, 2.3, 2.4**

  - [ ]* 10.2 Write property test: EnableNet10 infrastructure fully removed
    - **Property 2: EnableNet10 infrastructure fully removed**
    - Create a test that scans all `.csproj` and `Directory.Build.props` files and asserts zero occurrences of `EnableNet10`
    - **Validates: Requirements 3.1, 3.2, 3.3**

  - [ ]* 10.3 Write property test: Conditional compilation directives removed
    - **Property 3: Conditional compilation directives removed**
    - Create a test that scans all `.cs` files in the repository and asserts zero occurrences of `#if NET10_0_OR_GREATER`
    - **Validates: Requirements 4.1, 4.3, 4.4**

  - [ ]* 10.4 Write property test: No framework-conditional ItemGroups
    - **Property 4: No framework-conditional ItemGroups**
    - Create a test that scans all `.csproj` files and asserts zero `<ItemGroup>` elements with a `Condition` attribute referencing `$(TargetFramework)`
    - **Validates: Requirements 5.1, 5.2, 5.3**

- [ ] 11. Final validation — Full solution restore, build, and test
  - Run `dotnet restore fifthlang.sln`
  - Run `dotnet build fifthlang.sln --configuration Release`
  - Run `dotnet test fifthlang.sln --configuration Release`
  - Run `just run-generator` to verify AST code generation on net10.0
  - Run `dotnet test -p:UsePinnedRoslyn=true -v minimal` to verify pinned Roslyn toolset
  - Ensure all tests pass, ask the user if questions arise.
  - _Requirements: 10.1, 10.2, 10.3, 10.4, 10.5_

## Notes

- Tasks marked with `*` are optional and can be skipped for faster MVP
- Each task references specific requirements for traceability
- Checkpoints at tasks 5, 7, and 11 ensure incremental validation
- Property tests validate the four correctness properties from the design document
- The migration is a single atomic change — all tasks should be committed together
