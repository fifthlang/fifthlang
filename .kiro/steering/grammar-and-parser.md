---
description: grammar-and-parser
inclusion: always
---
## Architecture
- GRAM-001 [MANDATORY]: Keep syntax responsibilities split across lexer, parser, and AST builder. To comply, define tokens in `FifthLexer.g4`, syntax rules in `FifthParser.g4`, and parse-tree mapping in `AstBuilderVisitor.cs`.
## Workflow
- GRAM-002 [MANDATORY]: Any grammar change must follow the full grammar-update workflow. To comply, update grammar files and `AstBuilderVisitor.cs`, add samples, run parser tests, then run `dotnet test fifthlang.sln`.
## Validation
- GRAM-003 [MANDATORY]: All non-negative `.5th` samples in docs and tests must parse with the current grammar. To comply, run `just validate-examples` before commit.
- GRAM-008: Exclude intentional negative tests from normal example-validation checks. To comply, keep them in `*/Invalid/*`, include `invalid` in filename, or use explicit negative-test markers; use `--include-negatives` only for debugging.
## Syntax
- GRAM-004 [MANDATORY]: Do not use C# style `var <name> =` in Fifth examples or tests. To comply, use canonical declarations such as `name: type = value`.
## Guards
- GRAM-007: Use only parameter-constraint guard syntax. To comply, follow this contrast:

```fifth
// INVALID
myprint(int x) when x == 0 => std.print(x);

// VALID
myprint(int x | x == 0) { std.print(x); }
```
