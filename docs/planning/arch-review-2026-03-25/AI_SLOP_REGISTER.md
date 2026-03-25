# AI Slop Findings Register
**Date:** 2026-03-25 | **Repository:** aabs/fifthlang

---

## Taxonomy

This register uses the following categories of "AI slop" (low-signal, redundant, or poorly-integrated agent-generated code):

| Code | Category |
|------|----------|
| **A** | Redundant abstraction / abstraction-for-its-own-sake |
| **B** | Duplicated logic with superficial differences |
| **C** | Inconsistent naming / style / boundary violation |
| **D** | Overly generic helper masking invariants |
| **E** | Spec drift (behaviour not reflected in specs or constitution) |
| **F** | Dead / unreachable / vestigial code |
| **G** | Meaningless or hollow documentation / comments |
| **H** | Suspiciously uniform scaffolding with low signal |

Priority: **P1** = correctness/security risk | **P2** = maintainability/architectural | **P3** = cosmetic/low-risk

---

## Findings

### AS-001 — Three Distinct `Diagnostic` Types in One Codebase
- **Location:** `src/compiler/CompilationResult.cs:1-30` (`compiler.Diagnostic` + `compiler.DiagnosticRecord`); `src/compiler/LanguageTransformations/SparqlVariableBindingVisitor.cs:270-290` (local `Diagnostic` class + local `DiagnosticSeverity` enum)
- **Category:** B (duplicated logic), C (boundary violation)
- **Evidence:** 
  - `compiler.Diagnostic` (record, line 1) and `compiler.DiagnosticRecord` (record, line 22) are structurally identical — same fields (`Level`, `Message`, `Source`, `Code`, `Namespace`, `Line`, `Column`) — with a comment: *"This intentionally reuses the existing compiler.Diagnostic shape"*. If they are identical, one should be an alias or the other should be deleted.
  - `SparqlVariableBindingVisitor.Diagnostic` (class, line 270) in the `compiler.LanguageTransformations` namespace partially shadows `compiler.Diagnostic`. It uses a field named `Code` (string) and an incompatible `DiagnosticSeverity` enum rather than the shared `DiagnosticLevel`. SPARQL binding diagnostics are therefore never routed to the main `List<compiler.Diagnostic>` diagnostics collection.
- **Risk:** Correctness — SPARQL variable binding errors may be silently lost; maintainability — contributors must know which type to use.
- **Remediation:**
  1. Delete `compiler.DiagnosticRecord`; replace usages with `compiler.Diagnostic`.
  2. Replace `SparqlVariableBindingVisitor.Diagnostic` with `compiler.Diagnostic`, adapting `DiagnosticSeverity` → `DiagnosticLevel`.
  3. Plumb SPARQL binding diagnostics into the existing `List<compiler.Diagnostic>` parameter chain.
  4. Tests: add a test that a bad SPARQL variable binding produces a `DiagnosticLevel.Error` in the compilation result.
- **Priority:** P1

---

### AS-002 — `NullSafeRecursiveDescentVisitor`: Masking Workaround
- **Location:** `src/compiler/LanguageTransformations/NullSafeRecursiveDescentVisitor.cs`
- **Category:** D (helper masking invariants), A (redundant abstraction)
- **Evidence:** The class wraps `DefaultRecursiveDescentVisitor` and silently falls back to the original node when `Visit()` returns null. It is used by `TreeLinkageVisitor`, `TripleDiagnosticsVisitor`, `TryCatchFinallyValidationVisitor`, and `LambdaValidationVisitor`. The comment says it *"prevents null returns from corrupting the AST"* — but `DefaultRecursiveDescentVisitor` should never return null if the visitor invariant is maintained. The class exists as a band-aid for an underlying bug.
- **Risk:** Correctness — if a visitor bug produces null, it will be silently ignored rather than surfaced; this violates constitution §VI. The class also only handles `BinaryExp` and `FuncCallExp`, leaving all other node types unguarded — so the protection is incomplete and misleading.
- **Remediation:**
  1. Identify the root cause of the null returns (likely a visitor method returning null for an unhandled node type in `DefaultRecursiveDescentVisitor`).
  2. Fix the underlying generated visitor or the derived visitor to never return null.
  3. Delete `NullSafeRecursiveDescentVisitor` and make affected visitors extend `DefaultRecursiveDescentVisitor` directly.
  4. Tests: add assertions that visitor traversal never produces null for any sample AST.
- **Priority:** P1

---

### AS-003 — `TranslatorRegistry` Static Mutable State
- **Location:** `src/compiler/TranslatorRegistry.cs`
- **Category:** A (redundant abstraction), C (architectural boundary violation)
- **Evidence:** The class comment says it is a *"lightweight registry … to avoid large constructor API changes"* — this is an explicit acknowledgment that it is a workaround. The `Current` property is a static mutable field with no thread-safety guarantee. The comment implies this is temporary ("during experimentation"), but it is now on the critical path of every compilation.
- **Risk:** Thread-safety (parallel tests can corrupt `Current`), testability (cannot inject different translators per test without resetting global state), architectural (violates dependency injection principles).
- **Remediation:**
  1. Add `IBackendTranslator? translator = null` parameter to `Compiler` constructor.
  2. Fall back to `new LoweredAstToRoslynTranslator()` when null.
  3. Remove `TranslatorRegistry`.
  4. Update all tests that use `TranslatorRegistry.Current =` to use the constructor parameter.
- **Priority:** P1

---

### AS-004 — `FifthParserManager.cs.postpone` Legacy File
- **Location:** `src/compiler/FifthParserManager.cs.postpone` (219 lines)
- **Category:** F (vestigial dead code), E (spec drift)
- **Evidence:** The file contains an old version of `FifthParserManager` that references `il_ast` namespace (which no longer exists in the solution), `VerticalLinkageVisitor` (renamed to `TreeLinkageVisitor`), and a much shorter 12-pass pipeline. It is not compiled (`.postpone` extension). It is git-tracked and will confuse contributors. A developer searching for `FifthParserManager` could find this first and be misled.
- **Risk:** Contributor confusion, no direct correctness risk.
- **Remediation:** Delete the file; optionally add a git-note or PR comment documenting the IL-to-Roslyn migration history for future reference.
- **Priority:** P2

---

### AS-005 — `SymbolTableBuilderVisitor` and `VarRefResolverVisitor` Invoked 3–4 Times Each
- **Location:** `src/compiler/ParserManager.cs` lines 110, 138, 292, 295, 309, 340-341
- **Category:** B (duplicated logic), H (uniform scaffolding)
- **Evidence:** `SymbolTableBuilderVisitor` is invoked at phases 5 (initial), 23 (final), after TypeAnnotation (phase 25 inner), and again in the graph lowering inner block (line 340). `VarRefResolverVisitor` is invoked at phase 24 and again at line 341. `TreeLinkageVisitor` is invoked at phases 1, 19, and again at line 339 inside the graph lowering block. Each re-invocation is preceded by a comment explaining *why*, but the repetition suggests the pipeline lacks an incremental update mechanism and re-runs full scans to compensate.
- **Risk:** Performance (O(n) traversals repeated 3x per compilation), maintainability (hard to reason about ordering invariants).
- **Remediation:** 
  1. Document explicitly which passes *dirty* the symbol table or parent links and which passes *require* a fresh table.
  2. Investigate whether a single final symbol-table pass (after all structural transforms) is sufficient.
  3. If not, introduce an incremental symbol table update API.
- **Priority:** P2

---

### AS-006 — `TypeAnnotationVisitor` and `TripleGraphAdditionLoweringRewriter` God Classes
- **Location:** `src/compiler/LanguageTransformations/TypeAnnotationVisitor.cs` (942 lines), `src/compiler/LanguageTransformations/TripleGraphAdditionLoweringRewriter.cs` (946 lines)
- **Category:** A (over-large single responsibility violation)
- **Evidence:** Both files exceed 900 lines. Constitution §VIII-3 says "each transformation visitor should have a single, well-defined responsibility." `TypeAnnotationVisitor` handles type inference, generic resolution, method dispatch, knowledge-graph type annotation, and multiple other concerns. `TripleGraphAdditionLoweringRewriter` handles graph assertion, triple creation, operator lowering, and SPARQL generation.
- **Risk:** Maintainability (hard to test individual concerns), correctness (complex interactions between concerns).
- **Remediation:** Decompose each into 2-3 focused passes. For `TypeAnnotationVisitor`: separate generic type inference, standard type annotation, and KG-specific annotation. For `TripleGraphAdditionLoweringRewriter`: separate operator detection, triple object construction, and SPARQL statement generation.
- **Priority:** P2

---

### AS-007 — `ExpressionCloner` Class with Unclear Ownership
- **Location:** `src/compiler/LanguageTransformations/ExpressionCloner.cs`
- **Category:** A (abstraction of unclear scope), H (scaffolding)
- **Evidence:** File exists but is not referenced in the AI Slop Register search above. It is in `LanguageTransformations/` suggesting it is part of the pass pipeline but it is not in the `AnalysisPhase` enum.
- **Risk:** Possibly dead code or an orphaned helper with unclear contract.
- **Remediation:** Verify usages; if unused, delete; if used, document its contract and relationship to the `DefaultAstRewriter` cloning semantics.
- **Priority:** P3

---

### AS-008 — `LambdaDiagnostics.cs` and `ComprehensionDiagnostics.cs` Thin Wrappers
- **Location:** `src/compiler/LanguageTransformations/LambdaDiagnostics.cs`, `src/compiler/LanguageTransformations/ComprehensionDiagnostics.cs`, `src/compiler/LanguageTransformations/SparqlDiagnostics.cs`, `src/compiler/LanguageTransformations/ConstructorDiagnostics.cs`
- **Category:** A (abstraction-for-its-own-sake), H (scaffolding)
- **Evidence:** There are at least 4 `*Diagnostics.cs` files containing static helper classes with diagnostic message formatting logic. This pattern proliferates files for what could be a single `DiagnosticMessages.cs` static class with well-named methods. The separation by feature area may be intentional (feature ownership) but creates discovery friction and risks duplication of message formatting conventions.
- **Risk:** Low — maintainability and discoverability.
- **Remediation:** Evaluate whether to consolidate into a single `DiagnosticMessages` class with nested static classes by feature area, or keep the current files but add an index comment pointing to all `*Diagnostics.cs` files.
- **Priority:** P3

---

### AS-009 — `TODO` Comments on Core Type Constraints
- **Location:** `src/ast-model/AstMetamodel.cs:597`, `src/ast-model/AstMetamodel.cs:656`, `src/parser/AstBuilderVisitor.cs:701`, `src/compiler/LanguageTransformations/GenericTypeInferenceVisitor.cs:98,125`, `src/compiler/SemanticAnalysis/ConstructorResolver.cs:166,206`, `src/compiler/ParserManager.cs:477`
- **Category:** G (hollow documentation/deferred decision), F (vestigial branches)
- **Evidence:**
  - `AstMetamodel.cs:597`: `// TODO: is this a reference or something similar?` on `GraphNamespaceAlias` — unanswered design question in a core type
  - `AstMetamodel.cs:656`: `// TODO: work out what I meant by this` — acknowledged confusion on a public record
  - `GenericTypeInferenceVisitor.cs:98`: `// TODO: Handle complex generic types` — generics spec (completed-012) claimed to be done
  - `ParserManager.cs:477`: `// TODO: Temporarily disabled due to AST construction issues - fix and re-enable` — disabled code path with no associated issue/ticket
- **Risk:** Correctness (disabled code, incomplete features), contributor confusion.
- **Remediation:** Convert each TODO to a tracked GitHub issue; remove or clarify ambiguous type definitions in the metamodel; re-enable or delete the disabled parser manager code path with a decision record.
- **Priority:** P2 (for AstMetamodel and disabled path), P3 (for others)

---

### AS-010 — `Usings.cs` Global Using Pattern with No Enforcement
- **Location:** `src/compiler/Usings.cs`
- **Category:** C (inconsistent style)
- **Evidence:** A `Usings.cs` file in `src/compiler/` contains global using directives. Not all projects follow this pattern (some have `GlobalUsings.cs`, some use file-level usings inline, some have no global usings). This inconsistency is minor but indicates non-uniform agent-generated scaffold.
- **Risk:** Low — style only.
- **Remediation:** Establish a convention (either all projects use a `GlobalUsings.cs` or none do) and normalise.
- **Priority:** P3

---

## Summary

| Priority | Count | Key Items |
|----------|-------|-----------|
| P1 | 3 | AS-001 (Diagnostic duplication), AS-002 (NullSafe masking), AS-003 (static registry) |
| P2 | 4 | AS-004 (postpone file), AS-005 (repeated passes), AS-006 (god classes), AS-009 (TODOs) |
| P3 | 3 | AS-007 (ExpressionCloner), AS-008 (Diagnostics files), AS-010 (Usings) |
