---
inclusion: auto
---

# Build and Test Commands

## Prerequisites

- .NET 8.0 SDK (pinned via `global.json` to 8.0.118)
- Java 17+ (for ANTLR grammar compilation)
- ANTLR 4.8 jar included at `src/parser/tools/antlr-4.8-complete.jar`

## Essential Commands

```bash
# Restore (takes ~70s — NEVER CANCEL, set timeout to 120+ seconds)
dotnet restore fifthlang.sln

# Build (takes ~60s — NEVER CANCEL, set timeout to 120+ seconds)
dotnet build fifthlang.sln

# Run all tests (default regression gate — NEVER CANCEL, set timeout to 5+ minutes)
dotnet test fifthlang.sln

# Quick smoke test subset
dotnet test test/ast-tests/ast_tests.csproj

# Regenerate AST code after metamodel changes
dotnet run --project src/ast_generator/ast_generator.csproj -- --folder src/ast-generated
```

## Build Order Dependencies

ast-model → ast_generator → ast-generated → parser → code_generator → compiler → tests

Always build the full solution rather than individual projects.

## Critical Rules

- NEVER CANCEL any restore, build, test, or generation operation
- ANTLR grammar compilation happens automatically during parser project build
- AST code generation runs automatically before compilation via MSBuild targets
- Build warnings for ANTLR `assoc` option, C# nullable references, and switch exhaustiveness are expected and safe to ignore

## Validation Protocol (after any change)

1. `dotnet build fifthlang.sln` — build validation
2. `dotnet test fifthlang.sln` — full regression gate
3. Verify runtime behavior — compilation alone is NOT sufficient

## Granular Test Targets

```bash
just test-ast          # AST tests only
just test-runtime      # Runtime integration tests
just test-syntax       # Syntax parser tests (cleans + rebuilds first)
just test-all-roslyn   # Full matrix with Roslyn backend
```
