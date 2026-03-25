---
inclusion: auto
---

# Fifth Language Project Overview

Fifth Language is a C# .NET 10.0 compiler for the Fifth programming language. It uses an ANTLR-based split lexer/parser, AST code generation for builders and visitors, and a multi-pass compiler with language transformations that perform AST lowering through intermediate representations.

## Compiler Pipeline

1. Lexical Analysis → Parsing → Parse Tree (ANTLR split grammar)
2. Parse Tree → AST Building → High-Level AST (`AstBuilderVisitor.cs`)
3. High-Level AST → Language Transformations → Lowered AST (multiple visitor passes)
4. Lowered AST → Roslyn Code Generation → PE Assembly

## Key Directories

- `src/ast-model/` — AST metamodel definitions (`AstMetamodel.cs`)
- `src/ast-generated/` — Auto-generated builders/visitors/rewriters. NEVER hand-edit.
- `src/ast_generator/` — Code generator that produces `ast-generated/` from metamodels
- `src/parser/grammar/` — `FifthLexer.g4` + `FifthParser.g4` (split grammar)
- `src/parser/AstBuilderVisitor.cs` — Parse tree to high-level AST
- `src/compiler/LanguageTransformations/` — AST transformation passes
- `src/compiler/ParserManager.cs` — Transformation pipeline coordinator
- `src/fifthlang.system/` — Built-in system functions and knowledge graph support
- `test/ast-tests/` — TUnit AST and generator tests
- `test/syntax-parser-tests/` — Grammar parsing tests
- `test/runtime-integration-tests/` — End-to-end verification tests

## Authoritative References

- Constitution: `.specify/memory/constitution.md` — all architectural decisions and principles
- Agent instructions: `AGENTS.md` — operational commands and workflow guidance
- Spec-kit config: `.specify/config.yml` — build/test command definitions
