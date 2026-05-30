---
description: property-based-testing
inclusion: always
---
## Testing
- PBT-01: When the test project targets .NET 8+, the property-based test suite shall use FsCheck.
- PBT-02: When the test project targets .NET 8+ and uses xUnit, the property-based test suite shall use FsCheck.Xunit integration.
- PBT-03 [MANDATORY]: For broad SUT-correctness testing, the test suite shall use property-based tests as the default test type.
- PBT-04 [MANDATORY]: For regression testing, the test suite shall use unit tests only for specific cases that previously caused issues.
- PBT-05 [MANDATORY]: Each property-based test shall cover positive, negative, and edge-case input classes.
- PBT-06 [MANDATORY]: Each property shall define a universal invariant that holds for all valid inputs.
- PBT-07 [MANDATORY]: Each generator shall produce diverse realistic values, including edge cases, across the input domain.
- PBT-08 [MANDATORY]: When a property fails, the framework shall automatically shrink the failing input to a minimal counterexample.
- PBT-09 [MANDATORY]: For each property, the test shall enforce valid-domain evaluation through explicit preconditions or constrained generators.
- PBT-10 [MANDATORY]: Each property-based test shall be deterministic and reproducible by controlling randomness and eliminating hidden state or side effects.
- PBT-11 [MANDATORY]: Each property shall use a mechanical oracle that decides pass or fail and is stronger than exception-only or plausibility checks.
- PBT-12 [MANDATORY]: When significantly changing a pre-existing unit test, the test suite shall convert it to a property-based test that verifies invariants and pre/postconditions across an input class.
- PBT-13 [MANDATORY]: When a simpler correct reference model is available, each property shall use it as the oracle.
- PBT-14 [MANDATORY]: When exact expected outputs are not computable, each property shall use a metamorphic oracle with a predictable output relationship.
- PBT-15 [MANDATORY]: When no single strong oracle is available, each property shall combine multiple weak oracles.
- PBT-16 [MANDATORY]: When a property fails, the test output shall include an informative counterexample that shrinks well and includes classification labels.
