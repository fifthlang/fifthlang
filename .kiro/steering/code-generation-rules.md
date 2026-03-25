---
inclusion: fileMatch
fileMatchPattern: "src/ast-model/**,src/ast-generated/**,src/ast_generator/**"
---

# AST Code Generation Rules

## Generator-as-Source-of-Truth

The AST generator is authoritative for all files under `src/ast-generated/`. These files are NEVER hand-edited.

Generated files:
- `builders.generated.cs` — Builder pattern classes
- `visitors.generated.cs` — Visitor pattern classes
- `rewriter.generated.cs` — Rewriter pattern for lowering
- `typeinference.generated.cs` — Type inference support

## How to Change Generated Code

1. Edit `src/ast-model/AstMetamodel.cs`
2. Optionally update templates under `src/ast_generator/Templates/`
3. Regenerate: `just run-generator`
4. Build the full solution: `dotnet build fifthlang.sln`

## AST Design

- AST (`AstMetamodel.cs`): Rich, high-level constructs mirroring source language features, lowered through transformation passes for Roslyn code generation

## Visitor/Rewriter Pattern Selection

- `BaseAstVisitor` — Read-only analysis (symbol tables, diagnostics, validation). Cannot modify AST.
- `DefaultRecursiveDescentVisitor` — Type-preserving AST modifications. Cannot change node types or hoist statements.
- `DefaultAstRewriter` ⭐ PREFERRED for new lowering passes — Statement-level desugaring, cross-type rewrites, expression hoisting. Returns `RewriteResult` with node + prologue.

Use `DefaultAstRewriter` when introducing temporary variables, breaking down high-level constructs, transforming expression types, or any lowering requiring statement insertion.

See `src/ast_generator/README.md` for the comprehensive pattern guide.

## PR Requirements

Any PR modifying `src/ast-generated/` must include:
- The upstream metamodel or template changes
- The regeneration command used
- No hand-edits in generated files
