using System.Diagnostics;
using ast;
using compiler.Pipeline.Phases;
using Fifth;

namespace compiler.Pipeline;

/// <summary>
/// Orchestrates compiler phase registration, dependency validation, and execution.
/// </summary>
public class TransformationPipeline
{
    private readonly List<ICompilerPhase> _phases = new();
    private readonly HashSet<string> _availableCapabilities = new();

    /// <summary>Registered phases in execution order.</summary>
    public IReadOnlyList<ICompilerPhase> Phases => _phases.AsReadOnly();

    /// <summary>
    /// Register a phase. Validates that all declared dependencies are satisfied
    /// by capabilities provided by previously registered phases.
    /// </summary>
    public void RegisterPhase(ICompilerPhase phase)
    {
        foreach (var dep in phase.DependsOn)
        {
            if (!_availableCapabilities.Contains(dep))
            {
                throw new InvalidOperationException(
                    $"Phase '{phase.Name}' depends on capability '{dep}' " +
                    $"which is not provided by any previously registered phase.");
            }
        }

        _phases.Add(phase);
        foreach (var cap in phase.ProvidedCapabilities)
        {
            _availableCapabilities.Add(cap);
        }
    }

    /// <summary>
    /// Execute the pipeline on an AST with the given options.
    /// </summary>
    public PipelineResult Execute(AstThing ast, PipelineOptions? options = null, string? targetFramework = null)
    {
        ArgumentNullException.ThrowIfNull(ast);
        options ??= PipelineOptions.Default;

        var context = new PhaseContext
        {
            TargetFramework = targetFramework,
            EnableCaching = options.EnableCaching
        };

        var timings = new Dictionary<string, TimeSpan>();
        var totalSw = DebugHelpers.DebugEnabled ? Stopwatch.StartNew() : null;
        var currentAst = ast;
        var phaseCount = 0;

        foreach (var phase in _phases)
        {
            if (options.SkipPhases.Contains(phase.Name))
            {
                continue;
            }

            var phaseSw = Stopwatch.StartNew();
            try
            {
                var result = phase.Transform(currentAst, context);
                phaseSw.Stop();

                timings[phase.Name] = phaseSw.Elapsed;
                context.Diagnostics.AddRange(result.Diagnostics);
                phaseCount++;

                if (DebugHelpers.DebugEnabled)
                {
                    Console.Error.WriteLine(
                        $"[PHASE] {phase.Name} completed in {phaseSw.ElapsedMilliseconds}ms");
                }

                if (options.DumpAfter?.Contains(phase.Name) == true)
                {
                    (options.DumpCallback ?? DefaultDump)(result.TransformedAst, phase.Name);
                }

                if (!result.Success && options.StopOnError)
                {
                    EmitTotalTiming(totalSw, phaseCount);
                    return new PipelineResult(
                        result.TransformedAst, context.Diagnostics.AsReadOnly(),
                        false, timings);
                }

                currentAst = result.TransformedAst;

                if (phase.Name == options.StopAfter)
                {
                    break;
                }
            }
            catch (System.Exception ex)
            {
                phaseSw.Stop();
                timings[phase.Name] = phaseSw.Elapsed;
                phaseCount++;

                // Log to stderr
                Console.Error.WriteLine($"===========================================");
                Console.Error.WriteLine($"PHASE FAILURE DETECTED: {phase.Name}");
                Console.Error.WriteLine($"Exception Type: {ex.GetType().FullName}");
                Console.Error.WriteLine($"Message: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.Error.WriteLine($"Inner Exception: {ex.InnerException.GetType().FullName}");
                    Console.Error.WriteLine($"Inner Message: {ex.InnerException.Message}");
                }
                Console.Error.WriteLine($"Stack Trace:\n{ex.StackTrace}");
                Console.Error.WriteLine($"===========================================");

                // Add error diagnostic to context
                var errorMsg = $"Phase '{phase.Name}' failed: {ex.GetType().Name}: {ex.Message}";
                context.Diagnostics.Add(new Diagnostic(DiagnosticLevel.Error, errorMsg));

                if (DebugHelpers.DebugEnabled)
                {
                    Console.Error.WriteLine(
                        $"[PHASE] {phase.Name} completed in {phaseSw.ElapsedMilliseconds}ms");
                }

                if (options.StopOnError)
                {
                    EmitTotalTiming(totalSw, phaseCount);
                    return new PipelineResult(
                        currentAst, context.Diagnostics.AsReadOnly(),
                        false, timings);
                }

                // If !StopOnError, continue to next phase
            }
        }

        EmitTotalTiming(totalSw, phaseCount);
        return new PipelineResult(
            currentAst, context.Diagnostics.AsReadOnly(), true, timings);
    }

    /// <summary>
    /// Create the default Fifth compiler pipeline with all standard phases
    /// registered in the correct order.
    /// </summary>
    public static TransformationPipeline CreateDefault()
    {
        var pipeline = new TransformationPipeline();

        // Phase 1: Tree linkage
        pipeline.RegisterPhase(new TreeLinkagePhase());
        // Phase 2: Builtin injection
        pipeline.RegisterPhase(new BuiltinInjectorPhase());
        // Phase 3: Class constructor insertion (compound: inserter + relink)
        pipeline.RegisterPhase(new ClassCtorsPhase());
        // Phase 4: Constructor validation
        pipeline.RegisterPhase(new ConstructorValidationPhase());
        // Phase 5: Initial symbol table
        pipeline.RegisterPhase(new SymbolTableInitialPhase());
        // Phase 6: Namespace import resolution
        pipeline.RegisterPhase(new NamespaceImportResolverPhase());
        // Phase 7: Constructor resolution
        pipeline.RegisterPhase(new ConstructorResolutionPhase());
        // Phase 8: Definite assignment analysis
        pipeline.RegisterPhase(new DefiniteAssignmentPhase());
        // Phase 9: Base constructor validation
        pipeline.RegisterPhase(new BaseConstructorValidationPhase());
        // Phase 10: Type parameter resolution
        pipeline.RegisterPhase(new TypeParameterResolutionPhase());
        // Phase 11: Generic type inference
        pipeline.RegisterPhase(new GenericTypeInferencePhase());
        // Phase 12: Property to field expansion
        pipeline.RegisterPhase(new PropertyToFieldPhase());
        // Phase 13: Destructure pattern flattening (compound: visitor + propagator)
        pipeline.RegisterPhase(new DestructurePatternFlattenPhase());
        // Phase 14: Overload gathering
        pipeline.RegisterPhase(new OverloadGatheringPhase());
        // Phase 15: Guard validation
        pipeline.RegisterPhase(new GuardValidationPhase());
        // Phase 16: Overload transform
        pipeline.RegisterPhase(new OverloadTransformPhase());
        // Phase 17: Destructuring lowering
        pipeline.RegisterPhase(new DestructuringLoweringPhase());
        // Phase 18: Unary operator lowering
        pipeline.RegisterPhase(new UnaryOperatorLoweringPhase());
        // Phase 19: SPARQL variable binding
        pipeline.RegisterPhase(new SparqlVariableBindingPhase());
        // Phase 20: SPARQL literal lowering
        pipeline.RegisterPhase(new SparqlLiteralLoweringPhase());
        // Phase 21: TriG literal lowering
        pipeline.RegisterPhase(new TriGLiteralLoweringPhase());
        // Phase 22: Tree relink (mid-pipeline)
        pipeline.RegisterPhase(new TreeRelinkPhase());
        // Phase 23: Triple diagnostics
        pipeline.RegisterPhase(new TripleDiagnosticsPhase());
        // Phase 24: Triple expansion
        pipeline.RegisterPhase(new TripleExpansionPhase());
        // Phase 25: Final symbol table rebuild
        pipeline.RegisterPhase(new SymbolTableFinalPhase());
        // Phase 26: Final namespace import resolution
        pipeline.RegisterPhase(new NamespaceImportResolverFinalPhase());
        // Phase 27: Variable reference resolution
        pipeline.RegisterPhase(new VarRefResolverPhase());
        // Phase 28: Type annotation (compound: annotation + lowering + error collection)
        pipeline.RegisterPhase(new TypeAnnotationPhase());
        // Phase 29: External call validation
        pipeline.RegisterPhase(new ExternalCallValidationPhase());
        // Phase 30: Try/catch/finally validation
        pipeline.RegisterPhase(new TryCatchFinallyValidationPhase());
        // Phase 31: Query application type check
        pipeline.RegisterPhase(new QueryApplicationTypeCheckPhase());
        // Phase 32: Query application lowering
        pipeline.RegisterPhase(new QueryApplicationLoweringPhase());
        // Phase 33: List comprehension validation
        pipeline.RegisterPhase(new ListComprehensionValidationPhase());
        // Phase 34: List comprehension lowering
        pipeline.RegisterPhase(new ListComprehensionLoweringPhase());
        // Phase 35: Lambda validation
        pipeline.RegisterPhase(new LambdaValidationPhase());
        // Phase 36: Lambda closure conversion (compound: validation + conversion + relink)
        pipeline.RegisterPhase(new LambdaClosureConversionPhase());
        // Phase 37: Defunctionalisation (compound: rewrite + relink)
        pipeline.RegisterPhase(new DefunctionalisationPhase());
        // Phase 38: Tail call optimization (disabled by default via PipelineOptions.Default)
        pipeline.RegisterPhase(new TailCallOptimizationPhase());

        return pipeline;
    }

    /// <summary>
    /// Query which capabilities are available after a given phase.
    /// </summary>
    public IReadOnlySet<string> GetCapabilitiesAfter(string phaseName)
    {
        var caps = new HashSet<string>();
        foreach (var phase in _phases)
        {
            foreach (var cap in phase.ProvidedCapabilities)
            {
                caps.Add(cap);
            }

            if (phase.Name == phaseName)
            {
                break;
            }
        }

        return caps;
    }

    private static void DefaultDump(AstThing ast, string phaseName)
    {
        DebugHelpers.DebugLog($"[DUMP after {phaseName}] AST: {ast}");
    }

    private static void EmitTotalTiming(Stopwatch? sw, int count)
    {
        if (sw != null && DebugHelpers.DebugEnabled)
        {
            sw.Stop();
            Console.Error.WriteLine(
                $"[PIPELINE] Total: {sw.ElapsedMilliseconds}ms ({count} phases executed)");
        }
    }
}
