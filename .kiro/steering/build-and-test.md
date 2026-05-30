---
description: build-and-test
inclusion: always
---
## Prerequisites
- BUILD-001 [MANDATORY]: Use the pinned toolchain: .NET SDK `10.0.100` from `global.json`, Java 17+, and `src/parser/tools/antlr-4.13.2-complete.jar`. To comply, run `dotnet --version` and `java -version` before troubleshooting build failures.
## Commands
- BUILD-002: Use `dotnet restore fifthlang.sln` as the standard restore entry point. To comply, do not cancel restore; set automation timeout to at least 120 seconds.
- BUILD-003: Use `dotnet build fifthlang.sln` as the standard build entry point. To comply, do not cancel build; set automation timeout to at least 120 seconds.
## Testing
- BUILD-004: Use `dotnet test fifthlang.sln` as the default regression gate. To comply, do not cancel test runs; set automation timeout to at least 5 minutes.
- BUILD-005: Use `dotnet test test/ast-tests/ast_tests.csproj` only as a local smoke test. To comply, run full-solution tests before merging behavior changes.
- BUILD-014: Use targeted `just` test commands for local iteration, not as the final gate. To comply, after `just test-ast`, `just test-runtime`, `just test-syntax`, or `just test-all-roslyn`, still run `dotnet test fifthlang.sln`.
## Generation
- BUILD-006: After metamodel changes, regenerate AST code with `dotnet run --project src/ast_generator/ast_generator.csproj -- --folder src/ast-generated`. To comply, include regeneration in the same change set as the metamodel update.
- BUILD-011: Do not add manual AST generation to the normal workflow. To comply, use manual generation only for focused regeneration tasks.
## Verification
- BUILD-007: Validate tool versions before diagnosing restore or build errors. To comply, ensure `.NET` reports `10.0.x` and Java reports 17 or newer.
## Dependency
- BUILD-008: Build through `fifthlang.sln` so dependency order is resolved correctly. To comply, avoid ad hoc partial builds when validating integration behavior.
## Workflow
- BUILD-009: Do not cancel restore, build, test, or generation commands because long runtimes are expected. To comply, wait for completion unless the command is clearly hung.
## Parser
- BUILD-010: Do not add manual ANTLR generation to the normal workflow. To comply, rely on parser-project build targets for grammar compilation.
## Diagnostics
- BUILD-012: Treat known baseline warnings as expected unless their pattern changes. To comply, ignore existing ANTLR `assoc`, nullable, and switch-exhaustiveness warnings unless new or altered.
## Validation
- BUILD-013: Validate changes in order: build, full test, then runtime behavior. To comply, run `dotnet build fifthlang.sln`, `dotnet test fifthlang.sln`, then verify behavior manually or with integration tests.
