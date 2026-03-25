# Executive Summary — Deep Architectural Review
**Date:** 2026-03-25  
**Scope:** All source code under `src/`, `test/`, `.github/`, and governance artifacts under `.specify/`  
**Excluded (per mandate):** `docs/`, `website/`

---

## 1  What the System Is

Fifth Language Engine is a C# / .NET 10 compiler for a custom functional-ish language (Fifth) that targets .NET via a Roslyn-backed emission pipeline. The compiler implements a 33-phase sequential transformation pipeline, an ANTLR 4-based split-grammar parser, a metamodel-driven AST generator, and an LSP language server. The system is governed by a constitution document at `.specify/memory/constitution.md`, a set of completed speckit feature specs under `specs/`, and a CI workflow under `.github/workflows/ci.yml`.

---

## 2  Top 5 Risks

| # | Risk | Severity | Evidence |
|---|------|----------|----------|
| R1 | **Orphaned `FifthParserManager.cs.postpone`** – a 219-line file that references `il_ast` namespace and legacy pass ordering is sitting in `src/compiler/` and is tracked by git. Its presence implies an incomplete migration and its `il_ast` namespace is now absent from the solution, meaning a developer could try to re-enable it and break the build silently. | Medium | `src/compiler/FifthParserManager.cs.postpone` lines 1-30 |
| R2 | **Three distinct `Diagnostic` types in the same namespace** – `compiler.Diagnostic` (CompilationResult.cs), `compiler.DiagnosticRecord` (same file), and `SparqlVariableBindingVisitor+Diagnostic` (SparqlVariableBindingVisitor.cs:270) shadow each other, creating ambiguity for diagnostic routing and potentially causing SPARQL diagnostics to be silently discarded. | High | `src/compiler/CompilationResult.cs:22`, `src/compiler/LanguageTransformations/SparqlVariableBindingVisitor.cs:270` |
| R3 | **`TranslatorRegistry` static global state** – `TranslatorRegistry.Current` is a mutable static field. This is non-thread-safe, non-testable, and creates invisible coupling between the compiler driver and the backend. Any concurrent test or parallel compilation scenario can corrupt this silently. | High | `src/compiler/TranslatorRegistry.cs` |
| R4 | **Constitution references `ILMetamodel.cs` which does not exist** – the constitution (§I, §VII, §VIII, build-order step 5) documents an IL-level AST and `ILMetamodel.cs`/`il.builders.generated.cs`/`il.rewriter.generated.cs` as first-class artifacts, but none of these files exist in the repository. The `ast-generated/` directory contains no IL builder output. This is a governance/reality mismatch that misleads contributors. | Medium | `.specify/memory/constitution.md` lines 6, 223-225, 311-312; `src/ast-generated/` listing |
| R5 | **`NullSafeRecursiveDescentVisitor` is a masking workaround** – the class exists solely to guard against null returns from the generated visitor, which should be structurally impossible if invariants are maintained. It is used by `TreeLinkageVisitor`, `TripleDiagnosticsVisitor`, `TryCatchFinallyValidationVisitor`, and `LambdaValidationVisitor`. The class silently swallows null returns, violating the constitution's explicit prohibition on masking defects (§VI). | Medium | `src/compiler/LanguageTransformations/NullSafeRecursiveDescentVisitor.cs` |

---

## 3  Top 5 Cleanup Wins

| # | Win | Effort | Benefit |
|---|-----|--------|---------|
| W1 | **Delete `FifthParserManager.cs.postpone`** and add a git note documenting the migration. Removes dead legacy pass ordering, removes the `il_ast` reference confusion. | 5 min | Clarity, no future confusion |
| W2 | **Consolidate the three `Diagnostic` types** into a single `compiler.Diagnostic` record and plumb it through `SparqlVariableBindingVisitor`. | 1-2 h | Correctness, maintainability |
| W3 | **Remove `TranslatorRegistry` static state** and inject `IBackendTranslator` through the `Compiler` constructor. | 1-2 h | Testability, thread-safety |
| W4 | **Update the constitution** to remove all references to `ILMetamodel.cs`, `il.builders.generated.cs`, `il.rewriter.generated.cs`, and the legacy IL pipeline, replacing with accurate Roslyn-only descriptions. | 1 h | Governance conformance, contributor trust |
| W5 | **Deduplicate `SymbolTableBuilderVisitor` + `VarRefResolverVisitor` re-runs** – these are invoked 3–4 times each in the 33-phase pipeline (phases 5, 23, 25, and again inside the TypeAnnotation block). Document the reason or reduce the repetition. | 2-4 h | Pipeline clarity, faster compilations |

---

## 4  Top 3 Structural Refactors

| # | Refactor | Rationale |
|---|----------|-----------|
| S1 | **Formalise pipeline phase registration** – replace the monolithic `ApplyLanguageAnalysisPhases` method in `ParserManager.cs` (33 sequential `if (upTo >= AnalysisPhase.X)` blocks totalling ~450 lines) with a data-driven pipeline registry where each phase registers preconditions, name, and implementation. Enables safe phase-ordering changes, better telemetry, and partial compilation for LSP. | Maintainability, extensibility, debuggability |
| S2 | **Unify the type system** – `FifthType`, `TypeRef`, `TypeAlias`, and `TypeRegistry` in `ast-model/TypeSystem/` form a partially-integrated type system that is still evolving (several `TODO` comments). A formal type resolution contract with clear ownership between parse, annotation, and Roslyn emission phases would prevent the current proliferation of ad-hoc type inference visitors. | Correctness, feature velocity |
| S3 | **Isolate SPARQL/TriG concerns into a subsystem** – there are currently 10+ transformation passes dedicated to SPARQL/TriG/triple/graph operations scattered across `LanguageTransformations/`. Grouping them into a coherent `KnowledgeGraph/` sub-pipeline with documented invariants and a clear entry/exit boundary would improve testability and reduce the risk of ordering bugs. | Separation of concerns, testability |

---

## 5  Most Critical Constitution/Spec Mismatches to Resolve First

| Priority | Mismatch | Resolution Direction |
|----------|----------|---------------------|
| 1 | Constitution documents `ILMetamodel.cs` and `il.*` generated files that no longer exist → **Update constitution** to reflect Roslyn-only pipeline | Code wins; update governance |
| 2 | Constitution §IX mandates a `scripts/validate-examples.fish` script for local use; the actual validator is a C# tool at `src/tools/validate-examples/` → **Update constitution** to document the C# tool and add a `just validate-examples` recipe | Update governance + add just recipe |
| 3 | Constitution §IV mandates `test/syntax-parser-tests/` as a test project; this project exists in the sln but there are no corresponding spec acceptance criteria for most syntax features in the completed specs → **Ensure completed specs have parser test coverage references** | Spec update + test additions |
| 4 | The Roslyn migration note in §VII says the legacy pipeline "remains available behind a feature flag" but no such feature flag exists in the current codebase → **Clarify migration status** in constitution | Update governance |

---

*Full details are in the companion documents: ARCHITECTURE_OVERVIEW.md, CONFORMANCE_CHECKLIST.md, AI_SLOP_REGISTER.md, RISK_REGISTER.md, REMEDIATION_ROADMAP.md, DIAGRAMS.md.*
