---
inclusion: fileMatch
fileMatchPattern: "src/compiler/LanguageTransformations/**,src/compiler/ParserManager.cs,src/code_generator/**"
---

# Language Transformation Pipeline

## Transformation Pass Order

The compiler applies these passes sequentially in `ParserManager.cs`:

1. TreeLinkageVisitor — Parent-child relationships
2. BuiltinInjectorVisitor — Built-in function definitions
3. ClassCtorInserter — Default constructors
4. SymbolTableBuilderVisitor — Symbol tables for scoping
5. PropertyToFieldExpander — Property syntax to field access
6. OverloadGatheringVisitor — Group function overloads
7. OverloadTransformingVisitor — Guard/subclause pattern
8. DestructuringVisitor — Property references in destructuring
9. DestructuringLoweringRewriter — Destructuring to variable declarations
10. TypeAnnotationVisitor — Type inference and annotation

## Design Principles

- Each visitor has a single, well-defined responsibility
- Passes are order-dependent; later passes may depend on earlier ones
- Prefer multiple simple passes over complex single passes
- Document dependencies between transformation passes
- Each pass must preserve AST validity and type safety
- Prefer AST transformations over code generation complexity

## Adding a New Transformation

1. Create visitor in `src/compiler/LanguageTransformations/`
2. Choose the right pattern (see code-generation-rules steering for pattern guide)
3. Register in the pipeline in `src/compiler/ParserManager.cs`
4. Add tests in `test/ast-tests/` or `test/runtime-integration-tests/`
5. Build and run full test suite

## Roslyn Migration Note

The project is transitioning from legacy IL pipeline (AstToIlTransformationVisitor → ILEmissionVisitor → PEEmitter) to `LoweredAstToRoslynTranslator`. The legacy pipeline remains available behind a feature flag. Roslyn-generated PDBs must include full line-and-column sequence points for debugging fidelity.
