---
description: testing-standards
inclusion: always
---
## Completion
- FTR-01: A feature is not complete until end-to-end tests prove that it:

1. Uses actual Fifth language syntax including constructs such as TriG literals, SPARQL literals, and operators
2. Executes successfully at runtime rather than merely compiling
3. Produces results that are accessible and correct
4. Exercises the major code paths and result types involved

Features with only compilation tests or with failing runtime tests are incomplete.
## Commands
- FTR-02: The default regression command is:

```bash
dotnet test fifthlang.sln
```
- FTR-03: Use this quick smoke command while iterating:

```bash
dotnet test test/ast-tests/ast_tests.csproj
```
- FTR-04: Use this focused parser command when grammar behavior changes:

```bash
dotnet test test/syntax-parser-tests/ -v minimal
```
- FTR-05: Use filtered runtime integration tests for focused investigation:

```bash
dotnet test test/runtime-integration-tests/runtime-integration-tests.csproj --filter "FullyQualifiedName~YourTestName" -v minimal
```
- FTR-06: Validate knowledge-graph changes with:

```bash
dotnet test test/kg-smoke-tests/kg-smoke-tests.csproj
```
## Ast
- FTR-07: Use this quick smoke test after AST builder changes:

```csharp
using ast;
using ast_generated;

var intLiteral = new Int32LiteralExp { Value = 42 };
var builder = new Int32LiteralExpBuilder();
var result = builder.Build();
```

The builder construction should complete without errors.
