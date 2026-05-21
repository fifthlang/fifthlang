---
id: steering-testing-standards
title: Testing Standards
inclusion: fileMatch
fileMatchPattern: "test/**"
---

:::rule id="FTR-01" category="completion" 
A feature is not complete until end-to-end tests prove that it:

1. Uses actual Fifth language syntax including constructs such as TriG literals, SPARQL literals, and operators
2. Executes successfully at runtime rather than merely compiling
3. Produces results that are accessible and correct
4. Exercises the major code paths and result types involved

Features with only compilation tests or with failing runtime tests are incomplete.
:::

:::rule id="FTR-02" mandatory="false" category="commands" 
The default regression command is:

```bash
dotnet test fifthlang.sln
```
:::

:::rule id="FTR-03" mandatory="false" category="commands" 
Use this quick smoke command while iterating:

```bash
dotnet test test/ast-tests/ast_tests.csproj
```
:::

:::rule id="FTR-04" mandatory="false" category="commands" 
Use this focused parser command when grammar behavior changes:

```bash
dotnet test test/syntax-parser-tests/ -v minimal
```
:::

:::rule id="FTR-05" mandatory="false" category="commands" 
Use filtered runtime integration tests for focused investigation:

```bash
dotnet test test/runtime-integration-tests/runtime-integration-tests.csproj --filter "FullyQualifiedName~YourTestName" -v minimal
```
:::

:::rule id="FTR-06" mandatory="false" category="commands" 
Validate knowledge-graph changes with:

```bash
dotnet test test/kg-smoke-tests/kg-smoke-tests.csproj
```
:::

## AST Smoke Test

:::rule id="FTR-07" mandatory="false" category="ast" 
Use this quick smoke test after AST builder changes:

```csharp
using ast;
using ast_generated;

var intLiteral = new Int32LiteralExp { Value = 42 };
var builder = new Int32LiteralExpBuilder();
var result = builder.Build();
```

The builder construction should complete without errors.
:::
