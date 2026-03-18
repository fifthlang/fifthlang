---
inclusion: fileMatch
fileMatchPattern: "test/**"
---

# Testing Standards

## Framework and Tools

- xUnit for test framework
- FluentAssertions for assertions
- Test projects: `test/ast-tests/`, `test/syntax-parser-tests/`, `test/runtime-integration-tests/`

## Test-First (Non-Negotiable)

Practice TDD: write tests, see them fail, then implement. Never mask failing tests with broad try/catch. Let failures surface so CI correctly reflects state.

## Compilation is NOT Sufficient

A feature is NOT complete until proven to work end-to-end with passing tests that:
1. Use actual Fifth language syntax (TriG literals, SPARQL literals, operators)
2. Execute successfully at runtime (not just compile)
3. Validate results are accessible and correct
4. Exercise all major code paths and result types

Features with only compilation tests or failing runtime tests are INCOMPLETE.

## Test Design Principles

- Prefer property-based testing over single-point scenarios
- Aim to verify all corner cases
- Avoid testing internal implementation details
- Avoid embedding dependencies on concrete implementations where possible
- Tests referencing `.5th` samples need `CopyToOutputDirectory` metadata in the test `.csproj`

## Running Tests

```bash
# Full regression gate (default)
dotnet test fifthlang.sln

# Quick smoke while iterating
dotnet test test/ast-tests/ast_tests.csproj

# Parser-focused
dotnet test test/syntax-parser-tests/ -v minimal

# Filtered runtime tests
dotnet test test/runtime-integration-tests/runtime-integration-tests.csproj --filter "FullyQualifiedName~YourTestName" -v minimal

# Knowledge graph tests
dotnet test test/kg-smoke-tests/kg-smoke-tests.csproj
```

## AST Smoke Test

Quick validation after AST builder changes:
```csharp
using ast;
using ast_generated;

var intLiteral = new Int32LiteralExp { Value = 42 };
var builder = new Int32LiteralExpBuilder();
var result = builder.Build();
// Should complete without errors
```
