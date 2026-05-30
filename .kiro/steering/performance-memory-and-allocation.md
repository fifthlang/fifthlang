---
description: performance-memory-and-allocation
inclusion: always
---
## Performance
- PMA-001 [MANDATORY]: Hot-path code shall avoid unnecessary allocations and shall keep allocation rate low enough to prevent avoidable GC pressure.
- PMA-002 [MANDATORY]: Hot-path code shall avoid hidden boxing.
- PMA-003 [MANDATORY]: When a hot path is allocation-sensitive, allocation-heavy LINQ or iterator chains shall be replaced with materially cheaper loops.
- PMA-004 [MANDATORY]: Hot-path code shall minimize transient string allocation.
- PMA-005 [MANDATORY]: When parsing or formatting on hot paths, span-based APIs shall be used when they measurably reduce copying or allocation.
- PMA-006: Span<T> and ReadOnlySpan<T> shall be used only when ownership and lifetime remain safe and clear.
- PMA-007: Memory<T> and ReadOnlyMemory<T> shall be used when buffer lifetime must cross async or heap boundaries.
- PMA-008 [MANDATORY]: Large buffers and collections shall not be copied when a safe slice, view, or reference is sufficient.
- PMA-009: stackalloc shall be used only for small, bounded, short-lived buffers.
- PMA-010: ArrayPool<T> shall be used when measured allocation patterns show frequent buffer churn causing GC pressure.
- PMA-011: Object pooling shall be used only for objects that are expensive to create or reset and are used at predictable high frequency.
- PMA-012 [MANDATORY]: Rented or pooled resources shall be returned promptly exactly once and shall not be used or exposed after return.
- PMA-013 [MANDATORY]: GC.Collect shall not be used in production as a performance strategy.
