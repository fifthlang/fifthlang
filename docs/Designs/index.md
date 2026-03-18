# Design Documents

This section contains detailed design and implementation documentation for Fifth language features and compiler subsystems. These documents record design decisions, implementation strategies, and technical details that are useful for contributors working on the compiler.

## Feature Design Documents

| Document | Description |
|----------|-------------|
| [Fifth.Sdk](fifth-sdk-readme.md) | MSBuild SDK for `.5thproj` project files |
| [MSBuild Project Type — Implementation Summary](5thproj-implementation-summary.md) | Detailed record of the `.5thproj` implementation |
| [Namespace Implementation](namespace-implementation.md) | File-scoped namespaces, import directives, and resolution pipeline |

## Compiler Internals

| Document | Description |
|----------|-------------|
| [AST Rewriter Design](misc/AST_REWRITER_DESIGN.md) | `DefaultAstRewriter` pattern for statement-hoisting transformations |
| [Debugging Workflows](misc/debugging.md) | How to inspect emitted IL and capture diagnostics |
| [Destructuring Lowering Migration](misc/destructuring-lowering-migration.md) | Notes on migrating destructuring to the rewriter pattern |
| [Triple Diagnostics Refactor](misc/triple-diagnostics-refactor.md) | Diagnostic codes for triple literal errors (TRPL001–TRPL006) |
| [Exception Handling Migration](misc/migration-exception-handling.md) | Exception handling lowering details |

## Syntax Reference

| Document | Description |
|----------|-------------|
| [Syntax Samples](misc/syntax-samples-readme.md) | Sample programs for syntax test cases |
| [Syntax Test Cases](misc/syntax-testcases-bulleted.md) | Bulleted list of grammar test scenarios |
| [Syntax Test Plan](misc/syntax-testplan.md) | Test plan for grammar coverage |

## Performance

| Document | Description |
|----------|-------------|
| [Performance Baselines](perf/baselines.md) | Guard validation and other compiler performance baselines |

---

*These documents are primarily intended for compiler contributors. For user-facing documentation, see the [Getting Started](../Getting-Started/index.md) section.*
