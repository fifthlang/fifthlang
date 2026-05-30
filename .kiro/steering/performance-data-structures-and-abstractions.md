---
description: performance-data-structures-and-abstractions
inclusion: always
---
## Performance
- PDA-001 [MANDATORY]: Collection types shall be selected according to required lookup, iteration, mutation, and allocation characteristics.
- PDA-002 [MANDATORY]: Hot-path logic shall not enumerate the same sequence multiple times.
- PDA-003 [MANDATORY]: When measurements show cache-locality benefit, contiguous data-access patterns shall be preferred.
- PDA-004: Structs shall be used only when value semantics and measured allocation or locality benefits justify them; immutable frequently copied value types shall be readonly struct.
- PDA-005 [MANDATORY]: Immutability shall be the default design, and defensive copying on hot paths shall be used only when required for correctness.
- PDA-006 [MANDATORY]: Generic type-safe implementations shall be preferred over object-based abstractions when object-based designs materially increase boxing or allocation.
- PDA-007 [MANDATORY]: Hot paths shall avoid unnecessary virtual dispatch when an equally maintainable lower-overhead alternative exists.
- PDA-008 [MANDATORY]: Public APIs shall make efficient usage the default path and expensive behavior explicit.
