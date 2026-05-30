---
description: architecture
inclusion: always
---
## Governance
- ARCH-001 [MANDATORY]: Each rule in this document must be objectively testable by an automated check. To comply, include a `Verify:` line with a concrete pass/fail condition.
## Dependency
- ARCH-002: Project references under `src/` must follow this DAG and must not point backward:

```text
ast-model -> ast_generator -> ast-generated -> parser -> compiler -> tests
                                               ^
                                          fifthlang.system
```

To comply, reject any `<ProjectReference>` that targets an earlier node in this order.
## Ast
- ARCH-004: Only `src/ast-model/AstMetamodel.cs` may define AST types and inheritance. To comply, never create classes inheriting `AstThing`, `Expression`, `Statement`, or `TypeRef` anywhere but there.
## Backend
- ARCH-005: Only `LoweredAstToRoslynTranslator` may bridge to Roslyn types. To comply, do not use `Microsoft.CodeAnalysis` in `src/compiler/LanguageTransformations/`, `src/compiler/Pipeline/Phases/`, `src/ast-model/`, or `src/ast-generated/`.
## Pipeline
- ARCH-006 [MANDATORY]: Each phase must declare every capability it consumes via `DependsOn`. To comply, in each `ICompilerPhase`, ensure `DependsOn` covers all required AST state used by its visitors or rewriters.
- ARCH-007: Each `ICompilerPhase` must have exactly one responsibility category. To comply, if a phase performs multiple categories, split it or justify the composition in its XML summary.
- ARCH-008: A phase must not mutate the input AST after returning from `Transform`. To comply, do not store the input `ast` parameter in instance or static fields.
- ARCH-011: `TransformationPipeline.CreateDefault()` defines a fixed phase order. To comply, keep only direct `RegisterPhase` calls in that method, with no runtime reordering logic.
- ARCH-012: Phases must not communicate through static mutable state. To comply, pass data only through AST and `PhaseContext`; the sole static exception is read-only `DebugHelpers.DebugEnabled`.
## Lowering
- ARCH-009: Lowering phases must move constructs toward forms directly handled by `LoweredAstToRoslynTranslator`. To comply, output node shapes must be equal-or-lower level than input node shapes.
## Diagnostics
- ARCH-010: Phases must emit diagnostics only through `PhaseResult.Diagnostics` or `PhaseContext.Diagnostics`. To comply, avoid `Console.Out`; allow `Console.Error` only when `DebugHelpers.DebugEnabled` is true.
## Parser
- ARCH-013 [MANDATORY]: Only `FifthLexer.g4` and `FifthParser.g4` may define parseable Fifth syntax. To comply, `AstBuilderVisitor` may map parse trees to AST, but must not introduce syntax not accepted by the grammar.
- ARCH-014 [MANDATORY]: Semantic parser rules in `FifthParser.g4` must map one-to-one with `Visit*` methods in `AstBuilderVisitor.cs`. To comply, keep rule-name and visitor-method sets aligned, allowing ANTLR labeled alternatives.
## Visitor
- ARCH-015: Visitor base class must match the operation type.

| Operation | Base class |
|---|---|
| Read-only analysis | `BaseAstVisitor` |
| Type-preserving edits | `DefaultRecursiveDescentVisitor` |
| Cross-type rewrites or prologue insertion | `DefaultAstRewriter` |

To comply, do not mutate AST in `BaseAstVisitor`, and use `DefaultAstRewriter` when returning non-empty prologue or changing node types.
