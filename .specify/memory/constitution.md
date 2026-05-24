# Engineering Constitution
## Agentic
- AI-001 [MANDATORY]: Never leave temporary files in the repo after they are no longer in use.
- AI-002 [MANDATORY]: When developing diagnostic, experimental or temporary test code, make sure it is created and run somewhere other than the source repo.  This prevents dead code being left lying around.
## Prerequisites
- BUILD-001 [MANDATORY]: The local environment must include:
  
  - .NET 10.0 SDK pinned by `global.json` to `10.0.100`
  - Java 17 or newer for ANTLR grammar compilation
  - The ANTLR runtime package and the jar at `src/parser/tools/antlr-4.13.2-complete.jar`
## Commands
- BUILD-002: Use the solution restore command as the standard restore entry point:
  
  ```bash
  dotnet restore fifthlang.sln
  ```
  
  This operation typically takes about 70 seconds. Do not cancel it. Use a timeout of at least 120 seconds when automation controls execution time.
- BUILD-003: Use the solution build command as the standard build entry point:
  
  ```bash
  dotnet build fifthlang.sln
  ```
  
  This operation typically takes about 60 seconds. Do not cancel it. Use a timeout of at least 120 seconds when automation controls execution time.
## Testing
- BUILD-004: Use the solution test command as the default regression gate:
  
  ```bash
  dotnet test fifthlang.sln
  ```
  
  Do not cancel this run. Use a timeout of at least 5 minutes when automation controls execution time.
- BUILD-005: Use this command for a quick smoke-test subset while iterating locally:
  
  ```bash
  dotnet test test/ast-tests/ast_tests.csproj
  ```
- BUILD-014: These targeted commands are available for narrower local validation:
  
  ```bash
  just test-ast
  just test-runtime
  just test-syntax
  just test-all-roslyn
  ```
  
  Use them to iterate locally, but retain the full solution test run as the regression gate.
- PBT-01 [MANDATORY]: The standard property based testing (PBT) stack is:
  - `FsCheck` for property based testing
  - `FsCheck.XUnit` for property based testing integration into xunit
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
- PR-002: Pull requests that change behavior must add or update tests, and all relevant suites must pass locally.
- TDD-001 [MANDATORY]: You MUST practice test-first development.  Follow the process of "Red-Green-Refactor"

  The Rules of TDD are:
  - Start with a PBT test that fails.
  - Make the smallest change needed to make that test pass.
  - Keep each step tiny so you focus on one thing at a time.

  Never get a failing test to pass by masking its failure.  Only a valid addition of functionality counts.
- TDD-002 [MANDATORY]: Test code should be developed first, NEVER in retrospect.
- TDD-003 [MANDATORY]: Observe a test failing first, before implementing the application code that makes it pass.
- TDD-004 [MANDATORY]: When a test finally passes, refactor the new code to ensure it is clean and has no technical debt.
- UTR-002: Avoid testing internal implementation details and avoid depending on concrete implementations where looser behavioral validation is possible.
- UTR-003: Never mask failing tests with broad `try` or `catch` blocks or "success assertions".
- UTR-004: Failing tests indentify outstanding work and should never be suppressed
## Generation
- BUILD-006: After metamodel changes, regenerate AST output with:
  
  ```bash
  dotnet run --project src/ast_generator/ast_generator.csproj -- --folder src/ast-generated
  ```
- BUILD-011: AST code generation runs automatically before compilation via MSBuild targets. Manual generation is primarily for focused regeneration workflows.
- GEN-001: The AST generator is authoritative for all files under `src/ast-generated/`. These files must never be hand-edited.
- GEN-002: The primary generated files are:
  
  - `builders.generated.cs` for builder pattern classes
  - `visitors.generated.cs` for visitor pattern classes
  - `rewriter.generated.cs` for lowering-oriented rewriters
  - `typeinference.generated.cs` for type inference support
- PR-003: Do not hand-edit `src/ast-generated/`. If the metamodel changes, include the required regeneration steps in the pull request.
- PR-015: Generated code changes must follow metamodel versioning rules.
## Verification
- BUILD-007: Confirm the toolchain before debugging restore or build failures:
  
  ```bash
  dotnet --version
  java -version
  ```
  
  The .NET command should report `10.0.x`, and Java should report version 17 or newer.
## Dependency
- BUILD-008: The effective build order is:
  
  ```text
  ast-model -> ast_generator -> ast-generated -> parser -> compiler -> tests
  ```
  
  Always build the full solution rather than individual projects so dependency ordering is resolved correctly.
- PIPE-003: Transformation passes are order-dependent, and later passes may rely on invariants established by earlier passes.
## Workflow
- BUILD-009: Do not cancel restore, build, test, or generation operations. The documented timings in this repository are normal and expected.
- GEN-003: To change generated AST output:
  
  1. Edit `src/ast-model/AstMetamodel.cs`
  2. Update templates under `src/ast_generator/Templates/` when template behavior must change
  3. Regenerate with `just run-generator`
  4. Build the full solution with `dotnet build fifthlang.sln`
- GRAM-002: When grammar behavior changes:
  
  1. Edit `FifthLexer.g4` for tokens and keywords and `FifthParser.g4` for syntax rules as needed
  2. Update `AstBuilderVisitor.cs` for the new syntax constructs
  3. Add test samples under `src/parser/grammar/test_samples/*.5th`
  4. Rely on the normal build to run ANTLR compilation automatically
  5. Run parser tests with `dotnet test test/syntax-parser-tests/ -v minimal`
  6. Run the full regression suite with `dotnet test fifthlang.sln`
- PIPE-008: To add a new transformation:
  
  1. Create the visitor or rewriter in `src/compiler/LanguageTransformations/`
  2. Choose the correct base pattern using the code-generation steering guidance
  3. Register the transformation in `src/compiler/ParserManager.cs`
  4. Add tests in `test/ast-tests/` or `test/runtime-integration-tests/`
  5. Build the solution and run the full test suite
## Parser
- BUILD-010: ANTLR grammar compilation happens automatically during the parser project build. Do not add redundant manual generation steps to the normal workflow.
- PR-004: Any grammar change must have corresponding updates in both the parser grammar and the AST builder visitor.
- PR-017: When adding or updating `.5th` examples or test programs:
  
  1. Validate parsing locally with parser or syntax tests
  2. Use grammar-supported forms only and avoid legacy shorthand
  3. Add `CopyToOutputDirectory` metadata in the test `.csproj` when an integration test consumes the sample
  4. Run the relevant integration tests before committing
  5. Update the grammar files and constitution if the change intentionally introduces new surface syntax
## Diagnostics
- BUILD-012: The following warnings are expected and safe to ignore unless they change unexpectedly:
  
  - ANTLR `assoc` option warnings
  - C# nullable reference warnings
  - Switch exhaustiveness warnings
## Validation
- BUILD-013: After any change, validate in this order:
  
  1. `dotnet build fifthlang.sln`
  2. `dotnet test fifthlang.sln`
  3. Verify runtime behavior
  
  Compilation alone is not sufficient validation.
- GRAM-003 [MANDATORY]: All `.5th` files in `docs/`, `specs/`, `test/`, and `src/parser/grammar/test_samples/` must parse with the current grammar. CI enforces this with the `Validate .5th samples (parser-check)` step.
  
  Run `just validate-examples` locally before committing.
- GRAM-008: Intentionally invalid files are excluded from example validation by these heuristics:
  
  - directory matches under `*/Invalid/*`
  - filenames containing `invalid`
  - an explicit negative-test comment marker in the file
  
  For debugging, force validation of negative examples with:
  
  ```bash
  dotnet run --project src/tools/validate-examples/validate-examples.csproj -- --include-negatives
  ```
- PR-001: Before opening or updating a pull request:
  
  1. Build the full solution with `dotnet build fifthlang.sln` and do not cancel the run
  2. Run the full test suite with `dotnet test fifthlang.sln`
  3. Validate grammar examples with `just validate-examples`
## Development
- CAR-01 [MANDATORY]: Use `async` and `await` for naturally asynchronous operations and return `Task` or `Task<T>` by default.
- CAR-02 [MANDATORY]: Accept a `CancellationToken` in cancellable async APIs and pass it through to all cancellable downstream calls.
- CAR-03 [MANDATORY]: Do not block async flows with `.Result`, `.Wait()`, or `.GetAwaiter().GetResult()`; await tasks instead.
- CAR-04: Use `ValueTask` only in measured hot paths where synchronous completion is common and allocation reduction is proven.
- CAR-05 [MANDATORY]: Use clear LINQ and collection code by default, but materialize sequences when needed to avoid hidden multiple enumeration (for example, `ToList()` before multiple passes).
- CAR-06 [MANDATORY]: Dispose owned resources deterministically with `using` or `await using`.
- CAR-07 [MANDATORY]: Never dispose dependencies resolved from the DI container; disposal is owned by the container lifecycle.
- CAR-08: Use `Span<T>` and `ReadOnlySpan<T>` only in measured hot paths where they clearly reduce allocations without harming maintainability.
- CDB-01 [MANDATORY]: Define and consume abstractions (interfaces or equivalent contracts) at architectural boundaries where substitution or testability is required.
- CDB-02 [MANDATORY]: Use constructor injection for required dependencies; do not hide required dependencies behind property injection.
- CDB-03 [MANDATORY]: Choose DI lifetimes deliberately (for example, singleton for stateless shared services, scoped for request-bound services, transient for lightweight stateless services) and keep lifetime usage valid.
- CDB-04 [MANDATORY]: Do not instantiate service dependencies with `new` inside business logic when DI should provide them.
- CDB-05 [MANDATORY]: Use the .NET Options pattern (`IOptions<T>`, `IOptionsSnapshot<T>`, or `IOptionsMonitor<T>`) and bind configuration into typed options classes.
- CDB-06 [MANDATORY]: Define explicit serialization DTOs and keep them separate from behavior-rich domain types.
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
- CEL-01 [MANDATORY]: Validate method arguments at the boundary with guard clauses (for example, `ArgumentNullException.ThrowIfNull`) and fail fast on invalid inputs.
- CEL-02 [MANDATORY]: Throw exceptions only for exceptional conditions; use explicit return results for expected control flow outcomes.
- CEL-03 [MANDATORY]: Preserve diagnostic context when propagating exceptions: use `throw;` to rethrow and include the original exception as `InnerException` when wrapping.
- CEL-04 [MANDATORY]: Use structured logging with message templates and named properties (for example, `logger.LogInformation("Processed {OrderId}", orderId)`).
- CEL-05 [MANDATORY]: Never log secrets, credentials, tokens, or other sensitive values; log redacted or hashed identifiers where traceability is required.
- CEL-06 [MANDATORY]: Write comments only for intent, invariants, and non-obvious decisions; do not restate what the code already expresses clearly.
- CEL-07 [MANDATORY]: Document public APIs with XML documentation when intent, contracts, or failure behavior are not obvious from the signature.
- CEL-08 [MANDATORY]: Keep source-generated and hand-written code clearly separated (for example, generated files under a dedicated generated path, with partial types used only at intentional boundaries).
- CEL-09 [MANDATORY]: Do work that may fail off to the side before changing committed state. First prepare all risky work using temporary state; then commit using steps that cannot fail or are tightly controlled.
- CEL-10 [MANDATORY]: Prefer commit-or-rollback behavior when practical. A failed operation should ideally leave the observable state exactly as it was before the operation began
- CFT-01 [MANDATORY]: Set `TargetFramework` to `net10.0` in every production `.csproj` unless a documented compatibility constraint requires a different target.
- CFT-02 [MANDATORY]: Enable nullable reference types in every C# project by setting `<Nullable>enable</Nullable>` in the project file.
- CFT-03 [MANDATORY]: Set `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>` and allow suppression only for explicitly documented warning IDs.
- CFT-04 [MANDATORY]: Enable .NET analyzers in all projects and define analyzer severity centrally in a source-controlled root `.editorconfig`.
- CFT-05 [MANDATORY]: Define repository-wide C# style in a root `.editorconfig` and enforce it in CI (for example, `dotnet format --verify-no-changes`).
- CFT-06 [MANDATORY]: Apply consistent naming conventions through `.editorconfig` naming rules and use intention-revealing names for types, members, parameters, and locals.
- CFT-07 [MANDATORY]: Express nullability intent explicitly in API signatures using nullable annotations (for example, `string?`) and related nullability attributes where needed.
- CFT-08 [MANDATORY]: Do not suppress nullable warnings (for example, with `!` or pragma directives) unless the exact reason is documented at the call site.
- CFT-09 [MANDATORY]: Default to the narrowest visibility (`private` or `internal`) and widen to `public` only when a real consumer requires it.
## Concurrency
- CONC-01 [MANDATORY]: Measure throughput, latency, queue depth, allocation rate, and thread usage before changing concurrency design.
- CONC-02 [MANDATORY]: Classify each hot operation explicitly as I/O-bound, CPU-bound, or coordination-bound before choosing a concurrency model.
- CONC-03 [MANDATORY]: Use async and await for naturally asynchronous I/O-bound work.
- CONC-04 [MANDATORY]: Do not block on Task, ValueTask, or async waits in throughput-sensitive code paths.
- CONC-05 [MANDATORY]: Accept and propagate CancellationToken in long-running, queued, or potentially blocking operations.
- CONC-06 [MANDATORY]: Treat cancellation as a normal control outcome, not as an unexpected fault.
- CONC-07 [MANDATORY]: Apply explicit timeouts or deadlines to remote, queued, or potentially blocking work.
- CONC-08 [MANDATORY]: Set an explicit concurrency limit for every fan-out, queue consumer pool, or parallel work stage.
- CONC-09 [MANDATORY]: When consumers have finite capacity, use bounded queues or bounded channels rather than unbounded buffering.
- CONC-10: Use Channel<T> for asynchronous producer-consumer pipelines that require explicit bounding or backpressure.
- CONC-11 [MANDATORY]: Do not allow unbounded queue growth in steady-state production workloads.
- CONC-12 [MANDATORY]: Reject, defer, shed, or throttle work when the system cannot process more safely.
- CONC-13 [MANDATORY]: Every started Task must be awaited, observed, or deliberately detached with explicit error handling.
- CONC-14 [MANDATORY]: Handle exceptions at the boundary that owns the concurrent operation or work queue.
- CONC-15 [MANDATORY]: When running multiple tasks concurrently, define whether partial failure cancels siblings, waits for all, or collects all results.
- CONC-16 [MANDATORY]: Do not create more concurrent work items than the stage can complete within its latency and memory budget.
- CONC-17: Use Task.Run only to move CPU-bound work off a caller thread when that separation is required.
- CONC-18 [MANDATORY]: Do not wrap naturally asynchronous I/O in Task.Run.
- CONC-19 [MANDATORY]: Do not use Task.Wait, Result, or GetAwaiter().GetResult() in throughput-sensitive code paths.
- CONC-20: Use Parallel.For, Parallel.ForEach, or equivalent data parallelism only for CPU-bound work with enough work per item to amortize scheduling overhead.
- CONC-21 [MANDATORY]: Partition CPU-bound work explicitly so that independent units can run without shared mutable state.
- CONC-22 [MANDATORY]: Prefer message passing, ownership transfer, or immutable data over shared mutable state.
- CONC-23 [MANDATORY]: Choose the weakest synchronization mechanism that preserves correctness.
- CONC-24 [MANDATORY]: Use Interlocked or other atomic primitives for simple counters, flags, and state transitions instead of coarse locks.
- CONC-25 [MANDATORY]: Keep lock scope minimal and free of I/O, blocking waits, and long-running work.
- CONC-26 [MANDATORY]: Do not await inside a monitor lock.
- CONC-27 [MANDATORY]: Design lock acquisition order explicitly whenever more than one lock can be taken in the same workflow.
- CONC-28 [MANDATORY]: Remove hot-path lock contention before increasing thread count or parallelism.
- CONC-29 [MANDATORY]: Use concurrent collections for shared multi-threaded collection access instead of manually locking non-thread-safe collections.
- CONC-30 [MANDATORY]: Do not mix external locking with concurrent collections unless the combined protocol is explicitly documented and tested.
- CONC-31 [MANDATORY]: Give each pipeline stage one clear responsibility, one input contract, and one output contract.
- CONC-32 [MANDATORY]: Do not assume completion order matches submission order unless the design enforces it.
- CONC-33 [MANDATORY]: Queued or retryable work must be idempotent or duplicate-safe.
- CONC-34 [MANDATORY]: Do not increase throughput by blind retry; classify failure and apply bounded retry only where safe.
- CONC-35 [MANDATORY]: Do not create dedicated Thread instances for routine asynchronous or short-lived background work.
- CONC-36: Use a dedicated thread only when a workload has a clear long-running affinity that is not appropriate for ThreadPool scheduling.
- CONC-37 [MANDATORY]: Do not use Thread.Abort.
- CONC-38 [MANDATORY]: Stop concurrent work cooperatively through CancellationToken or equivalent explicit control.
- CONC-39: Do not change ThreadPool minimum or maximum thread settings unless measurement shows the default behavior is the bottleneck.
- CONC-40: Change .NET threading runtime configuration such as CPU group settings only when the deployment environment and measurements justify it.
- CONC-41 [MANDATORY]: Treat thread-pool starvation as a design bug to remove, not as a reason to add arbitrary threads.
- CONC-42 [MANDATORY]: Keep the full call path asynchronous when the entry operation is asynchronous.
- CONC-43 [MANDATORY]: Batch small independent units of work when per-item scheduling overhead materially limits throughput.
- CONC-44 [MANDATORY]: Do not split work so finely that scheduling, synchronization, or queue overhead dominates useful work.
- CONC-45 [MANDATORY]: Keep per-work-item memory bounded so that concurrency does not turn latency pressure into GC pressure.
- CONC-46 [MANDATORY]: Complete or close producer-consumer pipelines explicitly so consumers can terminate deterministically.
- CONC-47 [MANDATORY]: Record queue length, active work count, throughput, latency, failure rate, cancellation rate, and timeout rate for each concurrent stage.
- CONC-48 [MANDATORY]: Use runtime metrics, tracing, and profiling to confirm whether bottlenecks are CPU saturation, blocking, contention, queueing, or allocation.
- CONC-49 [MANDATORY]: Protect important concurrency and throughput characteristics with repeatable stress, soak, and benchmark tests.
- CONC-50 [MANDATORY]: Prefer the simplest concurrency model that meets measured throughput and latency goals.
## Design
- GEN-004: `AstMetamodel.cs` defines rich, high-level constructs that mirror source language features. These constructs are lowered through transformation passes before Roslyn code generation.
- PIPE-002: Each visitor or rewriter in the transformation pipeline must have one well-defined responsibility.
- PIPE-004: Prefer several simple, comprehensible passes over a single pass that mixes unrelated transformation logic.
- PIPE-007: Prefer expressing language adaptation as AST transformations rather than pushing additional complexity into code generation.
## Visitor
- GEN-005: Use `BaseAstVisitor` for read-only analysis such as symbol tables, diagnostics, and validation. This pattern must not modify the AST.
- GEN-006: Use `DefaultRecursiveDescentVisitor` for type-preserving AST modifications. This pattern must not change node types or hoist statements.
- GEN-007: Use `DefaultAstRewriter` for statement-level desugaring, cross-type rewrites, and expression hoisting. This is the preferred pattern for new lowering passes because it returns `RewriteResult` with a node and prologue.
  
  Choose this pattern when introducing temporary variables, breaking down high-level constructs, transforming expression types, or performing any lowering that requires statement insertion.
## Reference
- GEN-008: Use `src/ast_generator/README.md` as the detailed reference for visitor and rewriter pattern selection.
- OVR-004: Use these reference files according to their role:
  
  - `.specify/memory/constitution.md` for architectural decisions and principles
  - `AGENTS.md` for operational commands and workflow guidance
  - `.specify/config.yml` for build and test command definitions
- SYN-007: Use these locations when looking for canonical syntax examples:
  
  - `test/ast-tests/CodeSamples/*.5th`
  - `src/parser/grammar/test_samples/*.5th`
  - `docs/Getting-Started/`
## Review
- GEN-009: Any pull request modifying `src/ast-generated/` must include:
  
  - The upstream metamodel or template changes
  - The regeneration command used
  - Confirmation that the generated files were not hand-edited
## Architecture
- GRAM-001: The parser surface is divided across three primary assets:
  
  - `src/parser/grammar/FifthLexer.g4` for tokens, keywords, literals, and lexical structure
  - `src/parser/grammar/FifthParser.g4` for syntactic rules and grammar structure
  - `src/parser/AstBuilderVisitor.cs` for parse-tree to high-level AST transformation
## Syntax
- GRAM-004 [MANDATORY]: Do not use `var <name> =` in examples or tests. Use `name: type =` or the appropriate canonical Fifth form.
- GRAM-005 [MANDATORY]: Do not use declarations such as `graph g =` or `triple t =`. Use `g: graph =` or `t: triple =`.
- GRAM-006 [MANDATORY]: Do not use the legacy `when` guard shorthand. Use the parameter constraint form `param: Type | <expr>` together with block bodies.
- SYN-001: Basic syntax looks like this:
  
  ```fifth
  class Person {
      Name: string;
      Height: float;
  }
  
  main() => myprint(5 + 6);
  myprint(int x) => std.print(x);
  ```
- SYN-002: Use `name: type = value` form. Never use `var name =` in C# or JavaScript style, and never use type-first forms such as `type name =`.
  
  ```fifth
  x: int = 42;
  g: graph = KG.CreateGraph();
  ```
## Guards
- GRAM-007: The canonical contrast for guard syntax is:
  
  ```fifth
  // INVALID
  myprint(int x) when x == 0 => std.print(x);
  
  // VALID
  myprint(int x | x == 0) { std.print(x); }
  ```
- SYN-004: Use the parameter constraint form with block bodies:
  
  ```fifth
  myprint(int x | x == 0) { std.print(x); }
  ```
  
  Do not use the legacy `when` shorthand.
  
  ```fifth
  // INVALID
  // myprint(int x) when x == 0 => std.print(x);
  ```
## Knowledge Graph
- GRAM-009: Use these canonical knowledge-graph forms in examples and tests:
  
  - `name: store = sparql_store(<iri>);`
  - `store default = sparql_store(<iri>);`
  - `KG.CreateGraph()` to create graphs
  - `graph += triple` to add triples
  
  Validate these flows with `dotnet test test/kg-smoke-tests/kg-smoke-tests.csproj`.
- SYN-005: Use the canonical knowledge-graph forms:
  
  ```fifth
  myStore: store = sparql_store(<http://example.org/store>);
  store default = sparql_store(<http://example.org/default>);
  
  g: graph = KG.CreateGraph();
  // Add triples with += operator
  ```
- SYN-006: Use these literal forms in syntax and examples:
  
  - TriG literals use `<{...}>`
  - SPARQL literals use `?<...>`
  - Object-position literal values may be strings, booleans, chars, signed integers, unsigned integers, `float`, `double`, or `decimal`
## Overview
- OVR-001: Fifth Language is a C# .NET 10.0 compiler for the Fifth programming language. It uses an ANTLR-based split lexer and parser, AST code generation for builders and visitors, and a multi-pass compiler that lowers the AST through intermediate transformation stages.
## Pipeline
- OVR-002: The canonical compiler flow is:
  
  1. Lexical analysis and parsing into an ANTLR parse tree
  2. Parse-tree transformation into a high-level AST through `AstBuilderVisitor.cs`
  3. High-level AST lowering through multiple language-transformation passes
  4. Roslyn code generation from the lowered AST into a PE assembly
- PIPE-001: The compiler applies these passes sequentially in `ParserManager.cs`:
  
  1. `TreeLinkageVisitor` for parent-child relationships
  2. `BuiltinInjectorVisitor` for built-in function definitions
  3. `ClassCtorInserter` for default constructors
  4. `SymbolTableBuilderVisitor` for scoping symbol tables
  5. `PropertyToFieldExpander` for property syntax expansion
  6. `OverloadGatheringVisitor` for overload grouping
  7. `OverloadTransformingVisitor` for guard and subclause transformation
  8. `DestructuringVisitor` for destructuring property references
  9. `DestructuringLoweringRewriter` for lowering destructuring into variable declarations
  10. `TypeAnnotationVisitor` for type inference and annotation
- PR-005: Transformation changes must be integrated correctly into the pipeline in `ParserManager.cs`.
## Structure
- OVR-003: The major repository areas are:
  
  - `src/ast-model/` for AST metamodel definitions including `AstMetamodel.cs`
  - `src/ast-generated/` for generated builders, visitors, and rewriters that must not be hand-edited
  - `src/ast_generator/` for the generator that produces `ast-generated/`
  - `src/parser/grammar/` for `FifthLexer.g4` and `FifthParser.g4`
  - `src/parser/AstBuilderVisitor.cs` for parse-tree to AST conversion
  - `src/compiler/LanguageTransformations/` for AST transformation passes
  - `src/compiler/ParserManager.cs` for transformation pipeline coordination
  - `src/fifthlang.system/` for built-in system functions and knowledge graph support
  - `test/ast-tests/` for AST and generator tests
  - `test/syntax-parser-tests/` for grammar parsing tests
  - `test/runtime-integration-tests/` for end-to-end verification tests
## Performance
- PERF-01 [MANDATORY]: Measure before and after every non-trivial performance optimization.
- PERF-02 [MANDATORY]: Optimize only code paths that are proven hot or materially user-visible.
- PERF-03 [MANDATORY]: Never trade away correctness, determinism, or observability for performance.
- PERF-04 [MANDATORY]: Avoid unnecessary allocations in hot paths.
- PERF-05 [MANDATORY]: Avoid hidden boxing in hot paths.
- PERF-06 [MANDATORY]: Do not use allocation-heavy LINQ or iterator chains in hot paths when a simpler loop is materially cheaper.
- PERF-07 [MANDATORY]: Minimize transient string creation in hot paths.
- PERF-08 [MANDATORY]: Use span-based parsing and formatting APIs where they materially reduce copying or allocation.
- PERF-09: Use Span<T> and ReadOnlySpan<T> only where they improve performance without making ownership or lifetime unsafe or unclear.
- PERF-10: Use Memory<T> and ReadOnlyMemory<T> only where buffer lifetime must cross async or heap boundaries.
- PERF-11 [MANDATORY]: Avoid copying large buffers or collections when a safe slice, view, or reference is sufficient.
- PERF-12: Use stackalloc only for small, bounded, short-lived buffers.
- PERF-13: Use ArrayPool<T> when frequent buffer allocation creates measurable GC pressure.
- PERF-14: Use object pooling only for objects that are expensive to create or reset and are used predictably at high frequency.
- PERF-15 [MANDATORY]: Always return rented or pooled resources promptly and exactly once.
- PERF-16 [MANDATORY]: Do not expose pooled buffers or objects beyond the lifetime in which they are valid to use.
- PERF-17 [MANDATORY]: Use async and await for naturally asynchronous I/O-bound operations.
- PERF-18 [MANDATORY]: Do not block threads on asynchronous work in throughput-sensitive code paths.
- PERF-19: Use ValueTask only when measurement shows that avoiding Task allocation is materially beneficial.
- PERF-20 [MANDATORY]: Propagate CancellationToken in long-running or potentially blocking operations to avoid wasted work.
- PERF-21 [MANDATORY]: Use structured logging and avoid expensive log message construction when the log level is disabled.
- PERF-22 [MANDATORY]: Do not use exceptions for normal control flow in hot paths.
- PERF-23 [MANDATORY]: Validate arguments and fail early before expensive work begins.
- PERF-24 [MANDATORY]: Choose collection types based on required lookup, iteration, mutation, and allocation characteristics.
- PERF-25 [MANDATORY]: Avoid multiple enumeration of the same sequence in hot paths.
- PERF-26 [MANDATORY]: Prefer contiguous data access patterns when they materially improve cache locality.
- PERF-27: Use structs only when value semantics and measured allocation or locality benefits justify them.
- PERF-28: Use readonly struct for immutable value types that are frequently copied or passed by reference.
- PERF-29 [MANDATORY]: Prefer immutability by default, but avoid defensive copying in hot paths unless correctness requires it.
- PERF-30 [MANDATORY]: Prefer generic, type-safe code over object-based abstractions when object-based code would box or allocate materially more.
- PERF-31 [MANDATORY]: Avoid unnecessary virtual dispatch in hot paths when a simpler and equally maintainable alternative exists.
- PERF-32 [MANDATORY]: Avoid runtime reflection in hot paths.
- PERF-33: Prefer source generation over runtime reflection or runtime code discovery where it materially improves startup or throughput.
- PERF-34 [MANDATORY]: Choose serialization and deserialization paths that minimize allocation, copying, and intermediate materialization.
- PERF-35 [MANDATORY]: Stream or pipe large payloads instead of fully materializing them when full buffering is unnecessary.
- PERF-36 [MANDATORY]: Design hot-path code to reduce GC pressure rather than relying on forced collection.
- PERF-37 [MANDATORY]: Do not call GC.Collect in production code as a performance strategy.
- PERF-38 [MANDATORY]: Do not introduce parallelism unless the workload is safe, partitionable, and measurably faster under realistic contention.
- PERF-39 [MANDATORY]: Avoid shared mutable state and lock contention in throughput-sensitive code.
- PERF-40: Use SIMD, hardware intrinsics, or vectorized APIs only when measurement shows a clear benefit and portability remains acceptable.
- PERF-41 [MANDATORY]: Keep hot loops simple, branch-light, and allocation-free where practical.
- PERF-42 [MANDATORY]: Keep hot-path assumptions explicit and validate them outside the hot loop where possible.
- PERF-43: Change tiered compilation, quick JIT, or compilation settings only when benchmark evidence justifies it.
- PERF-44: Make libraries trimming-compatible when they are intended for trimmed deployments.
- PERF-45: Use Native AOT only when startup time, memory footprint, deployment model, or scale characteristics justify its constraints.
- PERF-46: Avoid dynamic code paths that prevent trimming or Native AOT where those deployment modes are required.
- PERF-47 [MANDATORY]: Use profiling and production-safe telemetry to locate CPU, memory, allocation, and latency bottlenecks.
- PERF-48 [MANDATORY]: Protect important performance characteristics with repeatable benchmarks or regression checks.
- PERF-49 [MANDATORY]: Design APIs so efficient usage is the default and expensive usage is explicit.
- PERF-50 [MANDATORY]: Prefer the simplest implementation that meets measured performance goals.
## Documentation
- PIPE-005: Document dependencies between transformation passes whenever later stages rely on earlier ones.
## Correctness
- PIPE-006: Each transformation pass must preserve AST validity and type safety.
## Code Generation
- PIPE-009: The compiler uses `LoweredAstToRoslynTranslator` to emit C# syntax trees from the lowered AST for Roslyn compilation and PE or PDB emission. Roslyn-generated PDBs must include full line-and-column sequence points so debugging fidelity is preserved.
## Contracts
- PR-006: When behavior changes affect public contracts or CLI behavior, update the relevant contracts and CLI help text.
## Maintainability
- PR-007: When a change increases complexity, document the rationale in the pull request.
## Quality
- PR-008: Favor the smallest viable change and keep diffs focused.
- PR-009: Confirm reproducibility by re-running the documented commands rather than relying on assumptions.
- PR-010: Verify that generated outputs and diagnostics remain deterministic.
## Lowering
- PR-011: Validate that AST transformations maintain correct lowering semantics through the pipeline.
## Breaking Change
- PR-012: Every breaking change must include a migration note in the pull request.
- PR-013: Breaking changes must include updated tests that reflect the new behavior.
## Versioning
- PR-014: Apply a minor or major version bump when the change warrants it.
## Deprecation
- PR-016: Every deprecation must be documented and covered by tests.
## Functions
- SYN-003: Function definitions can use either expression bodies or block bodies:
  
  ```fifth
  add(int a, int b) => a + b;
  
  greet(string name) {
      std.print("Hello " + name);
  }
  ```
## Completion
- UTR-001: A feature is not complete until integration tests prove it:

  1. Runs as intended in situ
  2. Executes successfully at runtime rather than merely compiling
  3. Produces results that are accessible and correct
  4. Exercises the major code paths and result types involved

  Features with only compilation tests or with failing runtime tests are incomplete.
