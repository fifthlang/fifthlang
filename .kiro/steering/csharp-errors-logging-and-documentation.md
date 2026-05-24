---
description: csharp-errors-logging-and-documentation
inclusion: always
---
## Development
- CEL-01 [MANDATORY]: Validate method arguments at the boundary with guard clauses (for example, `ArgumentNullException.ThrowIfNull`) and fail fast on invalid inputs.
- CEL-02 [MANDATORY]: Throw exceptions only for exceptional conditions; use explicit return results for expected control flow outcomes.
- CEL-03 [MANDATORY]: Preserve diagnostic context when propagating exceptions: use `throw;` to rethrow and include the original exception as `InnerException` when wrapping.
- CEL-04 [MANDATORY]: Use structured logging with message templates and named properties (for example, `logger.LogInformation("Processed {OrderId}", orderId)`).
- CEL-05 [MANDATORY]: Never log secrets, credentials, tokens, or other sensitive values; log redacted or hashed identifiers where traceability is required.
- CEL-06 [MANDATORY]: Write comments only for intent, invariants, and non-obvious decisions; do not restate what the code already expresses clearly.
- CEL-07 [MANDATORY]: Document public APIs with XML documentation when intent, contracts, or failure behavior are not obvious from the signature.
- CEL-08 [MANDATORY]: Keep source-generated and hand-written code clearly separated (for example, generated files under a dedicated generated path, with partial types used only at intentional boundaries).
- CEL-09 [MANDATORY]: Do work that may fail off to the side before changing committed state. First prepare all risky work using temporary state; then commit using steps that cannot fail or are tightly controlled.
- CEL-10 [MANDATORY]: Prefer commit-or-rollback behavior when practical. A failed operation should ideally leave the observable state exactly as it was before the operation began
