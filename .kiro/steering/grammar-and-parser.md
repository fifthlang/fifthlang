---
inclusion: fileMatch
fileMatchPattern: "src/parser/**,docs/**/*.5th,test/**/*.5th,src/parser/grammar/**"
---

# Grammar and Parser Rules

## Split Grammar Architecture

- `src/parser/grammar/FifthLexer.g4` — Tokens, keywords, literals, lexical structure
- `src/parser/grammar/FifthParser.g4` — Syntactic rules and grammar structure
- `src/parser/AstBuilderVisitor.cs` — Parse tree to high-level AST transformation

## Grammar Change Workflow

1. Edit `FifthLexer.g4` (tokens/keywords) and/or `FifthParser.g4` (syntax rules)
2. Update `AstBuilderVisitor.cs` for new syntax constructs
3. Add test samples in `src/parser/grammar/test_samples/*.5th`
4. ANTLR compilation happens automatically during build
5. Run parser tests: `dotnet test test/syntax-parser-tests/ -v minimal`
6. Run full regression: `dotnet test fifthlang.sln`

## Grammar Compliance for Examples and Tests

All `.5th` files in `docs/`, `specs/`, `test/`, and `src/parser/grammar/test_samples/` MUST parse with the current grammar. CI enforces this via the "Validate .5th samples (parser-check)" step.

Run locally before committing: `just validate-examples`

## Common Non-Fifth Patterns to Avoid

- `var <name> =` (C#/JS-style) → use `name: type =` or appropriate Fifth form
- `graph g =` or `triple t =` (type-first) → use `g: graph =` or `t: triple =`
- `when` guard shorthand (legacy) → use parameter constraint form `param: Type | <expr>` with block bodies

## Canonical Guard Syntax

```fifth
// INVALID (legacy shorthand)
myprint(int x) when x == 0 => std.print(x);

// VALID (grammar-compliant parameter constraint)
myprint(int x | x == 0) { std.print(x); }
```

## Negative Tests

Intentionally-invalid files are excluded from validation via:
- Directory heuristic: files under `*/Invalid/*`
- Filename heuristic: files with `invalid` in the name
- Content marker: explicit negative-test comment in the file

Force-validate negatives for debugging: `dotnet run --project src/tools/validate-examples/validate-examples.csproj -- --include-negatives`

## Knowledge Graph Syntax

- Store declarations: `name: store = sparql_store(<iri>);` or `store default = sparql_store(<iri>);`
- Graph operations: `KG.CreateGraph()` to create, `graph += triple` to add triples
- Validate: `dotnet test test/kg-smoke-tests/kg-smoke-tests.csproj`
