---
description: transformation-pipeline
inclusion: always
---
## Pipeline
- PIPE-001: Apply transformation passes in the canonical order defined by `ParserManager.cs`. To comply, keep pass registration in this sequence: TreeLinkage, BuiltinInjector, ClassCtorInserter, SymbolTableBuilder, PropertyToFieldExpander, OverloadGathering, OverloadTransforming, DestructuringVisitor, DestructuringLoweringRewriter, TypeAnnotation.
- PIPE-010: Transformation passes MUST use rewriters rather than visitors. To comply, derive from `DefaultAstRewriter` or a class derived from it.
- PIPE-011: Never use Recursive descent visitors unless the AST is not going to change.  To Comply, don't derive from DefaultRecursiveDescentVisitor or a class derived from it.
## Design
- PIPE-002: Each transformation pass must have one well-defined responsibility. To comply, split passes that mix unrelated concerns.
- PIPE-004: Prefer multiple simple passes over one mixed pass. To comply, introduce a new focused pass instead of extending an unrelated pass.
- PIPE-007: Prefer AST transformations over adding complexity to code generation. To comply, lower language constructs before `LoweredAstToRoslynTranslator`.
## Dependency
- PIPE-003: A pass may depend only on invariants established by earlier passes. To comply, place each pass after the pass that creates its prerequisites.
## Documentation
- PIPE-005: Document pass dependencies when one pass relies on another. To comply, record dependency assumptions in code comments or phase summaries.
## Correctness
- PIPE-006: Every pass must preserve AST validity and type safety. To comply, add tests that fail if the pass creates invalid or untyped AST states.
## Workflow
- PIPE-008: A new transformation is complete only after implementation, registration, and tests. To comply, add the pass in `src/compiler/LanguageTransformations/`, register it in `src/compiler/ParserManager.cs`, add tests, then run build and full tests.
## Code Generation
- PIPE-009: Emit Roslyn syntax from lowered AST through `LoweredAstToRoslynTranslator` with debug-accurate sequence points. To comply, preserve full line-and-column mapping in generated PDB data.
