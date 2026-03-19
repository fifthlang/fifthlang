# Implementation Plan: SDK Cross-Project References

## Overview

Two coordinated changes enable multi-project Fifth solutions to build and run without MSBuild workarounds: (1) SDK `Sdk.targets` gets a `GetTargetPath` target and updated `ResolveFifthProjectReferences` to auto-populate `ReferencePath`, and (2) the compiler's Roslyn translator discovers public static types in referenced assemblies and generates `using static` directives. Implementation language is C# (matching the existing codebase).

## Tasks

- [x] 1. Add `GetTargetPath` target and update `ResolveFifthProjectReferences` in SDK
  - [x] 1.1 Add `GetTargetPath` target to `src/Fifth.Sdk/Sdk/Sdk.targets`
    - Add a new `<Target Name="GetTargetPath" Outputs="$(_FifthAbsoluteOutputPath)">` after the existing property groups
    - Compute `_FifthAbsoluteOutputPath` using `System.IO.Path.IsPathRooted` check on `$(FifthOutputPath)`
    - When relative, prepend `$(MSBuildProjectDirectory)\$(FifthOutputPath)`; when absolute, use `$(FifthOutputPath)` unchanged
    - _Requirements: 1.1, 1.2, 1.3, 8.1, 8.2, 8.3_

  - [x] 1.2 Update `ResolveFifthProjectReferences` target in `src/Fifth.Sdk/Sdk/Sdk.targets`
    - After the existing `<MSBuild Projects="@(ProjectReference)" Targets="Build" .../>` call, add a second `<MSBuild>` call with `Targets="GetTargetPath"`
    - Add `<Output TaskParameter="TargetOutputs" ItemName="ReferencePath" />` to populate the `ReferencePath` item group
    - Pass same `Properties="Configuration=$(Configuration);TargetFramework=$(TargetFramework)"` to the second call
    - _Requirements: 2.1, 2.2, 2.3, 2.4_

  - [x] 1.3 Write property test for absolute path resolution (Property 1)
    - **Property 1: Absolute Path Resolution**
    - Generate random directory paths and random relative file paths; verify combining them always produces a rooted path
    - Generate random absolute paths and verify they are returned unchanged
    - Add FsCheck and FsCheck.Xunit packages to `test/ast-tests/ast_tests.csproj`
    - Create test class `CrossProjectReferencePropertyTests.cs` in `test/ast-tests/`
    - **Validates: Requirements 1.2, 1.3, 8.1, 8.2, 8.3**

- [x] 2. Checkpoint - Verify SDK changes
  - Ensure all tests pass, ask the user if questions arise.

- [x] 3. Implement `DiscoverPublicStaticTypes` in the Roslyn translator
  - [x] 3.1 Add `DiscoverPublicStaticTypes` method to `src/compiler/LoweredToRoslyn/LoweredAstToRoslynTranslator.cs`
    - Add `private static IReadOnlyList<string> DiscoverPublicStaticTypes(string assemblyPath)` method
    - Use `System.Reflection.MetadataLoadContext` to load assembly metadata without executing code
    - Iterate exported types, filter for public static classes (`abstract` + `sealed` in IL) with at least one public static method
    - Exclude compiler-generated types (names containing `<`, types with `CompilerGenerated` attribute)
    - Return fully qualified type names (e.g., `"CoreLib.Program"`)
    - Handle missing/invalid assembly paths gracefully: log warning diagnostic, return empty list
    - _Requirements: 3.1, 3.2, 3.3, 3.4_

  - [x] 3.2 Write property test for public static type discovery (Property 2)
    - **Property 2: Public Static Type Discovery Completeness**
    - Generate test assemblies via Roslyn in-memory compilation with random numbers of public static classes (each with random public static methods) plus non-static classes as noise
    - Verify `DiscoverPublicStaticTypes` returns exactly the public static types with methods
    - Add test to `CrossProjectReferencePropertyTests.cs` in `test/ast-tests/`
    - **Validates: Requirements 3.1, 3.2, 3.3**

- [x] 4. Wire translator options and generate `using static` directives
  - [x] 4.1 Add `Translate(AssemblyDef, TranslatorOptions?)` overload to `LoweredAstToRoslynTranslator`
    - Add new public method `Translate(AssemblyDef assembly, TranslatorOptions? options)` that calls `TranslateAssembly(assembly, options)`
    - Update existing `Translate(AssemblyDef)` to call `TranslateAssembly(assembly, null)` (should already be the case)
    - Thread `TranslatorOptions` parameter through `TranslateAssembly` and `BuildSyntaxTreeFromModule`
    - _Requirements: 3.1, 4.1_

  - [x] 4.2 Generate `using static` directives from additional references in `BuildSyntaxTreeFromModule`
    - After the existing `using static` directives and namespace import loop, add a new block
    - Iterate `options?.AdditionalReferences`, call `DiscoverPublicStaticTypes` for each
    - For each discovered type, add `using static FullyQualifiedTypeName` directive using existing `usingTracker` for dedup
    - Use `UsingDirective(ParseName(fullTypeName)).WithStaticKeyword(Token(SyntaxKind.StaticKeyword))` matching existing pattern
    - _Requirements: 4.1, 4.2, 4.3, 4.4_

  - [x] 4.3 Update `RoslynEmissionPhase` in `src/compiler/Compiler.cs` to pass references to translator
    - Construct `TranslatorOptions` with `AdditionalReferences` populated from `options.References` (filter for non-empty, existing file paths)
    - Call `translator.Translate(assemblyDef, translatorOptions)` instead of `translator.Translate(assemblyDef)`
    - _Requirements: 3.1, 4.1_

  - [x] 4.4 Write property test for using static generation correctness (Property 3)
    - **Property 3: Using Static Generation Correctness**
    - Generate random sets of type names, create test assemblies containing those types
    - Translate a minimal module with those assemblies as references
    - Verify the generated C# contains `using static` for each type with the correct fully qualified name
    - Add test to `CrossProjectReferencePropertyTests.cs` in `test/ast-tests/`
    - **Validates: Requirements 4.1, 4.2**

  - [x] 4.5 Write property test for no duplicate using static directives (Property 4)
    - **Property 4: No Duplicate Using Static Directives**
    - Generate random sets of type names including intentional duplicates across multiple assemblies
    - Translate a minimal module with those assemblies as references
    - Verify the count of `using static` directives equals the count of distinct type names
    - Add test to `CrossProjectReferencePropertyTests.cs` in `test/ast-tests/`
    - **Validates: Requirements 4.3**

  - [x] 4.6 Write property test for backward compatibility (Property 5)
    - **Property 5: Backward Compatibility — No Extra Directives Without References**
    - Generate random Fifth modules (varying numbers of functions)
    - Translate with `AdditionalReferences = null`
    - Verify the using directive set matches the known default set exactly (no additional `using static` directives)
    - Add test to `CrossProjectReferencePropertyTests.cs` in `test/ast-tests/`
    - **Validates: Requirements 4.4, 9.2, 9.3**

- [x] 5. Checkpoint - Verify compiler changes
  - Ensure all tests pass, ask the user if questions arise.

- [x] 6. Clean up sample projects and wire end-to-end
  - [x] 6.1 Clean up sample `.5thproj` files in `samples/FullProjectExample/`
    - Remove any `GetTargetPath` target overrides from `.5thproj` files
    - Remove any `PopulateFifthReferencePaths` targets from `.5thproj` files
    - Remove any `DefaultTargets="Build"` attributes from `.5thproj` files
    - Ensure each `.5thproj` contains only `<PropertyGroup>` (OutputType, TargetFramework, FifthCompilerCommand) and `<ItemGroup>` (ProjectReference) as needed
    - _Requirements: 5.1, 5.2, 5.3, 5.4_

  - [x] 6.2 Write unit tests for translator with references
    - Test `DiscoverPublicStaticTypes` with: one public static class → returns that class name; no public static classes → empty list; mix of static and non-static → only static ones; non-existent path → empty list; invalid DLL → empty list
    - Test `BuildSyntaxTreeFromModule` using directives: no additional references → default directives only; one reference with one public static type → adds one `using static`; multiple references → adds `using static` for each; duplicate type across references → only one emitted
    - Add tests to `test/ast-tests/CrossProjectReferenceTests.cs`
    - _Requirements: 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4, 9.2, 9.3_

- [x] 7. Final checkpoint - Ensure all tests pass
  - Ensure all tests pass, ask the user if questions arise.

## Notes

- Tasks marked with `*` are optional and can be skipped for faster MVP
- Each task references specific requirements for traceability
- Property tests use FsCheck + FsCheck.Xunit and should be added to `test/ast-tests/`
- The `TranslatorOptions` class already has `AdditionalReferences` property — no new class needed
- Build commands are long-running (~60-70s); never cancel them (see AGENTS.md)
- Checkpoints ensure incremental validation between SDK and compiler changes
