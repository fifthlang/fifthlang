---
description: performance-io-serialization-and-fail-fast
inclusion: always
---
## Performance
- PIS-001 [MANDATORY]: Serialization and deserialization paths shall minimize allocation, copying, and intermediate materialization.
- PIS-002 [MANDATORY]: When full buffering is unnecessary for large payloads, data shall be streamed or piped instead of fully materialized.
- PIS-003 [MANDATORY]: Structured logging shall be used, and expensive log message construction shall be skipped when the log level is disabled.
- PIS-004 [MANDATORY]: Exceptions shall not be used as normal control flow on hot paths.
- PIS-005 [MANDATORY]: Arguments and preconditions shall be validated before expensive work begins.
