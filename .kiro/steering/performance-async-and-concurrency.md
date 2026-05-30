---
description: performance-async-and-concurrency
inclusion: always
---
## Performance
- PAC-001 [MANDATORY]: For naturally asynchronous I/O-bound work, asynchronous APIs with async/await shall be used.
- PAC-002 [MANDATORY]: Throughput-sensitive code paths shall not block threads while waiting for asynchronous operations.
- PAC-003: ValueTask shall be used only when measurements show material allocation benefit and usage semantics are correct for ValueTask consumption.
- PAC-004 [MANDATORY]: Long-running or potentially blocking operations shall accept and propagate CancellationToken.
- PAC-005 [MANDATORY]: Parallelism shall be introduced only when the workload is thread-safe, partitionable, and measurably faster under realistic contention.
- PAC-006 [MANDATORY]: Throughput-sensitive code shall minimize shared mutable state and lock contention.
