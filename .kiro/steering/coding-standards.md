---
description: coding-standards
inclusion: always
---
## Design
- CODE-002: Prefer the simplest design that satisfies the functional requirement and the non-functional requirements and other guidelines. To comply, verify abstractions meet all requirements.
## Maintainability
- CODE-003: Keep changes minimal and scoped to the target behavior. To comply, avoid unrelated refactors and preserve existing public APIs unless the change requires breaking them.
## Versioning
- CODE-006: Use Semantic Versioning in `MAJOR.MINOR.PATCH` format. To comply, bump version components according to compatibility impact.
## Cli
- CODE-007: Use stdin/args for input, stdout for normal output, and stderr for errors. To comply, route diagnostics to stderr and keep success output on stdout.
- CODE-008: Default CLI output must be human-readable text. To comply, add JSON output only when it meaningfully improves automation.
- CODE-009: CLI output must be deterministic. To comply, avoid timestamps and non-deterministic ordering in command output.
## Generation
- CODE-010 [MANDATORY]: NEVER hand-edit files in `src/ast-generated/`. To comply, make changes in metamodel or templates, then regenerate.
- CODE-011 [MANDATORY]: Modify AST structure only through `src/ast-model/` metamodels. To comply, regenerate generated AST output after metamodel edits.
## Parser
- CODE-012 [MANDATORY]: Update grammar files when grammar behavior changes. To comply, change `FifthLexer.g4` and `FifthParser.g4` as required by the syntax change.
- CODE-013 [MANDATORY]: Keep `AstBuilderVisitor.cs` synchronized with grammar changes. To comply, update visitor methods whenever parse-tree shape or surface syntax changes.
## Repository
- CODE-015 [MANDATORY]: Use `scripts/` only for durable automation. To comply, keep one-off scripts outside tracked repository paths.
## Security
- CODE-018 [MANDATORY]: Do not execute arbitrary code during parsing or generation. To comply, restrict execution paths to trusted, explicit operations.
- CODE-019: Validate all external inputs before use. To comply, keep user input data separated from internal template logic.
