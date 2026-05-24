---
description: csharp-dependency-injection-and-boundaries
inclusion: always
---
## Development
- CDB-01 [MANDATORY]: Define and consume abstractions (interfaces or equivalent contracts) at architectural boundaries where substitution or testability is required.
- CDB-02 [MANDATORY]: Use constructor injection for required dependencies; do not hide required dependencies behind property injection.
- CDB-03 [MANDATORY]: Choose DI lifetimes deliberately (for example, singleton for stateless shared services, scoped for request-bound services, transient for lightweight stateless services) and keep lifetime usage valid.
- CDB-04 [MANDATORY]: Do not instantiate service dependencies with `new` inside business logic when DI should provide them.
- CDB-05 [MANDATORY]: Use the .NET Options pattern (`IOptions<T>`, `IOptionsSnapshot<T>`, or `IOptionsMonitor<T>`) and bind configuration into typed options classes.
- CDB-06 [MANDATORY]: Define explicit serialization DTOs and keep them separate from behavior-rich domain types.
