# Remediation Roadmap
**Date:** 2026-03-25 | **Repository:** aabs/fifthlang  
**Format:** Three-horizon (Now / Next / Later) + spec-driven backlog items

---

## Horizon 1 — Now (Low-risk, high ROI; can start immediately)

These items have minimal risk of breaking existing behaviour and deliver immediate clarity or correctness improvements.

---

### REM-001: Delete `FifthParserManager.cs.postpone`
- **Objective:** Remove the 219-line legacy file that references non-existent `il_ast` namespace and confuses contributors.
- **Rationale:** The file is inert (`.postpone` extension), git-tracked, and has no path back to compilation. Keeping it misleads contributors about the existence of an IL pipeline.
- **Acceptance Criteria:**
  - [x] `src/compiler/FifthParserManager.cs.postpone` deleted from repository.
  - [x] Solution builds cleanly (`dotnet build fifthlang.sln`).
  - [x] All tests pass (`dotnet test fifthlang.sln`).
- **Impacted Files:** `src/compiler/FifthParserManager.cs.postpone`
- **Required Tests:** None (no code change).
- **Risk:** None.

---

### REM-002: Update Constitution — Remove IL Pipeline References
- **Objective:** Remove all references to `ILMetamodel.cs`, `il.builders.generated.cs`, `il.rewriter.generated.cs`, and the "legacy IL pipeline behind a feature flag" from the constitution.
- **Rationale:** These artifacts do not exist. The constitution is the authoritative governance document; inaccuracies undermine contributor trust and cause conformance failures.
- **Acceptance Criteria:**
  - [x] `.specify/memory/constitution.md` §I, §VII, §VIII, build-order section updated to reflect Roslyn-only pipeline.
  - [x] All references to `ILMetamodel.cs`, `il.builders.generated.cs`, `il.rewriter.generated.cs` removed.
  - [x] Roslyn migration status updated from "in progress with feature flag" to "complete; legacy IL pipeline removed."
  - [x] ANTLR runtime version updated from `4.8` to `4.13.1` in constitution §V.
- **Impacted Files:** `.specify/memory/constitution.md`
- **Required Tests:** None (documentation change).
- **Risk:** None.

---

### REM-003: Update Constitution — Document `validate-examples` C# Tool
- **Objective:** Replace references to `scripts/validate-examples.fish` with the actual C# tool at `src/tools/validate-examples/`.
- **Rationale:** The fish script does not exist; the actual tool is `src/tools/validate-examples/validate-examples.csproj`.
- **Acceptance Criteria:**
  - [x] `.specify/memory/constitution.md` §IX updated to document the C# tool.
  - [x] `Justfile` gains a `validate-examples` recipe: `dotnet run --project src/tools/validate-examples/validate-examples.csproj`.
- **Impacted Files:** `.specify/memory/constitution.md`, `Justfile`
- **Required Tests:** Run `just validate-examples` and confirm exit 0.
- **Risk:** None.

---

### REM-004: Fix `DiagnosticRecord` Duplication — Merge into `compiler.Diagnostic`
- **Objective:** Remove `compiler.DiagnosticRecord` which is structurally identical to `compiler.Diagnostic`.
- **Rationale:** Two identical record types in the same file create confusion. The comment says they are intentionally identical, which defeats the purpose of having two types.
- **Acceptance Criteria:**
  - [x] `DiagnosticRecord` removed from `CompilationResult.cs`.
  - [x] All usages of `DiagnosticRecord` replaced with `compiler.Diagnostic`.
  - [x] Solution builds and all tests pass.
- **Impacted Files:** `src/compiler/CompilationResult.cs`; search for `DiagnosticRecord` usages.
- **Required Tests:** Existing compilation result tests should pass unchanged.
- **Risk:** Low (rename only).

---

### REM-005: Route SPARQL Diagnostics into Main Pipeline
- **Objective:** Replace `SparqlVariableBindingVisitor.Diagnostic` local class with `compiler.Diagnostic` and route binding errors into the `List<compiler.Diagnostic>` parameter chain.
- **Rationale:** SPARQL variable binding errors are currently silently discarded (AS-001, R2).
- **Acceptance Criteria:**
  - [ ] Local `Diagnostic` and `DiagnosticSeverity` types removed from `SparqlVariableBindingVisitor.cs`.
  - [ ] `SparqlVariableBindingVisitor` constructor accepts a `List<compiler.Diagnostic>` and emits `compiler.Diagnostic` errors.
  - [ ] Compilation of a Fifth program with an invalid SPARQL variable binding produces a `DiagnosticLevel.Error` in the result.
  - [ ] New test: `SparqlVariableBindingTest_InvalidVariable_ProducesError` in `runtime-integration-tests` or `ast-tests`.
- **Impacted Files:** `src/compiler/LanguageTransformations/SparqlVariableBindingVisitor.cs`; `src/compiler/ParserManager.cs`
- **Required Tests:** New test as above.
- **Risk:** Low-medium (changes diagnostic routing; validate via existing SPARQL tests).

---

### REM-006: Convert `TranslatorRegistry` to Constructor Injection
- **Objective:** Remove the `TranslatorRegistry` static mutable state by injecting `IBackendTranslator` through the `Compiler` constructor.
- **Rationale:** Static mutable state prevents parallel testing, violates constitution §VI, and creates invisible coupling (AS-003, R3).
- **Acceptance Criteria:**
  - [ ] `Compiler` constructor gains an optional `IBackendTranslator? translator = null` parameter.
  - [ ] Default is `new LoweredAstToRoslynTranslator()`.
  - [ ] `TranslatorRegistry` class deleted.
  - [ ] All call sites updated (search for `TranslatorRegistry.Current`).
  - [ ] Parallel test execution does not corrupt translator state.
  - [ ] Solution builds and all tests pass.
- **Impacted Files:** `src/compiler/TranslatorRegistry.cs` (delete), `src/compiler/Compiler.cs`, any test that sets `TranslatorRegistry.Current`.
- **Required Tests:** Existing tests; optionally add a test that constructs two `Compiler` instances with different translators and verifies isolation.
- **Risk:** Low (interface already exists; default behaviour unchanged).

---

### REM-007: Clarify or Delete `ExpressionCloner` and `DumpTreeVisitor`
- **Objective:** Audit `ExpressionCloner.cs` and `DumpTreeVisitor.cs` — determine if they are used, document if so, or delete if dead.
- **Rationale:** Reducing dead code improves maintainability (AS-007).
- **Acceptance Criteria:**
  - [ ] Each file either has a documented usage or is deleted.
  - [ ] Solution builds and all tests pass.
- **Impacted Files:** `src/compiler/LanguageTransformations/ExpressionCloner.cs`, `src/compiler/LanguageTransformations/DumpTreeVisitor.cs`
- **Risk:** Low if deleted (no callers); verify with build.

---

## Horizon 2 — Next (Structural refactors; moderate risk; requires testing investment)

---

### REM-008: Formalise Pipeline Phase Registration
- **Objective:** Replace the monolithic `ApplyLanguageAnalysisPhases` method (33 sequential `if (upTo >= AnalysisPhase.X)` blocks) with a data-driven phase registry.
- **Rationale:** The current 450-line method is brittle: adding a phase requires editing the monolith and the `AnalysisPhase` enum; there is no way to query what passes ran or extract per-phase timing for LSP partial compilation (S1 from Executive Summary).
- **Design:**
  ```csharp
  record PipelinePhase(
      AnalysisPhase Phase,
      string Name,
      Func<AstThing, List<Diagnostic>, string?, AstThing> Execute,
      AnalysisPhase[]? Preconditions = null
  );
  // Pipeline registered as IReadOnlyList<PipelinePhase>
  ```
- **Acceptance Criteria:**
  - [ ] `ApplyLanguageAnalysisPhases` refactored to iterate over a registered list of `PipelinePhase` instances.
  - [ ] `AnalysisPhase` enum retained for backward compatibility.
  - [ ] Per-phase timing available via `FIFTH_DEBUG=1`.
  - [ ] All existing tests pass unchanged.
  - [ ] New test verifying phase ordering matches current `AnalysisPhase` enum.
- **Impacted Files:** `src/compiler/ParserManager.cs`
- **Risk:** Medium — changes the pipeline orchestration; validate with full test suite.

---

### REM-009: Decompose `TypeAnnotationVisitor`
- **Objective:** Split `TypeAnnotationVisitor.cs` (942 lines) into focused passes.
- **Rationale:** The current class handles standard type annotation, generic type resolution, and KG-specific annotation — three independent concerns (AS-006).
- **Proposed Split:**
  - `StandardTypeAnnotationVisitor` — basic type propagation
  - `GenericTypeAnnotationVisitor` — generic type argument resolution
  - `KnowledgeGraphTypeAnnotationVisitor` — KG/SPARQL type annotation
- **Acceptance Criteria:**
  - [ ] Three new focused visitors; `TypeAnnotationVisitor` retained as a thin facade that calls them in order (for backward compatibility with `AnalysisPhase.TypeAnnotation`).
  - [ ] Each new visitor tested independently.
  - [ ] All existing type-annotation tests pass.
- **Risk:** Medium — touches the type inference core.

---

### REM-010: Consolidate SPARQL/KG Passes into `KnowledgeGraph/` Sub-Pipeline
- **Objective:** Move all 10 SPARQL/TriG/triple/graph transformation passes into a `KnowledgeGraph/` subdirectory with a documented sub-pipeline entry point.
- **Rationale:** KG concerns are cross-cutting but scattered across `LanguageTransformations/` (S3 from Executive Summary).
- **Affected Passes:** `SparqlLiteralLoweringRewriter`, `TriGLiteralLoweringRewriter`, `TripleDiagnosticsVisitor`, `TripleExpansionVisitor`, `TripleGraphAdditionLoweringRewriter`, `QueryApplicationTypeCheckRewriter`, `QueryApplicationLoweringRewriter`, `SparqlComprehensionValidationVisitor`, `SparqlVariableBindingVisitor`, `SparqlInterpolationValidator`
- **Acceptance Criteria:**
  - [ ] All 10 passes moved to `src/compiler/KnowledgeGraph/` subdirectory.
  - [ ] `ParserManager.cs` updated to call `KnowledgeGraphPipeline.Apply(ast, diagnostics, targetFramework)` for phases 16-22, 26-27, and 29.
  - [ ] `kg-smoke-tests` all pass.
- **Risk:** Medium — refactoring only; no logic changes.

---

### REM-011: Fix `NullSafeRecursiveDescentVisitor` Root Cause
- **Objective:** Identify and fix the root cause of visitor null returns; delete `NullSafeRecursiveDescentVisitor`.
- **Rationale:** The workaround masks bugs, partially covers the AST, and violates constitution §VI (AS-002, R6).
- **Acceptance Criteria:**
  - [ ] Root cause identified (likely an unhandled `AstThing` subtype in `DefaultRecursiveDescentVisitor`).
  - [ ] Fix applied to generated visitor or specific derived visitors.
  - [ ] `NullSafeRecursiveDescentVisitor` deleted.
  - [ ] `TreeLinkageVisitor`, `TripleDiagnosticsVisitor`, `TryCatchFinallyValidationVisitor`, `LambdaValidationVisitor` extend `DefaultRecursiveDescentVisitor` directly.
  - [ ] All tests pass.
- **Risk:** Medium — touches tree linkage and diagnostic visitors.

---

### REM-012: Reduce Redundant Symbol Table Rebuilds
- **Objective:** Document and reduce the 3-4 invocations of `SymbolTableBuilderVisitor`, `VarRefResolverVisitor`, `TreeLinkageVisitor` in the pipeline.
- **Rationale:** Redundant O(n) traversals slow compilation; unclear ordering invariants risk correctness (AS-005, R10).
- **Acceptance Criteria:**
  - [ ] Each repeated invocation is either removed (if safe) or annotated with a comment explaining which preceding pass invalidated the symbol table/links.
  - [ ] Performance test baseline established; subsequent builds are no slower.
  - [ ] All tests pass.
- **Risk:** Medium — symbol table correctness is fundamental.

---

### REM-013: Implement or Formally Defer SPARQL Query Execution
- **Objective:** Address `Store.cs:159` `throw new NotImplementedException`.
- **Rationale:** Uncovered code path crashes at runtime (R5).
- **Acceptance Criteria (implement path):**
  - [ ] SPARQL query execution implemented and integrated with the Fifth type system.
  - [ ] End-to-end test in `kg-smoke-tests` executes a SPARQL SELECT against a SPARQL store and returns results.
- **Acceptance Criteria (defer path):**
  - [ ] Code path guarded with an explicit `FifthException` carrying a descriptive message and a feature-flag check.
  - [ ] A tracking issue created and linked in a code comment.
- **Risk:** Medium (implementation) / Low (deferral).

---

## Horizon 3 — Later (Strategic improvements; long lead time)

---

### REM-014: Incremental Compilation / Partial Pipeline for LSP
- **Objective:** Enable the LSP server to run only the phases it needs (e.g., stop after `SymbolTableFinal` for hover/completion) rather than running the full 33-phase pipeline on every keystroke.
- **Rationale:** The `AnalysisPhase upTo` parameter already supports partial runs; formalising this as an LSP mode would reduce latency.
- **Dependency:** REM-008 (phase registration) is a prerequisite.
- **Risk:** High (new compilation mode).

---

### REM-015: Performance Architecture — Memoised Type Registry
- **Objective:** Replace global mutable `TypeRegistry.DefaultRegistry` with a per-compilation immutable registry to enable parallel compilations and improve caching.
- **Rationale:** `TypeRegistry.DefaultRegistry` is a static singleton; any multi-file compilation that parallelises would corrupt it.
- **Risk:** High (type system refactor).

---

### REM-016: Diagnostic Code Taxonomy
- **Objective:** Assign stable diagnostic codes (e.g., `FIFTH001`-`FIFTH999`) to all compiler diagnostic emissions; document them in a `DIAGNOSTICS.md`.
- **Rationale:** Currently only some diagnostics have codes (e.g., `TRPL001`). Stable codes enable tooling, suppression, and documentation (X-3 from conformance).
- **Risk:** Low (additive change).

---

### REM-017: Upgrade OmniSharp LSP Dependency
- **Objective:** Evaluate upgrading `OmniSharp.Extensions.LanguageServer` from `0.19.9` to the current stable version.
- **Rationale:** The LSP specification has evolved; newer versions may provide correctness improvements (R13).
- **Risk:** Medium (protocol changes may require handler updates).

---

## Spec-Driven Backlog Summary

| Item | Horizon | Priority | Est. Effort | Key Risk |
|------|---------|----------|-------------|----------|
| REM-001 Delete postpone file | Now | P2 | 5 min | None |
| REM-002 Update constitution (IL refs) | Now | P1 | 1 h | None |
| REM-003 Update constitution (validate tool) | Now | P2 | 30 min | None |
| REM-004 Remove DiagnosticRecord | Now | P2 | 1 h | Low |
| REM-005 Route SPARQL diagnostics | Now | P1 | 2-4 h | Low-Med |
| REM-006 Remove TranslatorRegistry | Now | P1 | 2 h | Low |
| REM-007 Audit ExpressionCloner/DumpTree | Now | P3 | 1 h | Low |
| REM-008 Pipeline phase registry | Next | P2 | 1-2 days | Medium |
| REM-009 Decompose TypeAnnotationVisitor | Next | P2 | 2-3 days | Medium |
| REM-010 KG sub-pipeline | Next | P2 | 1 day | Medium |
| REM-011 Fix NullSafe root cause | Next | P1 | 1-2 days | Medium |
| REM-012 Reduce symbol table rebuilds | Next | P2 | 1-2 days | Medium |
| REM-013 SPARQL store execute | Next | P1 | 2-5 days | Medium-High |
| REM-014 Incremental LSP pipeline | Later | P3 | 1 week | High |
| REM-015 Per-compile type registry | Later | P3 | 1 week | High |
| REM-016 Diagnostic code taxonomy | Later | P3 | 2 days | Low |
| REM-017 OmniSharp upgrade | Later | P3 | 2-3 days | Medium |
