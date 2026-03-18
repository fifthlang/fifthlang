# Development Guide

This section covers everything you need to know to **work on the Fifth compiler** itself — building from source, running tests, publishing releases, and understanding the repository layout.

## Where to Start

| Document | Description |
|----------|-------------|
| [Repository Overview](REPOSITORY_SUMMARY.md) | Architecture, components, technologies, and team |
| [Release Process](release-process.md) | How to create and publish official releases |
| [Publishing the SDK](publish-compiler-sdk.md) | Publishing `Fifth.Compiler.Tool` and `Fifth.Sdk` to NuGet |

## Build Commands (Quick Reference)

```bash
# Restore dependencies (~70 s, first run)
dotnet restore fifthlang.sln

# Build the full solution (~60 s)
dotnet build fifthlang.sln

# Run all tests (regression gate)
dotnet test fifthlang.sln

# Regenerate AST builders/visitors after changing AstMetamodel.cs
just run-generator
```

> **ANTLR grammar** compilation happens automatically during the parser project build.  
> **Never edit** files in `src/ast-generated/` manually — regenerate them with `just run-generator`.

## Key Source Locations

```
src/
├── parser/              ANTLR-based parser (FifthLexer.g4, FifthParser.g4)
├── ast-model/           Core AST definitions (AstMetamodel.cs)
├── ast-generated/       Auto-generated builders & visitors (DO NOT EDIT)
├── compiler/            Transformation pipeline (18+ phases)
├── code_generator/      IL emission (Roslyn-based)
├── fifthlang.system/    Runtime library (KG operations)
└── Fifth.Sdk/           MSBuild integration

test/
├── ast-tests/           AST builder & visitor tests
├── syntax-parser-tests/ Grammar & parsing tests
├── runtime-integration-tests/ End-to-end execution tests
└── kg-smoke-tests/      Knowledge graph feature tests
```

## Contributing

1. Check open issues tagged [`good-first-issue`](https://github.com/aabs/fifthlang/labels/good-first-issue).
2. Read [AGENTS.md](../../AGENTS.md) for coding conventions and agent workflow.
3. Open a [GitHub Discussion](https://github.com/aabs/fifthlang/discussions) for questions or proposals.
