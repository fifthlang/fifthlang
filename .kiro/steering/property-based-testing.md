---
description: property-based-testing
inclusion: always
---
## Testing
- PBT-01 [MANDATORY]: The standard property based testing (PBT) stack is:
- `FsCheck` for property based testing
- `FsCheck.XUnit` for property based testing integration into xunit
- PBT-02 [MANDATORY]: Always default to Property Based Tests rather than Unit tests.
- PBT-03 [MANDATORY]: Property Based Tests should be the default approach for testing that a SUT is broadly correct,
- PBT-04 [MANDATORY]: Unit tests should be reserved for regression cases, to test a specific case that is known to have previously caused issues.
- PBT-05 [MANDATORY]: Never just test single-point scenarios and  happy paths, instead use a Property Based Tests that will test all positive, negative and edge cases.
- PBT-06 [MANDATORY]: Define properties as universal rules that must hold for all valid inputs, rather than relying on specific example cases.
- PBT-07 [MANDATORY]: Design generators to produce diverse, realistic, and edge-case inputs across the full input space.
- PBT-08 [MANDATORY]: Ensure failing cases can be minimized automatically through shrinking to aid debugging.
- PBT-09 [MANDATORY]: Specify preconditions clearly or constrain generators so properties are only evaluated in valid domains.
- PBT-10 [MANDATORY]: Keep tests deterministic and reproducible by controlling randomness and eliminating hidden state or side effects.
- PBT-11 [MANDATORY]: Use strong oracles, models, or metamorphic relationships to validate correctness beyond simple assertions.

- Every property must have an oracle: a mechanical way to decide pass/fail that is stronger than “doesn’t throw” or “looks plausible”.
- Prefer a reference (spec) model oracle when you can: compute expected behaviour using a simpler, obviously-correct implementation and compare.
- If you can’t compute the exact expected output, use a metamorphic oracle: apply a transformation to inputs and assert a predictable relationship between outputs.
- Use multiple weak oracles together (invariants + metamorphic + cross-check) rather than one weak check.
- Fail with evidence: when a property fails, ensure the counterexample is informative (shrinks well; includes classification/labels).
- PBT-12 [MANDATORY]: When making significant changes to a pre-existing unit test, convert it to a Property based test that tests a whole class of invariants and pre and post conditions. 
