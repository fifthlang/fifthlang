---
description: code-generation-rules
inclusion: always
---
## Generation
- GEN-001: Treat `src/ast-generated/` as generator output only. To comply, never hand-edit files in `src/ast-generated/`.
- GEN-002: Keep generated outputs in their canonical files. To comply, emit builders to `builders.generated.cs`, visitors to `visitors.generated.cs`, rewriters to `rewriter.generated.cs`, and type inference helpers to `typeinference.generated.cs`.
## Workflow
- GEN-003: Change generated output by editing source inputs, then regenerating. To comply, update `src/ast-model/AstMetamodel.cs` or templates, run `just run-generator`, then run `dotnet build fifthlang.sln`.
## Design
- GEN-004: Keep `AstMetamodel.cs` focused on high-level language constructs. To comply, represent source-level features in the metamodel and lower them in transformation passes.
## Visitor
- GEN-005: Use `BaseAstVisitor` only for read-only analysis. To comply, use it for symbol collection, diagnostics, or validation, and do not mutate AST nodes.
- GEN-006: Use `DefaultRecursiveDescentVisitor` only for type-preserving AST edits. To comply, keep input and output node types the same and avoid statement hoisting.
- GEN-007: Use `DefaultAstRewriter` for lowering that changes node shape or inserts statements. To comply, choose this pattern when returning `RewriteResult` with prologue, adding temporaries, or doing cross-type rewrites.
## Reference
- GEN-008: Use `src/ast_generator/README.md` as the detailed reference for pattern selection. To comply, verify the selected visitor or rewriter pattern against that guide before adding a new pass.
## Review
- GEN-009: A pull request that changes `src/ast-generated/` must show the source change that produced it. To comply, include metamodel or template edits, the regeneration command, and confirmation that generated files were not hand-edited.
