# Constitution & Spec Conformance Checklist
**Date:** 2026-03-25 | **Source:** `.specify/memory/constitution.md`

Legend: ✅ **Conforms** | ⚠️ **Partial** | ❌ **Violates** | ❓ **Unknown**

---

## Phase 0 Derived Constraints

The following checklist items were derived from the constitution. Each item is testable/verifiable from the repository.

---

## Section I — Library-First, Contracts-First

| # | Checklist Item | Status | Evidence | Impact / Fix |
|---|---------------|--------|----------|--------------|
| I-1 | Every library under `src/` has a clear, documented purpose | ⚠️ Partial | `Fifth.Sdk` and `src/tools/` subdirectories lack README or summary doc comments on their entry types | Low; add README stubs |
| I-2 | AST metamodel is the authoritative source in `src/ast-model/AstMetamodel.cs` | ✅ Conforms | `AstMetamodel.cs` (1 258 lines) is the sole source for AST types; `ast-generated/` has no hand edits | — |
| I-3 | `ILMetamodel.cs` exists in `src/ast-model/` | ❌ Violates | File does not exist. Constitution mentions it at lines 6, 223-225, 311-312. The IL pipeline was removed without updating governance. | Update constitution to remove IL metamodel references |
| I-4 | Generated builders/visitors are in `src/ast-generated/` | ✅ Conforms | `builders.generated.cs`, `visitors.generated.cs`, `rewriter.generated.cs`, `typeinference.generated.cs` all present | — |
| I-5 | `il.builders.generated.cs` and `il.rewriter.generated.cs` exist in `src/ast-generated/` | ❌ Violates | Neither file exists. Constitution lists them at lines 224-225. | Update constitution; these are relics of removed IL pipeline |
| I-6 | Grammar is split into `FifthLexer.g4` + `FifthParser.g4` | ✅ Conforms | Both files exist at `src/parser/grammar/` | — |
| I-7 | Public CLIs use stdin/stdout/stderr text I/O | ✅ Conforms | `compiler` CLI (`Program.cs`) and `ast_generator` CLI (`Program.cs`) both use stdout/stderr | — |

---

## Section II — CLI and Text I/O Discipline

| # | Checklist Item | Status | Evidence | Impact / Fix |
|---|---------------|--------|----------|--------------|
| II-1 | Compiler emits diagnostics to stderr | ✅ Conforms | `Compiler.cs` passes `List<Diagnostic>` through all phases; `Program.cs` writes errors to stderr | — |
| II-2 | Output is deterministic and scriptable | ⚠️ Partial | `TranslatorRegistry.Current` is mutable static state; concurrent calls may produce non-deterministic output. `Compiler.cs:124` catches `System.Exception` broadly | Fix TranslatorRegistry; scope diagnostics per-compilation |
| II-3 | `FIFTH_DEBUG` env var enables verbose diagnostics | ✅ Conforms | `src/compiler/DebugLog.cs` — `DebugEnabled` reads `FIFTH_DEBUG` env var | — |

---

## Section III — Generator as Source of Truth

| # | Checklist Item | Status | Evidence | Impact / Fix |
|---|---------------|--------|----------|--------------|
| III-1 | No hand-edited files in `src/ast-generated/` | ✅ Conforms | All `.cs` files in `src/ast-generated/` are generated (confirmed by `.generated.cs` naming and generator tooling) | — |
| III-2 | Generator reads from `AstMetamodel.cs` and `ILMetamodel.cs` | ⚠️ Partial | Generator reads `AstMetamodel.cs` ✅. `ILMetamodel.cs` does not exist; constitution reference is stale | Update constitution |
| III-3 | `just run-generator` regenerates all generated files | ✅ Conforms | `Justfile` contains `run-generator` recipe | — |

---

## Section IV — Test-First

| # | Checklist Item | Status | Evidence | Impact / Fix |
|---|---------------|--------|----------|--------------|
| IV-1 | TUnit is used across all test projects | ✅ Conforms | All test projects reference TUnit; FluentAssertions used for assertions | — |
| IV-2 | `test/ast-tests/` covers AST and generator correctness | ✅ Conforms | 20+ test files in `test/ast-tests/` | — |
| IV-3 | `test/syntax-parser-tests/` covers grammar parsing | ✅ Conforms | Project exists and is in solution | — |
| IV-4 | `test/runtime-integration-tests/` provides end-to-end tests | ✅ Conforms | 20+ end-to-end test files present | — |
| IV-5 | No catch-all try/catch masking failures | ❌ Violates | `Compiler.cs:124` has `catch (System.Exception ex)` in `BuildAsync`; similarly in `RunAsync:165`, `LintAsync:193`. These are broad catches that convert exceptions to `Failed` results. `NullSafeRecursiveDescentVisitor` silently ignores null visitor returns (lines 18-35). | Narrow exception catches; remove null masking in NullSafeRecursiveDescentVisitor |
| IV-6 | Features proven end-to-end with Fifth syntax (not just C# interop) | ⚠️ Partial | Most runtime tests use Fifth code. `Store.cs:159` has `throw new NotImplementedException` for SPARQL query execution, indicating an incomplete feature | Complete SPARQL query execution or document as incomplete |
| IV-7 | All major code paths exercised by tests | ❓ Unknown | No code coverage threshold is enforced in CI (`ci.yml` collects but does not gate on coverage) | Add coverage gate |

---

## Section V — Reproducible Builds

| # | Checklist Item | Status | Evidence | Impact / Fix |
|---|---------------|--------|----------|--------------|
| V-1 | `.NET SDK 10.0.100` pinned in `global.json` | ✅ Conforms | `global.json` pins `10.0.100` | — |
| V-2 | Java 17+ required for ANTLR | ✅ Conforms | CI step `Setup Java 17` in `ci.yml`; ANTLR jar at `src/parser/tools/antlr-4.8-complete.jar` | — |
| V-3 | Build order: ast-model → ast_generator → ast-generated → parser → compiler → tests | ✅ Conforms | `.csproj` ProjectReference chains enforce this order | — |
| V-4 | NuGet lock files enabled | ✅ Conforms | `Directory.Build.props` sets `RestorePackagesWithLockFile=true`; lock files present in each project | — |
| V-5 | ANTLR runtime version matches constitution (4.8) | ⚠️ Partial | Constitution says "ANTLR 4.8 runtime" but `parser.csproj` uses `Antlr4.Runtime.Standard 4.13.1`. The ANTLR tool jar is `antlr-4.8-complete.jar`. This version mismatch (tool vs runtime) could cause subtle incompatibilities. | Update constitution to reflect actual runtime version 4.13.1 |

---

## Section VI — Simplicity, Minimal Surface, Safety

| # | Checklist Item | Status | Evidence | Impact / Fix |
|---|---------------|--------|----------|--------------|
| VI-1 | No non-required abstractions (YAGNI) | ⚠️ Partial | `NullSafeRecursiveDescentVisitor` is an abstraction added to work around a deeper structural issue. `TranslatorRegistry` is a "lightweight registry" that adds indirection without benefit. `DiagnosticRecord` duplicates `Diagnostic` in the same file. | See AI Slop Register |
| VI-2 | No catch-all error handling hiding defects | ❌ Violates | `Compiler.cs:124,165,193`; `NullSafeRecursiveDescentVisitor` silent null masking | Fix per IV-5 |
| VI-3 | Changes increasing complexity justified in PR | ❓ Unknown | No enforced PR template requiring complexity justification | Add PR template |

---

## Section VII — Multi-Pass Compilation & AST Lowering

| # | Checklist Item | Status | Evidence | Impact / Fix |
|---|---------------|--------|----------|--------------|
| VII-1 | Clean phase separation through named passes | ✅ Conforms | `AnalysisPhase` enum documents 33 phases | — |
| VII-2 | Legacy IL pipeline `remains available behind a feature flag` | ❌ Violates | No feature flag exists. `FifthParserManager.cs.postpone` is inert. The `il_ast` namespace is absent. The legacy pipeline is fully removed, not flagged. | Update constitution to state migration is complete |
| VII-3 | Roslyn emits PDBs with line/column sequence points | ⚠️ Partial | `TranslatorOptions.EmitDebugInfo=true` by default. Full line-column coverage for all statements is stated as a goal but cannot be verified without running the compiler; `MappingTable` exists but is not validated in tests | Add PDB sequence-point acceptance test |
| VII-4 | Phase ordering documented with preconditions | ⚠️ Partial | `AnalysisPhase` enum provides ordering. Preconditions/postconditions are only partially documented in code comments. | Document inter-phase preconditions |

---

## Section VIII — AST Design & Transformation Strategy

| # | Checklist Item | Status | Evidence | Impact / Fix |
|---|---------------|--------|----------|--------------|
| VIII-1 | Syntactic sugar lowered in AST transformations before Roslyn | ✅ Conforms | All 33 phases run before `LoweredAstToRoslynTranslator` | — |
| VIII-2 | IL AST is simple and close to IL instructions | ❓ Unknown | `ILMetamodel.cs` does not exist; no IL AST | Update constitution |
| VIII-3 | Each transformation visitor has single responsibility | ⚠️ Partial | Most passes are focused. However `TypeAnnotationVisitor.cs` (942 lines) and `TripleGraphAdditionLoweringRewriter.cs` (946 lines) are candidates for decomposition | Refactor over-large passes |
| VIII-4 | `DefaultAstRewriter` preferred for new lowering passes | ✅ Conforms | `DestructuringLoweringRewriter`, `SparqlLiteralLoweringRewriter`, `TriGLiteralLoweringRewriter`, etc. all use `DefaultAstRewriter` | — |

---

## Section IX — Parser & Grammar Integrity

| # | Checklist Item | Status | Evidence | Impact / Fix |
|---|---------------|--------|----------|--------------|
| IX-1 | All `.5th` samples in `test/`, `docs/`, `specs/` parse with current grammar | ✅ Conforms | CI `Validate .5th samples (parser-check)` step runs on every push/PR | — |
| IX-2 | `scripts/validate-examples.fish` exists for local validation | ❌ Violates | File does not exist. The actual tool is `src/tools/validate-examples/` (a C# dotnet tool). Constitution §IX.Enforcement references a non-existent fish script. | Update constitution to document the C# validator; add `just validate-examples` recipe |
| IX-3 | Grammar changes include test samples in `test_samples/*.5th` | ✅ Conforms | `src/parser/grammar/test_samples/` contains `.5th` files | — |

---

## Section X — Observability & Diagnostics

| # | Checklist Item | Status | Evidence | Impact / Fix |
|---|---------------|--------|----------|--------------|
| X-1 | Diagnostics include file path, line, column | ⚠️ Partial | `compiler.Diagnostic` record supports `Line?`, `Column?`, `Source?` fields. Not all phases populate these fields (e.g., most `LanguageTransformations` passes add errors without location). | Populate location in all diagnostic emissions |
| X-2 | No custom logging framework; uses .NET standard logging | ⚠️ Partial | `DebugLog` uses `Console.Error.WriteLine`; `ParserManager.cs` uses `Console.Error.WriteLine` directly for phase failure dumps. `language-server` uses `Microsoft.Extensions.Logging.Console`. | Normalise to `ILogger<T>` or structured `Console.Error` |
| X-3 | Diagnostic codes exist for stable reference | ⚠️ Partial | Some diagnostics use codes (e.g., `TRPL001` mentioned in `CompilationResult.cs` comment); many do not. `SparqlVariableBindingVisitor.Diagnostic.Code` field exists but the global `compiler.Diagnostic.Code` field is `null` in most emissions. | Assign stable codes to all diagnostic categories |

---

## Section XI — Versioning & Backward Compatibility

| # | Checklist Item | Status | Evidence | Impact / Fix |
|---|---------------|--------|----------|--------------|
| XI-1 | Semantic versioning used | ❓ Unknown | `Directory.Build.props` or `*.csproj` version strings not inspected in detail. | Verify version scheme |
| XI-2 | Generated code follows metamodel versioning | ✅ Conforms | `ast-generated/` files are regenerated from metamodel on every relevant change | — |

---

## Summary Statistics

| Status | Count |
|--------|-------|
| ✅ Conforms | 19 |
| ⚠️ Partial | 14 |
| ❌ Violates | 6 |
| ❓ Unknown | 4 |
| **Total** | **43** |
