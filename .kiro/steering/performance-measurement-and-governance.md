---
description: performance-measurement-and-governance
inclusion: always
---
## Performance
- PMG-001 [MANDATORY]: Before implementing a non-trivial optimization, baseline metrics shall be captured, and the same metrics shall be re-measured after the change.
- PMG-002 [MANDATORY]: Optimization work shall target only paths proven hot in profiling data or paths with material user-visible latency.
- PMG-003 [MANDATORY]: Performance optimizations shall preserve correctness, deterministic behavior, and required observability.
- PMG-004 [MANDATORY]: Performance investigations shall use profiling and production-safe telemetry to identify CPU, memory, allocation, and latency bottlenecks.
- PMG-005 [MANDATORY]: Critical performance characteristics shall be protected by repeatable benchmarks or automated regression checks.
- PMG-006 [MANDATORY]: When multiple designs meet performance targets, the simplest maintainable design shall be selected.
