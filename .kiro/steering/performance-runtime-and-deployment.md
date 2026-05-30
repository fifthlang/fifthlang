---
description: performance-runtime-and-deployment
inclusion: always
---
## Performance
- PRD-001 [MANDATORY]: Hot paths shall not use runtime reflection.
- PRD-002: When startup or throughput is materially improved, source generation shall be preferred over runtime reflection or runtime code discovery.
- PRD-003: Tiered compilation, quick JIT, or compilation settings shall be changed only when benchmark evidence justifies the change.
- PRD-004: When trimmed or Native AOT deployment is required, code paths incompatible with trimming or AOT shall be avoided and library surfaces shall remain trimming-compatible.
- PRD-005: Native AOT shall be adopted only when measured startup, memory, deployment, or scale benefits justify its constraints.
