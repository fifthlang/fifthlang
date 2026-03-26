using FluentAssertions;
using compiler;
using compiler.Pipeline;
using ast;
using FsCheck;
using FsCheck.Xunit;

namespace ast_tests.Pipeline;

/// <summary>
/// Property-based tests for pipeline execution behavior.
/// Tests Properties 3–9, 11–13, 15 from the design document.
/// </summary>
public class PipelineExecutionPropertyTests
{
    #region Helpers

    private static AstThing CreateMinimalAst()
    {
        return new AssemblyDef
        {
            Visibility = Visibility.Public,
            Name = AssemblyName.From("Test"),
            PublicKeyToken = "",
            Version = "1.0.0",
            AssemblyRefs = [],
            Modules = [],
            TestProperty = ""
        };
    }

    private static List<TrackingPhase> CreatePhases(int count, List<string> executionLog)
    {
        var phases = new List<TrackingPhase>();
        for (var i = 0; i < count; i++)
        {
            phases.Add(new TrackingPhase(executionLog)
            {
                Name = $"Phase_{i}"
            });
        }
        return phases;
    }

    private static TransformationPipeline BuildPipeline(IEnumerable<ICompilerPhase> phases)
    {
        var pipeline = new TransformationPipeline();
        foreach (var phase in phases)
            pipeline.RegisterPhase(phase);
        return pipeline;
    }

    #endregion

    #region Mock Phases

    private class TrackingPhase : ICompilerPhase
    {
        private readonly List<string> _executionLog;
        public string Name { get; init; } = "Mock";
        public IReadOnlyList<string> DependsOn { get; init; } = Array.Empty<string>();
        public IReadOnlyList<string> ProvidedCapabilities { get; init; } = Array.Empty<string>();
        private readonly bool _shouldFail;
        private readonly List<Diagnostic>? _diagnosticsToEmit;

        public TrackingPhase(List<string> executionLog, bool shouldFail = false, List<Diagnostic>? diagnosticsToEmit = null)
        {
            _executionLog = executionLog;
            _shouldFail = shouldFail;
            _diagnosticsToEmit = diagnosticsToEmit;
        }

        public PhaseResult Transform(AstThing ast, PhaseContext context)
        {
            _executionLog.Add(Name);
            var diags = _diagnosticsToEmit ?? new List<Diagnostic>();
            if (_shouldFail)
                return PhaseResult.Fail(ast, diags);
            return PhaseResult.Ok(ast, diags);
        }
    }

    private class ContextCapturingPhase : ICompilerPhase
    {
        public string Name { get; init; } = "ContextCapture";
        public IReadOnlyList<string> DependsOn { get; init; } = Array.Empty<string>();
        public IReadOnlyList<string> ProvidedCapabilities { get; init; } = Array.Empty<string>();
        public PhaseContext? CapturedContext { get; private set; }

        public PhaseResult Transform(AstThing ast, PhaseContext context)
        {
            CapturedContext = context;
            return PhaseResult.Ok(ast);
        }
    }

    private class ThrowingPhase : ICompilerPhase
    {
        public string Name { get; init; } = "Thrower";
        public IReadOnlyList<string> DependsOn { get; init; } = Array.Empty<string>();
        public IReadOnlyList<string> ProvidedCapabilities { get; init; } = Array.Empty<string>();

        public PhaseResult Transform(AstThing ast, PhaseContext context)
        {
            throw new InvalidOperationException($"Phase '{Name}' intentionally failed");
        }
    }

    #endregion

    #region Property 3: Phase execution respects registration order

    /// <summary>
    /// Property 3: Phase execution respects registration order.
    /// Register N mock phases, execute, verify invocation order matches registration.
    /// Validates: Requirements 4.3, 11.2
    /// </summary>
    [Property]
    public Property PhaseExecution_RespectsRegistrationOrder()
    {
        var gen = Gen.Choose(2, 20);
        return Prop.ForAll(gen.ToArbitrary(), n =>
        {
            var executionLog = new List<string>();
            var phases = CreatePhases(n, executionLog);
            var pipeline = BuildPipeline(phases);

            pipeline.Execute(CreateMinimalAst(), new PipelineOptions { StopOnError = false });

            var expectedOrder = phases.Select(p => p.Name).ToList();
            return executionLog.SequenceEqual(expectedOrder);
        });
    }

    #endregion

    #region Property 4: SkipPhases excludes named phases from execution

    /// <summary>
    /// Property 4: SkipPhases excludes named phases from execution.
    /// Register N mock phases, generate random skip subsets, verify skipped phases don't execute.
    /// Validates: Requirements 5.2, 9.1
    /// </summary>
    [Property]
    public Property SkipPhases_ExcludesNamedPhasesFromExecution()
    {
        var gen = from n in Gen.Choose(2, 20)
                  from skipMask in Gen.ListOf(n, Gen.Elements(true, false))
                  select (n, skipMask: skipMask.ToList());

        return Prop.ForAll(gen.ToArbitrary(), tuple =>
        {
            var (n, skipMask) = tuple;
            var executionLog = new List<string>();
            var phases = CreatePhases(n, executionLog);
            var pipeline = BuildPipeline(phases);

            var skipSet = new HashSet<string>();
            for (var i = 0; i < n; i++)
            {
                if (skipMask[i])
                    skipSet.Add(phases[i].Name);
            }

            var options = new PipelineOptions { SkipPhases = skipSet, StopOnError = false };
            pipeline.Execute(CreateMinimalAst(), options);

            // Skipped phases should not appear in execution log
            var skippedExecuted = executionLog.Any(name => skipSet.Contains(name));
            // Non-skipped phases should all appear
            var expectedExecuted = phases.Where(p => !skipSet.Contains(p.Name)).Select(p => p.Name).ToList();
            return !skippedExecuted && executionLog.SequenceEqual(expectedExecuted);
        });
    }

    #endregion

    #region Property 5: StopAfter halts execution at the named phase

    /// <summary>
    /// Property 5: StopAfter halts execution at the named phase.
    /// Register N mock phases, pick random StopAfter, verify only phases up to that point execute.
    /// Validates: Requirements 5.3, 9.2
    /// </summary>
    [Property]
    public Property StopAfter_HaltsExecutionAtNamedPhase()
    {
        var gen = from n in Gen.Choose(2, 20)
                  from stopIdx in Gen.Choose(0, 19).Select(i => i % n)
                  select (n, stopIdx);

        return Prop.ForAll(gen.ToArbitrary(), tuple =>
        {
            var (n, stopIdx) = tuple;
            var executionLog = new List<string>();
            var phases = CreatePhases(n, executionLog);
            var pipeline = BuildPipeline(phases);

            var options = new PipelineOptions
            {
                StopAfter = phases[stopIdx].Name,
                StopOnError = false
            };
            pipeline.Execute(CreateMinimalAst(), options);

            var expectedExecuted = phases.Take(stopIdx + 1).Select(p => p.Name).ToList();
            return executionLog.SequenceEqual(expectedExecuted);
        });
    }

    #endregion

    #region Property 6: StopOnError halts execution on phase failure

    /// <summary>
    /// Property 6: StopOnError halts execution on phase failure.
    /// Register N mock phases, make random phase fail, verify subsequent phases don't execute.
    /// Validates: Requirements 5.4, 2.2, 2.3
    /// </summary>
    [Property]
    public Property StopOnError_HaltsExecutionOnPhaseFailure()
    {
        var gen = from n in Gen.Choose(2, 20)
                  from failIdx in Gen.Choose(0, 19).Select(i => i % n)
                  select (n, failIdx);

        return Prop.ForAll(gen.ToArbitrary(), tuple =>
        {
            var (n, failIdx) = tuple;
            var executionLog = new List<string>();
            var phases = new List<ICompilerPhase>();
            for (var i = 0; i < n; i++)
            {
                phases.Add(new TrackingPhase(executionLog, shouldFail: i == failIdx)
                {
                    Name = $"Phase_{i}"
                });
            }
            var pipeline = BuildPipeline(phases);

            var options = new PipelineOptions { StopOnError = true };
            var result = pipeline.Execute(CreateMinimalAst(), options);

            // Only phases up to and including the failing phase should execute
            var expectedExecuted = Enumerable.Range(0, failIdx + 1).Select(i => $"Phase_{i}").ToList();
            return !result.Success && executionLog.SequenceEqual(expectedExecuted);
        });
    }

    #endregion

    #region Property 7: Diagnostics accumulation across phases

    /// <summary>
    /// Property 7: Diagnostics accumulation across phases.
    /// Register N mock phases each producing random diagnostics, verify union in result.
    /// Validates: Requirements 3.4, 4.4
    /// </summary>
    [Property]
    public Property DiagnosticsAccumulation_AcrossPhases()
    {
        var gen = from n in Gen.Choose(2, 15)
                  from diagCounts in Gen.ListOf(n, Gen.Choose(0, 5))
                  select (n, diagCounts: diagCounts.ToList());

        return Prop.ForAll(gen.ToArbitrary(), tuple =>
        {
            var (n, diagCounts) = tuple;
            var executionLog = new List<string>();
            var allExpectedDiags = new List<Diagnostic>();
            var phases = new List<ICompilerPhase>();

            for (var i = 0; i < n; i++)
            {
                var diags = new List<Diagnostic>();
                for (var d = 0; d < diagCounts[i]; d++)
                {
                    var diag = new Diagnostic(DiagnosticLevel.Warning, $"Phase_{i}_Diag_{d}");
                    diags.Add(diag);
                    allExpectedDiags.Add(diag);
                }
                phases.Add(new TrackingPhase(executionLog, diagnosticsToEmit: diags)
                {
                    Name = $"Phase_{i}"
                });
            }

            var pipeline = BuildPipeline(phases);
            var result = pipeline.Execute(CreateMinimalAst(), new PipelineOptions { StopOnError = false });

            // All diagnostics from all phases should be in the result
            var resultMessages = result.Diagnostics.Select(d => d.Message).ToList();
            var expectedMessages = allExpectedDiags.Select(d => d.Message).ToList();
            return resultMessages.SequenceEqual(expectedMessages);
        });
    }

    #endregion

    #region Property 8: Options forwarding to PhaseContext

    /// <summary>
    /// Property 8: Options forwarding to PhaseContext.
    /// Generate random targetFramework strings and EnableCaching bools, verify PhaseContext values.
    /// Validates: Requirements 3.5, 18.1, 18.3
    /// </summary>
    [Property]
    public Property OptionsForwarding_ToPhaseContext()
    {
        var gen = from framework in Gen.Elements("net8.0", "net9.0", "net10.0", "netstandard2.0", null as string)
                  from caching in Arb.Generate<bool>()
                  select (framework, caching);

        return Prop.ForAll(gen.ToArbitrary(), tuple =>
        {
            var (framework, caching) = tuple;
            var capturingPhase = new ContextCapturingPhase { Name = "Capture" };
            var pipeline = new TransformationPipeline();
            pipeline.RegisterPhase(capturingPhase);

            var options = new PipelineOptions { EnableCaching = caching };
            pipeline.Execute(CreateMinimalAst(), options, targetFramework: framework);

            var ctx = capturingPhase.CapturedContext;
            return ctx != null
                && ctx.TargetFramework == framework
                && ctx.EnableCaching == caching;
        });
    }

    #endregion

    #region Property 9: PhaseTimings populated for all executed phases

    /// <summary>
    /// Property 9: PhaseTimings populated for all executed phases.
    /// Execute pipeline, verify timing entries exist for all executed phases with non-negative values.
    /// Validates: Requirements 8.1, 8.5
    /// </summary>
    [Property]
    public Property PhaseTimings_PopulatedForAllExecutedPhases()
    {
        var gen = Gen.Choose(2, 20);
        return Prop.ForAll(gen.ToArbitrary(), n =>
        {
            var executionLog = new List<string>();
            var phases = CreatePhases(n, executionLog);
            var pipeline = BuildPipeline(phases);

            var result = pipeline.Execute(CreateMinimalAst(), new PipelineOptions { StopOnError = false });

            // Every executed phase should have a timing entry
            if (result.PhaseTimings.Count != n)
                return false;

            foreach (var phase in phases)
            {
                if (!result.PhaseTimings.ContainsKey(phase.Name))
                    return false;
                if (result.PhaseTimings[phase.Name] < TimeSpan.Zero)
                    return false;
            }
            return true;
        });
    }

    #endregion

    #region Property 11: DumpAfter invokes callback with correct arguments

    /// <summary>
    /// Property 11: DumpAfter invokes callback with correct arguments.
    /// Register N phases, set random DumpAfter subset, verify callback invoked with correct args.
    /// Validates: Requirements 5.5, 10.1
    /// </summary>
    [Property]
    public Property DumpAfter_InvokesCallbackWithCorrectArguments()
    {
        var gen = from n in Gen.Choose(2, 15)
                  from dumpMask in Gen.ListOf(n, Gen.Elements(true, false))
                  select (n, dumpMask: dumpMask.ToList());

        return Prop.ForAll(gen.ToArbitrary(), tuple =>
        {
            var (n, dumpMask) = tuple;
            var executionLog = new List<string>();
            var phases = CreatePhases(n, executionLog);
            var pipeline = BuildPipeline(phases);

            var dumpSet = new HashSet<string>();
            for (var i = 0; i < n; i++)
            {
                if (dumpMask[i])
                    dumpSet.Add(phases[i].Name);
            }

            var dumpLog = new List<(AstThing ast, string phaseName)>();
            var options = new PipelineOptions
            {
                DumpAfter = dumpSet,
                DumpCallback = (ast, name) => dumpLog.Add((ast, name)),
                StopOnError = false
            };
            pipeline.Execute(CreateMinimalAst(), options);

            // Callback should be invoked exactly for phases in DumpAfter
            var dumpedNames = dumpLog.Select(d => d.phaseName).ToList();
            var expectedDumped = phases.Where(p => dumpSet.Contains(p.Name)).Select(p => p.Name).ToList();

            if (!dumpedNames.SequenceEqual(expectedDumped))
                return false;

            // All dump entries should have non-null AST
            return dumpLog.All(d => d.ast != null);
        });
    }

    #endregion

    #region Property 12: Phases property exposes all registered phases

    /// <summary>
    /// Property 12: Phases property exposes all registered phases.
    /// Register N phases, verify Phases.Count == N and order matches.
    /// Validates: Requirements 4.6, 15.1, 15.2
    /// </summary>
    [Property]
    public Property PhasesProperty_ExposesAllRegisteredPhases()
    {
        var gen = Gen.Choose(1, 20);
        return Prop.ForAll(gen.ToArbitrary(), n =>
        {
            var executionLog = new List<string>();
            var phases = CreatePhases(n, executionLog);
            var pipeline = BuildPipeline(phases);

            if (pipeline.Phases.Count != n)
                return false;

            for (var i = 0; i < n; i++)
            {
                if (pipeline.Phases[i].Name != phases[i].Name)
                    return false;
            }
            return true;
        });
    }

    #endregion

    #region Property 13: GetCapabilitiesAfter returns cumulative capabilities

    /// <summary>
    /// Property 13: GetCapabilitiesAfter returns cumulative capabilities.
    /// Register phases with random capabilities, verify cumulative set at each point.
    /// Validates: Requirements 15.4
    /// </summary>
    [Property]
    public Property GetCapabilitiesAfter_ReturnsCumulativeCapabilities()
    {
        var gen = Gen.Choose(2, 15);
        return Prop.ForAll(gen.ToArbitrary(), n =>
        {
            var pipeline = new TransformationPipeline();
            var phases = new List<ICompilerPhase>();

            for (var i = 0; i < n; i++)
            {
                var phase = new TrackingPhase(new List<string>())
                {
                    Name = $"Phase_{i}",
                    ProvidedCapabilities = new[] { $"Cap_{i}" }
                };
                phases.Add(phase);
                pipeline.RegisterPhase(phase);
            }

            // Verify cumulative capabilities at each phase
            var expectedCaps = new HashSet<string>();
            foreach (var phase in phases)
            {
                expectedCaps.Add(phase.ProvidedCapabilities[0]);
                var actual = pipeline.GetCapabilitiesAfter(phase.Name);
                if (!actual.SetEquals(expectedCaps))
                    return false;
            }
            return true;
        });
    }

    #endregion

    #region Property 15: Exception handling preserves phase context

    /// <summary>
    /// Property 15: Exception handling preserves phase context.
    /// Register mock phase that throws, verify PipelineResult contains error diagnostic with phase name.
    /// Validates: Requirements 13.1
    /// </summary>
    [Property]
    public Property ExceptionHandling_PreservesPhaseContext()
    {
        var gen = Gen.Choose(2, 15);
        return Prop.ForAll(gen.ToArbitrary(), n =>
        {
            var throwIdx = n / 2; // throw in the middle
            var executionLog = new List<string>();
            var phases = new List<ICompilerPhase>();

            for (var i = 0; i < n; i++)
            {
                if (i == throwIdx)
                {
                    phases.Add(new ThrowingPhase { Name = $"Phase_{i}" });
                }
                else
                {
                    phases.Add(new TrackingPhase(executionLog) { Name = $"Phase_{i}" });
                }
            }

            var pipeline = BuildPipeline(phases);
            var result = pipeline.Execute(CreateMinimalAst(), new PipelineOptions { StopOnError = true });

            // Pipeline should return failure
            if (result.Success)
                return false;

            // Diagnostics should contain an error mentioning the throwing phase name
            var throwingPhaseName = $"Phase_{throwIdx}";
            var hasErrorDiag = result.Diagnostics.Any(d =>
                d.Level == DiagnosticLevel.Error &&
                d.Message.Contains(throwingPhaseName));

            if (!hasErrorDiag)
                return false;

            // Phases after the throwing phase should not have executed
            var phasesAfterThrow = Enumerable.Range(throwIdx + 1, n - throwIdx - 1)
                .Select(i => $"Phase_{i}");
            return !executionLog.Any(name => phasesAfterThrow.Contains(name));
        });
    }

    #endregion
}
