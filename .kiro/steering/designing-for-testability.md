---
description: designing-for-testability
inclusion: always
---
## Testability
- TD-001: Inject dependencies that can be mocked. 
- TD-002: Prefer Interface registration in DI containers, rather than registering concrete types.
- TD-003: When registering complex objects for injection elsewhere, create an Interface for the behaviour being injected so that the interface can be the point of dependency.
