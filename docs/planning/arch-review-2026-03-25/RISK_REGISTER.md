# Risk Register
**Date:** 2026-03-25 | **Repository:** aabs/fifthlang

Legend — Likelihood: **H** High / **M** Medium / **L** Low | Impact: **H** High / **M** Medium / **L** Low

---

## Risk Table

| ID | Title | Likelihood | Impact | Score | Category | Evidence | Mitigation |
|----|-------|-----------|--------|-------|----------|----------|-----------|
| R1 | **Orphaned `.postpone` file re-activation** — A contributor could attempt to re-enable `FifthParserManager.cs.postpone`, which references non-existent `il_ast` namespace, breaking the build | L | M | L-M | Correctness | `src/compiler/FifthParserManager.cs.postpone:1-8` | Delete the file; add a MIGRATION.md note |
| R2 | **SPARQL diagnostics silently discarded** — `SparqlVariableBindingVisitor.Diagnostic` is a separate class from `compiler.Diagnostic`; its output is never added to the main diagnostic list | H | H | H | Correctness | `SparqlVariableBindingVisitor.cs:270`; no wiring to `List<compiler.Diagnostic>` | AS-001 remediation |
| R3 | **Thread-safety of `TranslatorRegistry`** — parallel test execution or future parallel compilation would corrupt the static `Current` property | M | H | M-H | Correctness/Reliability | `src/compiler/TranslatorRegistry.cs` | AS-003 remediation: inject translator via constructor |
| R4 | **Constitution/governance divergence** — `ILMetamodel.cs`, `il.builders.generated.cs`, `il.rewriter.generated.cs`, and `scripts/validate-examples.fish` are referenced in the constitution but do not exist; ANTLR runtime version mismatch (4.8 stated, 4.13.1 used) | H | M | H-M | Governance | `.specify/memory/constitution.md`; directory listings | Update constitution (CONFORMANCE items I-3, I-5, IX-2, V-5) |
| R5 | **Incomplete SPARQL query execution** — `Store.cs:159` throws `NotImplementedException` for SPARQL query execution; any code path triggering this at runtime will crash | M | H | M-H | Correctness | `src/fifthlang.system/Store.cs:159` | Implement or gate with a feature flag and clear error message; add test |
| R6 | **`NullSafeRecursiveDescentVisitor` masking bugs** — silent null suppression in `TreeLinkageVisitor`, `TripleDiagnosticsVisitor`, `TryCatchFinallyValidationVisitor`, `LambdaValidationVisitor` means visitor bugs may produce silently corrupted ASTs | M | H | M-H | Correctness | `NullSafeRecursiveDescentVisitor.cs:18-35` | AS-002 remediation |
| R7 | **Broad exception catch in `Compiler.BuildAsync`** — `catch (System.Exception)` at line 124 converts all unhandled exceptions (including `StackOverflowException` predecessors and programming errors) into `Failed` results, obscuring bugs | M | M | M | Debuggability | `src/compiler/Compiler.cs:124,165,193` | Narrow to specific exception types; let programming errors propagate |
| R8 | **`TypeAnnotationVisitor` (942 lines) complexity** — a single visitor performing type inference, method dispatch, KG annotation, and generic resolution creates a high-risk change surface | M | M | M | Maintainability | `LanguageTransformations/TypeAnnotationVisitor.cs` | AS-006: decompose into focused passes |
| R9 | **`LoweredAstToRoslynTranslator` (2 712 lines) complexity** — single class handles all AST-to-C# translation including closures, generics, overloads, KG constructs, and PE emission wiring | M | M | M | Maintainability | `LoweredToRoslyn/LoweredAstToRoslynTranslator.cs` | Decompose into partial classes or sub-translators by feature domain |
| R10 | **Repeated symbol table + var-ref + tree-link rebuilds** — 3-4 invocations each of `SymbolTableBuilderVisitor`, `VarRefResolverVisitor`, `TreeLinkageVisitor` in the pipeline; every change to the transformation order risks introducing a pass that uses a stale symbol table | M | M | M | Correctness/Perf | `ParserManager.cs:110,138,292,309,339-341` | Document which passes invalidate what; reduce redundant rebuilds |
| R11 | **Missing PDB sequence-point test** — constitution §VII requires Roslyn PDBs with full line/column for all statements; no acceptance test verifies this | M | M | M | Quality | `ci.yml`; `LoweredAstToRoslynTranslator.cs` | Add a test that emits a `.5th` program and validates PDB sequence points |
| R12 | **No coverage gate in CI** — code coverage is collected but no threshold is enforced; regressions in test coverage will not fail CI | M | L | L-M | Quality | `ci.yml` (no threshold step) | Add minimum coverage threshold (e.g., 60%) |
| R13 | **OmniSharp LSP dependency version** — `OmniSharp.Extensions.LanguageServer 0.19.9` may be significantly behind current releases; LSP protocol changes could affect correctness | L | M | L-M | Compatibility | `src/language-server/Fifth.LanguageServer.csproj` | Evaluate upgrade path |
| R14 | **`System.CommandLine` beta dependency** — `System.CommandLine 2.0.0-beta4.22272.1` is used in both `ast_generator` and `compiler`; beta APIs can change without notice | L | L | L | Stability | `compiler.csproj`, `ast_generator.csproj` | Upgrade to stable release when available |
| R15 | **`GraphNamespaceAlias` design uncertainty** — `AstMetamodel.cs:597` has a `TODO: is this a reference or something similar?` on a type that appears in the KG pipeline; incorrect AST placement could cause incorrect code generation | M | M | M | Correctness | `AstMetamodel.cs:597` | Clarify design; add a test exercising `GraphNamespaceAlias` through the full pipeline |

---

## Risk Heat Map

```
         Impact
         L    M    H
         ┌────┬────┬────┐
High   L │    │ R4 │ R2 │
         ├────┼────┼────┤
Medium M │    │R8,9│R3,5│
         │    │R10 │R6  │
         ├────┼────┼────┤
Low    L │R14 │R1,13│    │
         └────┴────┴────┘
```

---

## Top Risks by Priority

| Rank | Risk ID | One-liner |
|------|---------|-----------|
| 1 | R2 | SPARQL diagnostics silently discarded |
| 2 | R3 | TranslatorRegistry thread-safety |
| 3 | R6 | NullSafe visitor masking bugs |
| 4 | R5 | NotImplementedException in SPARQL store |
| 5 | R4 | Governance/constitution divergence |
