# Engineering Constitution
## Agentic
- AI-002: When diagnostic, experimental, or temporary test code is required, it shall be created and run outside the source repository.
- AI-003: When requirements are ambiguous, at least one clarifying question shall be asked before implementation.
- AI-004: Before implementation starts, explicit acceptance criteria shall be defined and used as the completion gate.
- AI-005: Each behavior claim in the completion report shall be backed by code evidence, test output, or documentation evidence.
- AI-006: When information is uncertain, uncertainty shall be stated explicitly and assumptions shall not be presented as facts.
- AI-007: When an API, file, symbol, or command cannot be verified, it shall not be asserted as existing.
- AI-008: For each requested change, only the smallest set of edits required to satisfy the request shall be applied.
- AI-009: When editing existing code, established project conventions and architecture boundaries shall be preserved.
- AI-010: After code changes, relevant tests, lint checks, or build checks shall be executed and results shall be reported.
- AI-011: When checks fail, the failure cause and next corrective action shall be reported, and no additional feature work shall be performed.
- AI-012: Final code output shall not include dead code, placeholder logic, or commented-out alternatives.
## Governance
- ARCH-001 [MANDATORY]: Each rule in this document must be objectively testable by an automated check. To comply, include a `Verify:` line with a concrete pass/fail condition.
## Dependency
- ARCH-002: Project references under `src/` must follow this DAG and must not point backward:

  ```text
  ast-model -> ast_generator -> ast-generated -> parser -> compiler -> tests
                                                 ^
                                            fifthlang.system
  ```

  To comply, reject any `<ProjectReference>` that targets an earlier node in this order.
- BUILD-008: Build through `fifthlang.sln` so dependency order is resolved correctly. To comply, avoid ad hoc partial builds when validating integration behavior.
- PIPE-003: A pass may depend only on invariants established by earlier passes. To comply, place each pass after the pass that creates its prerequisites.
## Ast
- ARCH-004: Only `src/ast-model/AstMetamodel.cs` may define AST types and inheritance. To comply, never create classes inheriting `AstThing`, `Expression`, `Statement`, or `TypeRef` anywhere but there.
## Backend
- ARCH-005: Only `LoweredAstToRoslynTranslator` may bridge to Roslyn types. To comply, do not use `Microsoft.CodeAnalysis` in `src/compiler/LanguageTransformations/`, `src/compiler/Pipeline/Phases/`, `src/ast-model/`, or `src/ast-generated/`.
## Pipeline
- ARCH-006 [MANDATORY]: Each phase must declare every capability it consumes via `DependsOn`. To comply, in each `ICompilerPhase`, ensure `DependsOn` covers all required AST state used by its visitors or rewriters.
- ARCH-007: Each `ICompilerPhase` must have exactly one responsibility category. To comply, if a phase performs multiple categories, split it or justify the composition in its XML summary.
- ARCH-008: A phase must not mutate the input AST after returning from `Transform`. To comply, do not store the input `ast` parameter in instance or static fields.
- ARCH-011: `TransformationPipeline.CreateDefault()` defines a fixed phase order. To comply, keep only direct `RegisterPhase` calls in that method, with no runtime reordering logic.
- ARCH-012: Phases must not communicate through static mutable state. To comply, pass data only through AST and `PhaseContext`; the sole static exception is read-only `DebugHelpers.DebugEnabled`.
- OVR-002: The canonical compiler flow is:
  
  1. Lexical analysis and parsing into an ANTLR parse tree
  2. Parse-tree transformation into a high-level AST through `AstBuilderVisitor.cs`
  3. High-level AST lowering through multiple language-transformation passes
  4. Roslyn code generation from the lowered AST into a PE assembly
- PIPE-001: Apply transformation passes in the canonical order defined by `ParserManager.cs`. To comply, keep pass registration in this sequence: TreeLinkage, BuiltinInjector, ClassCtorInserter, SymbolTableBuilder, PropertyToFieldExpander, OverloadGathering, OverloadTransforming, DestructuringVisitor, DestructuringLoweringRewriter, TypeAnnotation.
- PIPE-010: Transformation passes MUST use rewriters rather than visitors. To comply, derive from `DefaultAstRewriter` or a class derived from it.
- PIPE-011: Never use Recursive descent visitors unless the AST is not going to change.  To Comply, don't derive from DefaultRecursiveDescentVisitor or a class derived from it.
## Lowering
- ARCH-009: Lowering phases must move constructs toward forms directly handled by `LoweredAstToRoslynTranslator`. To comply, output node shapes must be equal-or-lower level than input node shapes.
## Diagnostics
- ARCH-010: Phases must emit diagnostics only through `PhaseResult.Diagnostics` or `PhaseContext.Diagnostics`. To comply, avoid `Console.Out`; allow `Console.Error` only when `DebugHelpers.DebugEnabled` is true.
- BUILD-012: Treat known baseline warnings as expected unless their pattern changes. To comply, ignore existing ANTLR `assoc`, nullable, and switch-exhaustiveness warnings unless new or altered.
## Parser
- ARCH-013 [MANDATORY]: Only `FifthLexer.g4` and `FifthParser.g4` may define parseable Fifth syntax. To comply, `AstBuilderVisitor` may map parse trees to AST, but must not introduce syntax not accepted by the grammar.
- ARCH-014 [MANDATORY]: Semantic parser rules in `FifthParser.g4` must map one-to-one with `Visit*` methods in `AstBuilderVisitor.cs`. To comply, keep rule-name and visitor-method sets aligned, allowing ANTLR labeled alternatives.
- BUILD-010: Do not add manual ANTLR generation to the normal workflow. To comply, rely on parser-project build targets for grammar compilation.
- CODE-012 [MANDATORY]: Update grammar files when grammar behavior changes. To comply, change `FifthLexer.g4` and `FifthParser.g4` as required by the syntax change.
- CODE-013 [MANDATORY]: Keep `AstBuilderVisitor.cs` synchronized with grammar changes. To comply, update visitor methods whenever parse-tree shape or surface syntax changes.
## Visitor
- ARCH-015: Visitor base class must match the operation type.

  | Operation | Base class |
  |---|---|
  | Read-only analysis | `BaseAstVisitor` |
  | Type-preserving edits | `DefaultRecursiveDescentVisitor` |
  | Cross-type rewrites or prologue insertion | `DefaultAstRewriter` |

  To comply, do not mutate AST in `BaseAstVisitor`, and use `DefaultAstRewriter` when returning non-empty prologue or changing node types.
- GEN-005: Use `BaseAstVisitor` only for read-only analysis. To comply, use it for symbol collection, diagnostics, or validation, and do not mutate AST nodes.
- GEN-006: Use `DefaultRecursiveDescentVisitor` only for type-preserving AST edits. To comply, keep input and output node types the same and avoid statement hoisting.
- GEN-007: Use `DefaultAstRewriter` for lowering that changes node shape or inserts statements. To comply, choose this pattern when returning `RewriteResult` with prologue, adding temporaries, or doing cross-type rewrites.
## Prerequisites
- BUILD-001 [MANDATORY]: Use the pinned toolchain: .NET SDK `10.0.100` from `global.json`, Java 17+, and `src/parser/tools/antlr-4.13.2-complete.jar`. To comply, run `dotnet --version` and `java -version` before troubleshooting build failures.
## Commands
- BUILD-002: Use `dotnet restore fifthlang.sln` as the standard restore entry point. To comply, do not cancel restore; set automation timeout to at least 120 seconds.
- BUILD-003: Use `dotnet build fifthlang.sln` as the standard build entry point. To comply, do not cancel build; set automation timeout to at least 120 seconds.
## Testing
- BUILD-004: Use `dotnet test fifthlang.sln` as the default regression gate. To comply, do not cancel test runs; set automation timeout to at least 5 minutes.
- BUILD-005: Use `dotnet test test/ast-tests/ast_tests.csproj` only as a local smoke test. To comply, run full-solution tests before merging behavior changes.
- BUILD-014: Use targeted `just` test commands for local iteration, not as the final gate. To comply, after `just test-ast`, `just test-runtime`, `just test-syntax`, or `just test-all-roslyn`, still run `dotnet test fifthlang.sln`.
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
- TDD-001 [MANDATORY]: The development workflow shall follow test-first Red-Green-Refactor.
- TDD-002 [MANDATORY]: Before writing production code for a behavior, the developer shall write the test code for that behavior.
- TDD-003 [MANDATORY]: Before implementing code for a behavior, the developer shall execute the corresponding test and observe it fail.
- TDD-004 [MANDATORY]: When the test passes, the developer shall refactor the changed code while preserving test pass status.
- TDD-005 [MANDATORY]: At the start of each cycle, the developer shall create a failing property-based test.
- TDD-006 [MANDATORY]: In the Green step, the developer shall make the smallest functional code change that makes the failing test pass.
- TDD-007 [MANDATORY]: In each cycle, the developer shall limit changes to a single concern.
- TDD-008 [MANDATORY]: When moving from red to green, the implementation shall not mask failures; it shall pass by adding valid functionality.
- TDD-009 [MANDATORY]: After refactoring, the changed code shall satisfy the project cleanliness and technical-debt standards.
- UTR-004: When behavioral validation is possible, tests shall validate externally observable behavior and avoid internal-detail and concrete-implementation assertions.
- UTR-005: Tests shall not mask failures with broad try/catch blocks or unconditional success assertions.
- UTR-006: When a test fails, the team shall treat it as outstanding work and shall not suppress it.
## Generation
- BUILD-006: After metamodel changes, regenerate AST code with `dotnet run --project src/ast_generator/ast_generator.csproj -- --folder src/ast-generated`. To comply, include regeneration in the same change set as the metamodel update.
- BUILD-011: Do not add manual AST generation to the normal workflow. To comply, use manual generation only for focused regeneration tasks.
- CODE-010 [MANDATORY]: NEVER hand-edit files in `src/ast-generated/`. To comply, make changes in metamodel or templates, then regenerate.
- CODE-011 [MANDATORY]: Modify AST structure only through `src/ast-model/` metamodels. To comply, regenerate generated AST output after metamodel edits.
- GEN-001: Treat `src/ast-generated/` as generator output only. To comply, never hand-edit files in `src/ast-generated/`.
- GEN-002: Keep generated outputs in their canonical files. To comply, emit builders to `builders.generated.cs`, visitors to `visitors.generated.cs`, rewriters to `rewriter.generated.cs`, and type inference helpers to `typeinference.generated.cs`.
## Verification
- BUILD-007: Validate tool versions before diagnosing restore or build errors. To comply, ensure `.NET` reports `10.0.x` and Java reports 17 or newer.
## Workflow
- BUILD-009: Do not cancel restore, build, test, or generation commands because long runtimes are expected. To comply, wait for completion unless the command is clearly hung.
- GEN-003: Change generated output by editing source inputs, then regenerating. To comply, update `src/ast-model/AstMetamodel.cs` or templates, run `just run-generator`, then run `dotnet build fifthlang.sln`.
- GRAM-002 [MANDATORY]: Any grammar change must follow the full grammar-update workflow. To comply, update grammar files and `AstBuilderVisitor.cs`, add samples, run parser tests, then run `dotnet test fifthlang.sln`.
- PIPE-008: A new transformation is complete only after implementation, registration, and tests. To comply, add the pass in `src/compiler/LanguageTransformations/`, register it in `src/compiler/ParserManager.cs`, add tests, then run build and full tests.
## Validation
- BUILD-013: Validate changes in order: build, full test, then runtime behavior. To comply, run `dotnet build fifthlang.sln`, `dotnet test fifthlang.sln`, then verify behavior manually or with integration tests.
- GRAM-003 [MANDATORY]: All non-negative `.5th` samples in docs and tests must parse with the current grammar. To comply, run `just validate-examples` before commit.
- GRAM-008: Exclude intentional negative tests from normal example-validation checks. To comply, keep them in `*/Invalid/*`, include `invalid` in filename, or use explicit negative-test markers; use `--include-negatives` only for debugging.
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
## Design
- CODE-002: Prefer the simplest design that satisfies the functional requirement and the non-functional requirements and other guidelines. To comply, verify abstractions meet all requirements.
- GEN-004: Keep `AstMetamodel.cs` focused on high-level language constructs. To comply, represent source-level features in the metamodel and lower them in transformation passes.
- PIPE-002: Each transformation pass must have one well-defined responsibility. To comply, split passes that mix unrelated concerns.
- PIPE-004: Prefer multiple simple passes over one mixed pass. To comply, introduce a new focused pass instead of extending an unrelated pass.
- PIPE-007: Prefer AST transformations over adding complexity to code generation. To comply, lower language constructs before `LoweredAstToRoslynTranslator`.
## Maintainability
- CODE-003: Keep changes minimal and scoped to the target behavior. To comply, avoid unrelated refactors and preserve existing public APIs unless the change requires breaking them.
## Versioning
- CODE-006: Use Semantic Versioning in `MAJOR.MINOR.PATCH` format. To comply, bump version components according to compatibility impact.
## Cli
- CODE-007: Use stdin/args for input, stdout for normal output, and stderr for errors. To comply, route diagnostics to stderr and keep success output on stdout.
- CODE-008: Default CLI output must be human-readable text. To comply, add JSON output only when it meaningfully improves automation.
- CODE-009: CLI output must be deterministic. To comply, avoid timestamps and non-deterministic ordering in command output.
## Repository
- CODE-015 [MANDATORY]: Use `scripts/` only for durable automation. To comply, keep one-off scripts outside tracked repository paths.
## Security
- CODE-018 [MANDATORY]: Do not execute arbitrary code during parsing or generation. To comply, restrict execution paths to trusted, explicit operations.
- CODE-019: Validate all external inputs before use. To comply, keep user input data separated from internal template logic.
## Reference
- GEN-008: Use `src/ast_generator/README.md` as the detailed reference for pattern selection. To comply, verify the selected visitor or rewriter pattern against that guide before adding a new pass.
- OVR-004: Use these reference files according to their role:
  
  - `.specify/memory/constitution.md` for architectural decisions and principles
  - `AGENTS.md` for operational commands and workflow guidance
  - `.specify/config.yml` for build and test command definitions
- SYN-007: Use repository sample paths as canonical syntax references. To comply, prefer `test/ast-tests/CodeSamples/*.5th`, `src/parser/grammar/test_samples/*.5th`, and `docs/Getting-Started/`.
## Review
- GEN-009: A pull request that changes `src/ast-generated/` must show the source change that produced it. To comply, include metamodel or template edits, the regeneration command, and confirmation that generated files were not hand-edited.
## Architecture
- GRAM-001 [MANDATORY]: Keep syntax responsibilities split across lexer, parser, and AST builder. To comply, define tokens in `FifthLexer.g4`, syntax rules in `FifthParser.g4`, and parse-tree mapping in `AstBuilderVisitor.cs`.
## Syntax
- GRAM-004 [MANDATORY]: Do not use C# style `var <name> =` in Fifth examples or tests. To comply, use canonical declarations such as `name: type = value`.
- SYN-001 [MANDATORY]: Use canonical Fifth syntax in examples. To comply, use forms such as:

  ```fifth
  class Person {
      Name: string;
      Height: float;
  }

  main(): int
  {
    myprint(5 + 6);
    return 0;  
  }
  myprint(int x): int
  {
    std.print(x);
  }
  ```
- SYN-002: Declare variables in `name: type = value` form. To comply, use declarations such as:

  ```fifth
  x: int = 42;
  g: graph = KG.CreateGraph();
  ```
## Guards
- GRAM-007: Use only parameter-constraint guard syntax. To comply, follow this contrast:

  ```fifth
  // INVALID
  myprint(int x) when x == 0 => std.print(x);

  // VALID
  myprint(int x | x == 0) { std.print(x); }
  ```
- SYN-004: Write guards as parameter constraints, not `when` clauses. To comply, write guards like:

  ```fifth
  myprint(int x | x == 0) { std.print(x); }
  ```
## Overview
- OVR-001: Fifth Language is a C# .NET 10.0 compiler for the Fifth programming language. It uses an ANTLR-based split lexer and parser, AST code generation for builders and visitors, and a multi-pass compiler that lowers the AST through intermediate transformation stages.
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
- PAC-001 [MANDATORY]: For naturally asynchronous I/O-bound work, asynchronous APIs with async/await shall be used.
- PAC-002 [MANDATORY]: Throughput-sensitive code paths shall not block threads while waiting for asynchronous operations.
- PAC-003: ValueTask shall be used only when measurements show material allocation benefit and usage semantics are correct for ValueTask consumption.
- PAC-004 [MANDATORY]: Long-running or potentially blocking operations shall accept and propagate CancellationToken.
- PAC-005 [MANDATORY]: Parallelism shall be introduced only when the workload is thread-safe, partitionable, and measurably faster under realistic contention.
- PAC-006 [MANDATORY]: Throughput-sensitive code shall minimize shared mutable state and lock contention.
- PCV-001 [MANDATORY]: Hot loops shall remain simple, branch-light, and allocation-free where practical.
- PCV-002 [MANDATORY]: Hot-loop assumptions shall be explicit, and invariant checks shall be validated outside the loop when possible.
- PCV-003: SIMD, hardware intrinsics, or vectorized APIs shall be used only when measurement shows clear benefit and target-platform portability remains acceptable.
- PDA-001 [MANDATORY]: Collection types shall be selected according to required lookup, iteration, mutation, and allocation characteristics.
- PDA-002 [MANDATORY]: Hot-path logic shall not enumerate the same sequence multiple times.
- PDA-003 [MANDATORY]: When measurements show cache-locality benefit, contiguous data-access patterns shall be preferred.
- PDA-004: Structs shall be used only when value semantics and measured allocation or locality benefits justify them; immutable frequently copied value types shall be readonly struct.
- PDA-005 [MANDATORY]: Immutability shall be the default design, and defensive copying on hot paths shall be used only when required for correctness.
- PDA-006 [MANDATORY]: Generic type-safe implementations shall be preferred over object-based abstractions when object-based designs materially increase boxing or allocation.
- PDA-007 [MANDATORY]: Hot paths shall avoid unnecessary virtual dispatch when an equally maintainable lower-overhead alternative exists.
- PDA-008 [MANDATORY]: Public APIs shall make efficient usage the default path and expensive behavior explicit.
- PIS-001 [MANDATORY]: Serialization and deserialization paths shall minimize allocation, copying, and intermediate materialization.
- PIS-002 [MANDATORY]: When full buffering is unnecessary for large payloads, data shall be streamed or piped instead of fully materialized.
- PIS-003 [MANDATORY]: Structured logging shall be used, and expensive log message construction shall be skipped when the log level is disabled.
- PIS-004 [MANDATORY]: Exceptions shall not be used as normal control flow on hot paths.
- PIS-005 [MANDATORY]: Arguments and preconditions shall be validated before expensive work begins.
- PMA-001 [MANDATORY]: Hot-path code shall avoid unnecessary allocations and shall keep allocation rate low enough to prevent avoidable GC pressure.
- PMA-002 [MANDATORY]: Hot-path code shall avoid hidden boxing.
- PMA-003 [MANDATORY]: When a hot path is allocation-sensitive, allocation-heavy LINQ or iterator chains shall be replaced with materially cheaper loops.
- PMA-004 [MANDATORY]: Hot-path code shall minimize transient string allocation.
- PMA-005 [MANDATORY]: When parsing or formatting on hot paths, span-based APIs shall be used when they measurably reduce copying or allocation.
- PMA-006: Span<T> and ReadOnlySpan<T> shall be used only when ownership and lifetime remain safe and clear.
- PMA-007: Memory<T> and ReadOnlyMemory<T> shall be used when buffer lifetime must cross async or heap boundaries.
- PMA-008 [MANDATORY]: Large buffers and collections shall not be copied when a safe slice, view, or reference is sufficient.
- PMA-009: stackalloc shall be used only for small, bounded, short-lived buffers.
- PMA-010: ArrayPool<T> shall be used when measured allocation patterns show frequent buffer churn causing GC pressure.
- PMA-011: Object pooling shall be used only for objects that are expensive to create or reset and are used at predictable high frequency.
- PMA-012 [MANDATORY]: Rented or pooled resources shall be returned promptly exactly once and shall not be used or exposed after return.
- PMA-013 [MANDATORY]: GC.Collect shall not be used in production as a performance strategy.
- PMG-001 [MANDATORY]: Before implementing a non-trivial optimization, baseline metrics shall be captured, and the same metrics shall be re-measured after the change.
- PMG-002 [MANDATORY]: Optimization work shall target only paths proven hot in profiling data or paths with material user-visible latency.
- PMG-003 [MANDATORY]: Performance optimizations shall preserve correctness, deterministic behavior, and required observability.
- PMG-004 [MANDATORY]: Performance investigations shall use profiling and production-safe telemetry to identify CPU, memory, allocation, and latency bottlenecks.
- PMG-005 [MANDATORY]: Critical performance characteristics shall be protected by repeatable benchmarks or automated regression checks.
- PMG-006 [MANDATORY]: When multiple designs meet performance targets, the simplest maintainable design shall be selected.
- PRD-001 [MANDATORY]: Hot paths shall not use runtime reflection.
- PRD-002: When startup or throughput is materially improved, source generation shall be preferred over runtime reflection or runtime code discovery.
- PRD-003: Tiered compilation, quick JIT, or compilation settings shall be changed only when benchmark evidence justifies the change.
- PRD-004: When trimmed or Native AOT deployment is required, code paths incompatible with trimming or AOT shall be avoided and library surfaces shall remain trimming-compatible.
- PRD-005: Native AOT shall be adopted only when measured startup, memory, deployment, or scale benefits justify its constraints.
## Documentation
- PIPE-005: Document pass dependencies when one pass relies on another. To comply, record dependency assumptions in code comments or phase summaries.
## Correctness
- PIPE-006: Every pass must preserve AST validity and type safety. To comply, add tests that fail if the pass creates invalid or untyped AST states.
## Code Generation
- PIPE-009: Emit Roslyn syntax from lowered AST through `LoweredAstToRoslynTranslator` with debug-accurate sequence points. To comply, preserve full line-and-column mapping in generated PDB data.
## Functions
- SYN-003: Always define functions with block bodies. To comply, use forms such as:

  ```fifth
  greet(string name) {
      std.print("Hello " + name);
  }
  ```
## Knowledge Graph
- SYN-005: Use canonical store and graph forms for knowledge-graph code. To comply, use forms such as:

  ```fifth
  myStore: store = sparql_store(<http://example.org/store>);
  g: graph = KG.CreateGraph();
  ```
- SYN-006: Use canonical literal syntax for TriG and SPARQL values. To comply, use `<{...}>` for TriG, `?<...>` for SPARQL, and supported scalar types only for object literals.
## Completion
- UTR-001: A feature shall be marked complete only when integration tests pass at runtime, verify in-situ behavior, verify accessible and correct results, and exercise major code paths and result types.
- UTR-002: When only compilation checks exist, the feature shall be marked incomplete.
- UTR-003: When any runtime test fails, the feature shall be marked incomplete.
