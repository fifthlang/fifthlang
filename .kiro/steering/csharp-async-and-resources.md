---
description: csharp-async-and-resources
inclusion: always
---
## Development
- CAR-01 [MANDATORY]: Use `async` and `await` for naturally asynchronous operations and return `Task` or `Task<T>` by default.
- CAR-02 [MANDATORY]: Accept a `CancellationToken` in cancellable async APIs and pass it through to all cancellable downstream calls.
- CAR-03 [MANDATORY]: Do not block async flows with `.Result`, `.Wait()`, or `.GetAwaiter().GetResult()`; await tasks instead.
- CAR-04: Use `ValueTask` only in measured hot paths where synchronous completion is common and allocation reduction is proven.
- CAR-05 [MANDATORY]: Use clear LINQ and collection code by default, but materialize sequences when needed to avoid hidden multiple enumeration (for example, `ToList()` before multiple passes).
- CAR-06 [MANDATORY]: Dispose owned resources deterministically with `using` or `await using`.
- CAR-07 [MANDATORY]: Never dispose dependencies resolved from the DI container; disposal is owned by the container lifecycle.
- CAR-08: Use `Span<T>` and `ReadOnlySpan<T>` only in measured hot paths where they clearly reduce allocations without harming maintainability.
