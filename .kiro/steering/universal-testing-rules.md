---
description: universal-testing-rules
inclusion: always
---
## Completion
- TR-001: A feature is not complete until integration tests prove it:

1. Runs as intended in situ
2. Executes successfully at runtime rather than merely compiling
3. Produces results that are accessible and correct
4. Exercises the major code paths and result types involved

Features with only compilation tests or with failing runtime tests are incomplete.
## Testing
- TR-002: Avoid testing internal implementation details and avoid depending on concrete implementations where looser behavioral validation is possible.
- TR-003: Never mask failing tests with broad `try` or `catch` blocks or "success assertions".
