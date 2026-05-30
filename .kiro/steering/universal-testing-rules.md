---
description: universal-testing-rules
inclusion: always
---
## Completion
- UTR-001: A feature shall be marked complete only when integration tests pass at runtime, verify in-situ behavior, verify accessible and correct results, and exercise major code paths and result types.
- UTR-002: When only compilation checks exist, the feature shall be marked incomplete.
- UTR-003: When any runtime test fails, the feature shall be marked incomplete.
## Testing
- UTR-004: When behavioral validation is possible, tests shall validate externally observable behavior and avoid internal-detail and concrete-implementation assertions.
- UTR-005: Tests shall not mask failures with broad try/catch blocks or unconditional success assertions.
- UTR-006: When a test fails, the team shall treat it as outstanding work and shall not suppress it.
