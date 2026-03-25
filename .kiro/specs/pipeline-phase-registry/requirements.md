# Requirements Document

## Introduction

Replace the monolithic `FifthParserManager.ApplyLanguageAnalysisPhases` method (~450 lines, 33 sequential `if (upTo >= AnalysisPhase.X)` blocks) with a composable pipeline architecture. Each compiler phase becomes a class implementing `ICompilerPhase`, registered in a `TransformationPipeline` orchestrator that handles execution, dependency validation, timing, and diagnostics. The pipeline is configured via `PipelineOptions` (replacing the `upTo` enum parameter) and returns structured `PhaseResult` values (replacing the null-return-for-error convention). A shared `PhaseContext` flows through all phases, carrying the symbol table, type registry, and shared data.

This merges the pragmatic REM-008 registry approach with the full ISSUE-005 composable pipeline vision. Backward compatibility is relaxed where it serves the greater good — all callers will be migrated directly to the new API, and the old monolithic method will be deleted.

## Glossary

- **Pipeline**: The ordered sequence of compiler analysis/transformation phases executed by `TransformationPipeline.Execute`.
- **Phase**: A single named step in the Pipeline that transforms or validates an `AstThing`, implemented as a class implementing `ICompilerPhase`.
- **ICompilerPhase**: The interface that all pipeline phases implement. Defines `Name`, `DependsOn`, `ProvidedCapabilities`, and a `Transform` method.
- **PhaseContext**: A shared context object passed through all phases, carrying `ISymbolTable`, `ITypeRegistry`, diagnostics, target framework, and extensible shared data.
- **PhaseResult**: A structured return type from phase execution: `(AstThing TransformedAst, IReadOnlyList<Diagnostic> Diagnostics, bool Success)`.
- **TransformationPipeline**: The orchestrator class that registers phases, validates dependencies, and executes the pipeline.
- **PipelineOptions**: Configuration object controlling pipeline execution: `SkipPhases`, `StopAfter`, `StopOnError`, `EnableCaching`, `DumpAfter`.
- **AstThing**: The root AST node type passed through every phase.
- **Diagnostic**: A compiler diagnostic (error, warning, info) emitted during phase execution.
- **Early_Exit**: A point in the Pipeline where execution halts because error-level Diagnostics have been detected and `StopOnError` is true.
- **Compound_Phase**: A Phase whose `Transform` method runs multiple visitors/rewriters in sequence (e.g., TypeAnnotation runs type annotation, augmented assignment lowering, error collection, and graph triple operator lowering with re-linking).
- **Phase_Timing**: A `Stopwatch`-based measurement of wall-clock time spent in each Phase, emitted to stderr when `FIFTH_DEBUG=1`.
- **Capability**: A string token declared by a phase via `ProvidedCapabilities`, representing a semantic guarantee (e.g., `"TreeStructure"`, `"Symbols"`, `"Types"`). Other phases can declare dependencies on capabilities.
- **AnalysisPhase (removed)**: The existing enum that will be deleted along with the monolithic `ApplyLanguageAnalysisPhases` method. Callers that referenced enum values will be migrated to use string-based phase names with `PipelineOptions.StopAfter`.

## Requirements

### Requirement 1: ICompilerPhase Interface

**User Story:** As a compiler developer, I want each pipeline phase to be a proper class with explicit metadata, so that phases are self-documenting, independently testable, and composable.

#### Acceptance Criteria

1. THE system SHALL define an `ICompilerPhase` interface with properties: `string Name`, `IReadOnlyList<string> DependsOn`, `IReadOnlyList<string> ProvidedCapabilities`, and a method `PhaseResult Transform(AstThing ast, PhaseContext context)`.
2. THE `Name` property SHALL return a unique human-readable identifier for the phase (e.g., `"TreeLinkage"`, `"TypeAnnotation"`).
3. THE `DependsOn` property SHALL return the list of capability strings that must be provided by earlier phases before this phase can execute.
4. THE `ProvidedCapabilities` property SHALL return the list of capability strings that this phase provides to subsequent phases.
5. WHEN a phase has no dependencies, THE `DependsOn` property SHALL return an empty list.

### Requirement 2: PhaseResult Structured Return Type

**User Story:** As a compiler developer, I want phase execution to return a structured result instead of null-for-error, so that error handling is explicit and composable.

#### Acceptance Criteria

1. THE system SHALL define a `PhaseResult` record type containing: `AstThing TransformedAst`, `IReadOnlyList<Diagnostic> Diagnostics`, and `bool Success`.
2. WHEN a phase completes successfully, THE `PhaseResult` SHALL have `Success = true` and `TransformedAst` set to the transformed AST.
3. WHEN a phase encounters errors, THE `PhaseResult` SHALL have `Success = false`, `Diagnostics` populated with the errors, and `TransformedAst` set to the last valid AST state.
4. WHEN a phase produces warnings or info diagnostics without errors, THE `PhaseResult` SHALL have `Success = true` with the diagnostics included.

### Requirement 3: PhaseContext Shared Context

**User Story:** As a compiler developer, I want a shared context object passed through all phases, so that phases can share data (symbol tables, type registries) without global state or parameter threading.

#### Acceptance Criteria

1. THE system SHALL define a `PhaseContext` class containing: a `List<Diagnostic> Diagnostics` collection, a `string? TargetFramework` property, a `Dictionary<string, object> SharedData` dictionary for extensible inter-phase communication, and a `bool EnableCaching` flag.
2. THE `PhaseContext` SHALL be created by the `TransformationPipeline` before execution begins and passed to each phase's `Transform` method.
3. WHEN a phase needs to share data with subsequent phases (e.g., symbol table, type annotation results), IT SHALL store the data in `PhaseContext.SharedData` using a well-known string key.
4. THE `PhaseContext.Diagnostics` list SHALL accumulate diagnostics from all phases during pipeline execution.
5. THE `PhaseContext.TargetFramework` SHALL be set from the caller-provided value and be accessible to all phases.

### Requirement 4: TransformationPipeline Orchestrator

**User Story:** As a compiler developer, I want a pipeline orchestrator class that manages phase registration, dependency validation, and execution, so that the pipeline is configurable and extensible.

#### Acceptance Criteria

1. THE system SHALL define a `TransformationPipeline` class with methods: `RegisterPhase(ICompilerPhase phase)`, `Execute(AstThing ast, PipelineOptions options)`, and a `Phases` property exposing the registered phases.
2. WHEN `RegisterPhase` is called, THE pipeline SHALL validate that all capabilities listed in the phase's `DependsOn` are provided by previously registered phases, and throw `InvalidOperationException` if any dependency is unsatisfied.
3. THE `Execute` method SHALL iterate registered phases in registration order, calling each phase's `Transform` method with the current AST and `PhaseContext`.
4. THE `Execute` method SHALL return a `PipelineResult` containing: the final `AstThing`, the aggregated `IReadOnlyList<Diagnostic>`, a `bool Success` flag, and a `Dictionary<string, TimeSpan> PhaseTimings` when timing is enabled.
5. THE `TransformationPipeline` SHALL support a static `CreateDefault()` factory method that returns a pipeline pre-configured with all standard Fifth compiler phases in the correct order.
6. THE `Phases` property SHALL expose the registered phases as `IReadOnlyList<ICompilerPhase>` for inspection by tests and tooling.

### Requirement 5: PipelineOptions Configuration

**User Story:** As a compiler developer, I want to configure pipeline execution with skip, stop-after, and debugging options, so that I can run partial pipelines for testing, LSP, and debugging.

#### Acceptance Criteria

1. THE system SHALL define a `PipelineOptions` record type containing: `HashSet<string> SkipPhases`, `string? StopAfter`, `bool StopOnError` (default true), `bool EnableCaching` (default false), and `HashSet<string>? DumpAfter`.
2. WHEN `SkipPhases` contains a phase name, THE pipeline SHALL skip that phase during execution.
3. WHEN `StopAfter` is set to a phase name, THE pipeline SHALL stop execution after that phase completes (inclusive).
4. WHEN `StopOnError` is true and a phase returns `Success = false`, THE pipeline SHALL stop execution and return the accumulated result.
5. WHEN `DumpAfter` contains a phase name, THE pipeline SHALL invoke a configurable AST dump callback after that phase completes, enabling debugging inspection of intermediate AST states.
6. THE `PipelineOptions` SHALL provide a static `Default` property that returns options equivalent to running all phases with `StopOnError = true`.

### Requirement 6: Concrete Phase Implementations

**User Story:** As a compiler developer, I want each of the existing 33 pipeline phases wrapped as an `ICompilerPhase` class, so that the entire pipeline is expressed in the new architecture.

#### Acceptance Criteria

1. THE system SHALL provide an `ICompilerPhase` implementation for each of the 33 phases currently in the `AnalysisPhase` enum (excluding `None` and `All`), preserving the exact execution semantics of the current monolithic method.
2. EACH phase class SHALL declare appropriate `DependsOn` capabilities reflecting the actual dependencies in the current pipeline (e.g., `SymbolTablePhase` depends on `"TreeStructure"` and `"Builtins"`).
3. EACH phase class SHALL declare appropriate `ProvidedCapabilities` reflecting what the phase provides (e.g., `TreeLinkagePhase` provides `"TreeStructure"`).
4. WHEN a phase in the current method runs multiple visitors in sequence (a Compound_Phase), THE corresponding `ICompilerPhase` implementation SHALL encapsulate the full compound logic within its `Transform` method.
5. THE system SHALL mark TailCallOptimization as a disabled/skipped phase in the default pipeline configuration, matching the current commented-out state.

### Requirement 7: Explicit Dependency Declarations

**User Story:** As a compiler developer, I want each phase to explicitly declare what it depends on and what it provides, so that dependency violations are caught at registration time rather than at runtime.

#### Acceptance Criteria

1. WHEN a phase is registered with `RegisterPhase`, THE pipeline SHALL verify that every capability in `DependsOn` is provided by at least one previously registered phase.
2. WHEN a dependency is not satisfied, THE pipeline SHALL throw an `InvalidOperationException` with a message identifying the missing capability and the phase that requires it.
3. THE dependency validation SHALL occur at registration time, not at execution time, so that misconfigured pipelines fail fast.
4. THE system SHALL allow phases with empty `DependsOn` to be registered at any position (they have no dependencies to validate).

### Requirement 8: Per-Phase Timing via FIFTH_DEBUG

**User Story:** As a compiler developer, I want per-phase wall-clock timing emitted when `FIFTH_DEBUG=1`, so that I can identify slow phases during development and profiling.

#### Acceptance Criteria

1. WHILE `FIFTH_DEBUG=1` is set in the environment, THE pipeline SHALL measure the wall-clock execution time of each phase using `System.Diagnostics.Stopwatch`.
2. WHILE `FIFTH_DEBUG=1` is set in the environment, THE pipeline SHALL emit a line to stderr for each executed phase in the format: `[PHASE] <PhaseName> completed in <elapsed_ms>ms`.
3. WHILE `FIFTH_DEBUG=1` is set in the environment, THE pipeline SHALL emit a summary line to stderr after all phases complete in the format: `[PIPELINE] Total: <total_ms>ms (<phase_count> phases executed)`.
4. WHEN `FIFTH_DEBUG` is not set or is not `1`/`true`/`on`, THE pipeline SHALL not perform timing measurements or emit timing output.
5. THE `PipelineResult.PhaseTimings` dictionary SHALL be populated with per-phase timing data regardless of debug mode, so that callers (e.g., LSP) can access timing programmatically.

### Requirement 9: Phase Skip and Stop-After Capabilities

**User Story:** As a compiler developer, I want to skip specific phases or stop after a named phase, so that I can run partial pipelines for testing and LSP scenarios.

#### Acceptance Criteria

1. WHEN `PipelineOptions.SkipPhases` contains a phase name, THE pipeline SHALL skip that phase and not invoke its `Transform` method.
2. WHEN `PipelineOptions.StopAfter` is set, THE pipeline SHALL execute phases up to and including the named phase, then stop.
3. WHEN a skipped phase provides capabilities that later phases depend on, THE pipeline SHALL still allow execution (the caller is responsible for ensuring correctness when skipping phases).
4. THE `StopAfter` option SHALL accept phase names as strings (e.g., `"TypeAnnotation"`, `"GuardValidation"`), replacing the current `AnalysisPhase` enum-based `upTo` parameter.

### Requirement 10: AST Dump Hooks Between Phases

**User Story:** As a compiler developer, I want to dump the AST state after specific phases for debugging, so that I can inspect intermediate transformations without modifying pipeline code.

#### Acceptance Criteria

1. WHEN `PipelineOptions.DumpAfter` contains a phase name, THE pipeline SHALL invoke a dump callback with the current AST and phase name after that phase completes.
2. THE dump callback SHALL be configurable on `PipelineOptions` as an `Action<AstThing, string>?` delegate (phase name passed as second argument).
3. WHEN no dump callback is configured but `DumpAfter` is set, THE pipeline SHALL use a default callback that writes to stderr via `DebugHelpers.DebugLog`.

### Requirement 11: Phase Isolation for Testing

**User Story:** As a compiler developer, I want to test individual phases with only their declared dependencies, so that I can write focused unit tests without running the entire pipeline.

#### Acceptance Criteria

1. THE `TransformationPipeline` SHALL support registering a subset of phases (not just the full default set), enabling test pipelines with only specific phases.
2. WHEN a test creates a `TransformationPipeline` and registers only specific phases, THE pipeline SHALL execute only those phases in registration order.
3. THE `ICompilerPhase` interface SHALL be implementable by test doubles (mocks/stubs) for testing phases in isolation with controlled inputs.
4. EACH concrete phase class SHALL be independently constructable without requiring the full pipeline infrastructure.

### Requirement 12: Caller Migration and Legacy Removal

**User Story:** As a compiler developer, I want all existing callers of `ApplyLanguageAnalysisPhases` migrated to the new `TransformationPipeline` API and the old monolithic method deleted, so that the codebase has a single pipeline implementation with no dead code.

#### Acceptance Criteria

1. THE `Compiler.cs` `TransformPhase` method SHALL be updated to use `TransformationPipeline.CreateDefault().Execute(ast, options)` instead of calling `FifthParserManager.ApplyLanguageAnalysisPhases` directly.
2. THE `ParsingService.cs` SHALL be updated to use the new pipeline API.
3. THE `SymbolService.cs` SHALL be updated to use the new pipeline API.
4. THE `ParseHarness.cs` (both copies in `test/TestInfrastructure/` and `test/ast-tests/TestInfrastructure/`) SHALL be updated to use the new pipeline API, translating `ParseOptions.Phase` to `PipelineOptions.StopAfter` using string-based phase names.
5. ALL direct test file calls to `FifthParserManager.ApplyLanguageAnalysisPhases` SHALL be migrated to the new pipeline API.
6. THE monolithic `ApplyLanguageAnalysisPhases` method SHALL be deleted from `FifthParserManager` after all callers are migrated.
7. THE `AnalysisPhase` enum SHALL be deleted from `FifthParserManager` after all callers are migrated to string-based phase names.
8. THE `ExecutePhase` helper method SHALL be deleted from `FifthParserManager` (error handling is now encapsulated within each phase's `Transform` method).

### Requirement 13: Error Handling

**User Story:** As a compiler developer, I want the new pipeline to handle phase execution errors with the same fidelity as the current implementation, preserving error reporting and recovery behaviour.

#### Acceptance Criteria

1. WHEN a phase's `Transform` method throws an exception, THE pipeline SHALL catch it, log the error (phase name, exception type, message, stack trace) to stderr, add an error diagnostic, and either re-throw or continue based on `StopOnError`.
2. WHEN the TreeLinkage phase throws an exception, THE pipeline SHALL handle it with the same specialised logging that exists today (logging to `DebugHelpers.DebugLog` and adding to diagnostics before re-throwing).
3. WHEN the PropertyToField phase throws an exception, THE pipeline SHALL handle it with the same specialised logging (logging to `DebugHelpers.DebugLog` before re-throwing, without adding to diagnostics).
4. THE pipeline SHALL support phase-specific error handling by allowing phases to implement custom exception handling within their `Transform` method.

### Requirement 14: Compound Phase Encapsulation

**User Story:** As a compiler developer, I want compound phases (phases that run multiple visitors) to be encapsulated within a single `ICompilerPhase` class, so that the pipeline remains a flat list while preserving complex phase logic.

#### Acceptance Criteria

1. THE ClassCtors phase SHALL encapsulate both `ClassCtorInserter` and the subsequent `TreeLinkageVisitor` re-link within its `Transform` method.
2. THE DestructurePatternFlatten phase SHALL encapsulate both `DestructuringVisitor` and `DestructuringConstraintPropagator` within its `Transform` method.
3. THE TypeAnnotation phase SHALL encapsulate the full compound logic: type annotation visitor execution, symbol table rebuild, augmented assignment lowering, type error collection, graph triple operator lowering with re-linking/re-annotation, and second-pass type error collection.
4. THE LambdaClosureConversion phase SHALL encapsulate lambda capture validation, closure conversion rewriting, and tree re-linking within its `Transform` method.
5. THE Defunctionalisation phase SHALL encapsulate defunctionalisation rewriting and tree re-linking within its `Transform` method.

### Requirement 15: Pipeline Queryability

**User Story:** As a compiler developer, I want to query the pipeline programmatically, so that tooling, tests, and the LSP server can inspect what phases exist and their properties.

#### Acceptance Criteria

1. THE `TransformationPipeline.Phases` property SHALL expose the complete ordered list of registered `ICompilerPhase` instances.
2. WHEN a caller reads the `Phases` property, THE pipeline SHALL return all registered phases including any that would be skipped by current `PipelineOptions`.
3. THE `ICompilerPhase` interface SHALL be a public type accessible to other projects in the solution (tests, language server, compiler).
4. THE pipeline SHALL expose a method to query which capabilities are available after a given phase name, enabling tooling to understand phase dependencies.

### Requirement 16: Validation Phases After TypeAnnotation

**User Story:** As a compiler developer, I want the post-TypeAnnotation validation phases (ExternalCallValidation, TryCatchFinallyValidation) to be registered as proper `ICompilerPhase` implementations.

#### Acceptance Criteria

1. THE pipeline SHALL include an ExternalCallValidation phase that runs `ExternalCallValidationVisitor` after TypeAnnotation, with `StopOnError` behaviour on error diagnostics.
2. THE pipeline SHALL include a TryCatchFinallyValidation phase that runs `TryCatchFinallyValidationVisitor` after ExternalCallValidation, with `StopOnError` behaviour on error diagnostics.
3. WHEN the `PhaseContext.Diagnostics` is not being collected (null-equivalent scenario), THE ExternalCallValidation and TryCatchFinallyValidation phases SHALL be skipped, matching the current `if (diagnostics != null)` guard.

### Requirement 17: Phase Ordering Verification Tests

**User Story:** As a compiler developer, I want tests that verify the default pipeline structure, so that accidental reordering or missing phases are caught immediately.

#### Acceptance Criteria

1. THE test suite SHALL contain a test that verifies the default pipeline's phase ordering matches the expected sequence.
2. THE test suite SHALL contain a test that verifies every expected phase has a corresponding `ICompilerPhase` registration in the default pipeline.
3. THE test suite SHALL contain a test that verifies no duplicate phase names exist in the default pipeline.
4. THE test suite SHALL contain a test that verifies all declared dependencies are satisfied by earlier phases in the default pipeline.

### Requirement 18: Future Extension Points

**User Story:** As a compiler developer, I want the architecture to support future caching and parallel execution without requiring further refactoring, so that these optimisations can be added incrementally.

#### Acceptance Criteria

1. THE `PhaseContext.EnableCaching` flag SHALL be respected by the pipeline infrastructure, even if caching is not implemented in this iteration (the flag is plumbed through for future use).
2. THE explicit dependency declarations on phases SHALL be sufficient to determine which phases are independent and could theoretically run in parallel (future work, not implemented now).
3. THE `PipelineOptions.EnableCaching` flag SHALL be forwarded to `PhaseContext.EnableCaching`.
4. THE architecture SHALL NOT preclude adding a `ParallelTransformationPipeline` subclass in the future that uses dependency information to parallelise independent phases.
