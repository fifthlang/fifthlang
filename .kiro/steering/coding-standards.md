---
inclusion: auto
---

# Coding Standards and Principles

## Core Principles

- Library-First: Every feature starts as a focused library under `src/` with a clear public contract
- Simplicity and YAGNI: Prefer the simplest design that works. No incidental complexity or non-required abstractions.
- Minimal Surface: Make targeted, minimal changes that respect existing structure and APIs
- Safety: Do not add catch-all error handling that hides defects. Changes increasing complexity must be justified.

## C# Conventions

- C# 12 language version (or latest supported by .NET 8 SDK)
- .NET 8.0 target framework
- Semantic Versioning (MAJOR.MINOR.PATCH) for all packages

## CLI and Text I/O Discipline

- stdin/args → input; stdout → primary output; stderr → errors/diagnostics
- Support human-readable text; add JSON output where suitable for automation
- Favor deterministic, scriptable commands
- Output must be deterministic and stable (no timestamps or non-deterministic ordering)

## File Editing Rules

- NEVER hand-edit files in `src/ast-generated/`
- To modify AST: edit metamodels in `src/ast-model/`, then regenerate
- Grammar changes: update both `FifthLexer.g4` AND `FifthParser.g4` as needed
- Always update `AstBuilderVisitor.cs` for grammar changes

## Repository Cleanliness

- Never commit temporary debugging helpers, IL dumps, or scratch `.5th` programs
- `scripts/` is reserved for durable automation only
- Never commit `tmp_*.5th`, `build_debug_il/`, `KEEP_FIFTH_TEMP`, or `--keep-temp` outputs
- Use `.gitignore` patterns and local temp directories for experiments

## Security

- Avoid executing arbitrary code during generation or parsing
- Validate inputs; separate user inputs from internal templates
- Do not introduce network calls or file system side-effects without explicit review

## Key NuGet Packages

- `Antlr4.Runtime.Standard` — ANTLR runtime
- `RazorLight` — Template engine for code generation
- `System.CommandLine` — CLI parsing
- `xUnit` + `FluentAssertions` — Testing
- `dunet` — Discriminated unions
- `Vogen` — Value object generation
