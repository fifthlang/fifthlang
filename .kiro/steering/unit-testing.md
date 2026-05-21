---
description: unit-testing
inclusion: always
---
## Testing
- UT-001: The standard unit testing stack for .NET is:

- `xUnit` as the test framework
- `FluentAssertions` for assertions
- UT-002 [MANDATORY]: All unit tests should be in a dedicated Unit Testing project.  
NEVER put tests in the same project as the code being tested.
- UT-003 [MANDATORY]: All tests projects should be located under a root folder called `tests/`.
- UT-004 [MANDATORY]: Unit testing should only be used when capturing and testing specific cases that have previously failed under PBT.
