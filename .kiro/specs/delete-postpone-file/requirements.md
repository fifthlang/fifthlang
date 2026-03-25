# Requirements Document

## Introduction

This document captures the requirements for REM-001: deleting the legacy file `src/compiler/FifthParserManager.cs.postpone` from the repository. The file is a 219-line inert artifact with a `.postpone` extension that prevents compilation. It references non-existent namespaces (`il_ast`, `compiler.LangProcessingPhases`) and misleads contributors into believing an IL pipeline exists. Removing it is a zero-risk cleanup with no code or behavioural impact.

## Glossary

- **Postpone_File**: The file `src/compiler/FifthParserManager.cs.postpone` — a legacy C# source file rendered inert by its `.postpone` extension, which excludes it from MSBuild compilation globs.
- **Repository**: The `aabs/fifthlang` git repository containing the Fifth language compiler, runtime, and tooling.
- **Build_System**: The `dotnet build` / MSBuild toolchain that compiles the `fifthlang.sln` solution.
- **Test_Suite**: The full set of tests executed by `dotnet test fifthlang.sln`.

## Requirements

### Requirement 1: Delete the Postpone File

**User Story:** As a contributor, I want the legacy `FifthParserManager.cs.postpone` file removed from the repository, so that I am not confused by stale references to a non-existent IL pipeline.

#### Acceptance Criteria

1. WHEN the deletion is applied, THE Repository SHALL no longer contain the file `src/compiler/FifthParserManager.cs.postpone` in the working tree or git index.

### Requirement 2: Build Integrity After Deletion

**User Story:** As a contributor, I want the solution to build cleanly after the file is removed, so that I can confirm the file had no compilation dependency.

#### Acceptance Criteria

1. WHEN the Postpone_File has been deleted, THE Build_System SHALL compile `fifthlang.sln` with zero errors and no new warnings.

### Requirement 3: Test Integrity After Deletion

**User Story:** As a contributor, I want all existing tests to pass after the file is removed, so that I can confirm the file had no runtime or test dependency.

#### Acceptance Criteria

1. WHEN the Postpone_File has been deleted, THE Test_Suite SHALL pass all tests with the same results as before the deletion.
