---
description: performance-cpu-loops-and-vectorization
inclusion: always
---
## Performance
- PCV-001 [MANDATORY]: Hot loops shall remain simple, branch-light, and allocation-free where practical.
- PCV-002 [MANDATORY]: Hot-loop assumptions shall be explicit, and invariant checks shall be validated outside the loop when possible.
- PCV-003: SIMD, hardware intrinsics, or vectorized APIs shall be used only when measurement shows clear benefit and target-platform portability remains acceptable.
