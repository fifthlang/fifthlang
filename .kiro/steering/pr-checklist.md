---
inclusion: manual
---

# PR Checklist and Quality Gates

## Pre-PR Validation

1. Build succeeds for the full solution (no cancellations): `dotnet build fifthlang.sln`
2. All tests pass: `dotnet test fifthlang.sln`
3. Grammar examples validate: `just validate-examples`

## PR Requirements

- New/updated tests added; all suites pass locally
- No hand-edits in `src/ast-generated/`; regeneration steps included if metamodel changed
- Grammar changes have corresponding parser and visitor updates
- Transformation changes are properly integrated into the pipeline in `ParserManager.cs`
- Public contracts and CLI help text updated when behavior changes
- Rationale documented for any complexity increases

## Review Standards

- Favor smallest viable change; keep diffs focused
- Confirm reproducibility by re-running documented commands
- Verify deterministic outputs and diagnostics
- Validate that AST transformations maintain proper lowering semantics

## Breaking Changes

Breaking changes require:
- A migration note in the PR
- Updated tests reflecting new behavior
- A minor/major version bump as appropriate
- Generated code changes follow metamodel versioning
- Deprecations must be documented and tested

## Grammar Compliance Checklist

When adding/updating `.5th` examples or test programs:
1. Validate parsing locally with parser/syntax tests
2. Use grammar-supported forms only (no legacy shorthand)
3. Add `CopyToOutputDirectory` metadata in test `.csproj` if sample is referenced by integration tests
4. Run relevant integration tests before committing
5. Update grammar files and constitution if intentionally introducing new surface syntax
