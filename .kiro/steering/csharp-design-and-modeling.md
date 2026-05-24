---
description: csharp-design-and-modeling
inclusion: always
---
## Development
- CDM-01: Default to immutable models by using `init` setters, `readonly` fields, and immutable collections unless mutability is required.
- CDM-02: Implement value objects as `record` or `record struct` when value-based equality is the intended behavior.
- CDM-03: Use `struct` only for small, immutable value types when profiling shows a measurable allocation or locality benefit.
- CDM-04: Keep each class focused on one responsibility; split classes when behavior changes for unrelated reasons.
- CDM-05: Write methods to perform one operation at one abstraction level; extract helper methods when a method mixes high-level flow and low-level detail.
- CDM-06: Use generic abstractions to share type-safe behavior instead of duplicating equivalent logic per type.
- CDM-07: Use pattern matching (`is`, `switch`, `switch` expressions) as the default for type and state branching when it improves clarity and exhaustiveness.
- CDM-08: Place extension members in focused static classes and use them only for cohesive behavior that is naturally discoverable on the extended type.
- CDM-09: Do not call `DateTime.UtcNow` or `DateTime.Now` directly in domain logic; inject time via `TimeProvider` or an equivalent abstraction.
- CDM-10: Do not instantiate randomness directly in domain logic (for example, `new Random()`); inject a randomness abstraction where determinism matters.
- CDM-11 [MANDATORY]: Treat shared mutable state as unsafe by default and require an explicit concurrency strategy (for example, immutability, locks, or concurrent collections).
- CDM-12: Keep framework-specific code (for example, ASP.NET Core and EF Core) at boundary layers and keep core domain logic framework-agnostic.
