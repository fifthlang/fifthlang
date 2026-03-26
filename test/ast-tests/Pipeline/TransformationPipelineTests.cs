using FluentAssertions;
using compiler;
using compiler.Pipeline;
using compiler.Pipeline.Phases;
using ast;
using FsCheck;
using FsCheck.Xunit;

namespace ast_tests.Pipeline;

/// <summary>
/// Unit tests for TransformationPipeline structure and configuration.
/// Validates: Requirements 17.1, 17.2, 17.3, 17.4, 15.1, 15.2, 15.4
/// </summary>
public class TransformationPipelineTests
{
    #region Default Pipeline Structure Tests

    [Fact]
    public void DefaultPipeline_HasExpected38Phases()
    {
        var pipeline = TransformationPipeline.CreateDefault();
        pipeline.Phases.Should().HaveCount(38);
    }

    [Fact]
    public void DefaultPipeline_AllPhaseNamesAreUnique()
    {
        var pipeline = TransformationPipeline.CreateDefault();
        var names = pipeline.Phases.Select(p => p.Name).ToList();
        names.Should().OnlyHaveUniqueItems();
    }

    [Fact]
    public void DefaultPipeline_AllDependenciesAreSatisfiedByEarlierPhases()
    {
        var pipeline = TransformationPipeline.CreateDefault();
        var availableCapabilities = new HashSet<string>();

        foreach (var phase in pipeline.Phases)
        {
            foreach (var dep in phase.DependsOn)
            {
                availableCapabilities.Should().Contain(dep,
                    $"Phase '{phase.Name}' depends on '{dep}' which is not provided by any earlier phase");
            }

            foreach (var cap in phase.ProvidedCapabilities)
            {
                availableCapabilities.Add(cap);
            }
        }
    }

    [Fact]
    public void DefaultPipeline_FirstPhaseIsTreeLinkage_LastIsTailCallOptimization()
    {
        var pipeline = TransformationPipeline.CreateDefault();
        pipeline.Phases.First().Name.Should().Be("TreeLinkage");
        pipeline.Phases.Last().Name.Should().Be("TailCallOptimization");
    }

    [Fact]
    public void DefaultPipelineOptions_TailCallOptimizationIsSkipped()
    {
        PipelineOptions.Default.SkipPhases.Should().Contain("TailCallOptimization");
    }

    #endregion

    #region PipelineOptions.Default Tests

    [Fact]
    public void PipelineOptionsDefault_HasStopOnErrorTrue()
    {
        PipelineOptions.Default.StopOnError.Should().BeTrue();
    }

    [Fact]
    public void PipelineOptionsDefault_SkipPhasesContainsOnlyTailCallOptimization()
    {
        PipelineOptions.Default.SkipPhases.Should().BeEquivalentTo(new[] { "TailCallOptimization" });
    }

    [Fact]
    public void PipelineOptionsDefault_StopAfterIsNull()
    {
        PipelineOptions.Default.StopAfter.Should().BeNull();
    }

    #endregion

    #region PhaseResult Factory Tests

    [Fact]
    public void PhaseResultOk_CreatesSuccessTrue()
    {
        var ast = CreateMinimalAst();
        var result = PhaseResult.Ok(ast);
        result.Success.Should().BeTrue();
        result.TransformedAst.Should().BeSameAs(ast);
        result.Diagnostics.Should().BeEmpty();
    }

    [Fact]
    public void PhaseResultFail_CreatesSuccessFalse()
    {
        var ast = CreateMinimalAst();
        var diagnostics = new List<Diagnostic>
        {
            new(DiagnosticLevel.Error, "test error")
        };
        var result = PhaseResult.Fail(ast, diagnostics);
        result.Success.Should().BeFalse();
        result.TransformedAst.Should().BeSameAs(ast);
        result.Diagnostics.Should().HaveCount(1);
    }

    #endregion

    #region RegisterPhase Tests

    [Fact]
    public void RegisterPhase_ThrowsOnUnsatisfiedDependency()
    {
        var pipeline = new TransformationPipeline();
        var phase = new MockPhase
        {
            Name = "NeedsMissing",
            DependsOn = new[] { "NonExistentCapability" }
        };

        var act = () => pipeline.RegisterPhase(phase);
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*NonExistentCapability*");
    }

    [Fact]
    public void RegisterPhase_SucceedsForPhaseWithEmptyDependsOn()
    {
        var pipeline = new TransformationPipeline();
        var phase = new MockPhase
        {
            Name = "NoDeps",
            DependsOn = Array.Empty<string>()
        };

        var act = () => pipeline.RegisterPhase(phase);
        act.Should().NotThrow();
        pipeline.Phases.Should().HaveCount(1);
    }

    #endregion

    #region GetCapabilitiesAfter Tests

    [Fact]
    public void GetCapabilitiesAfter_ReturnsCumulativeCapabilities()
    {
        var pipeline = new TransformationPipeline();
        pipeline.RegisterPhase(new MockPhase
        {
            Name = "Phase1",
            ProvidedCapabilities = new[] { "Cap1" }
        });
        pipeline.RegisterPhase(new MockPhase
        {
            Name = "Phase2",
            DependsOn = new[] { "Cap1" },
            ProvidedCapabilities = new[] { "Cap2" }
        });
        pipeline.RegisterPhase(new MockPhase
        {
            Name = "Phase3",
            DependsOn = new[] { "Cap2" },
            ProvidedCapabilities = new[] { "Cap3" }
        });

        var capsAfterPhase1 = pipeline.GetCapabilitiesAfter("Phase1");
        capsAfterPhase1.Should().BeEquivalentTo(new[] { "Cap1" });

        var capsAfterPhase2 = pipeline.GetCapabilitiesAfter("Phase2");
        capsAfterPhase2.Should().BeEquivalentTo(new[] { "Cap1", "Cap2" });

        var capsAfterPhase3 = pipeline.GetCapabilitiesAfter("Phase3");
        capsAfterPhase3.Should().BeEquivalentTo(new[] { "Cap1", "Cap2", "Cap3" });
    }

    #endregion

    #region Phases Property Tests

    [Fact]
    public void PhasesProperty_ReturnsAllRegisteredPhasesInOrder()
    {
        var pipeline = new TransformationPipeline();
        var phase1 = new MockPhase { Name = "A" };
        var phase2 = new MockPhase { Name = "B" };
        var phase3 = new MockPhase { Name = "C" };

        pipeline.RegisterPhase(phase1);
        pipeline.RegisterPhase(phase2);
        pipeline.RegisterPhase(phase3);

        pipeline.Phases.Should().HaveCount(3);
        pipeline.Phases[0].Name.Should().Be("A");
        pipeline.Phases[1].Name.Should().Be("B");
        pipeline.Phases[2].Name.Should().Be("C");
    }

    #endregion

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

    private class MockPhase : ICompilerPhase
    {
        public string Name { get; init; } = "Mock";
        public IReadOnlyList<string> DependsOn { get; init; } = Array.Empty<string>();
        public IReadOnlyList<string> ProvidedCapabilities { get; init; } = Array.Empty<string>();
        public PhaseResult Transform(AstThing ast, PhaseContext context) => PhaseResult.Ok(ast);
    }

    #endregion
}


/// <summary>
/// Property-based tests for pipeline structural integrity and dependency validation.
/// </summary>
public class TransformationPipelinePropertyTests
{
    /// <summary>
    /// Property 1: Default pipeline structural integrity.
    /// Verify unique names, satisfied deps, expected phase count on CreateDefault().
    /// Validates: Requirements 1.2, 6.1, 7.1, 17.1, 17.2, 17.3, 17.4
    /// </summary>
    [Property]
    public bool DefaultPipeline_HasStructuralIntegrity()
    {
        var pipeline = TransformationPipeline.CreateDefault();

        // Expected phase count
        if (pipeline.Phases.Count != 38)
            return false;

        // All names unique
        var names = pipeline.Phases.Select(p => p.Name).ToList();
        if (names.Distinct().Count() != names.Count)
            return false;

        // All dependencies satisfied by earlier phases
        var availableCapabilities = new HashSet<string>();
        foreach (var phase in pipeline.Phases)
        {
            foreach (var dep in phase.DependsOn)
            {
                if (!availableCapabilities.Contains(dep))
                    return false;
            }

            foreach (var cap in phase.ProvidedCapabilities)
                availableCapabilities.Add(cap);
        }

        return true;
    }

    /// <summary>
    /// Property 2: Dependency validation rejects unsatisfied dependencies.
    /// Generate random phases with random DependsOn strings, verify RegisterPhase throws when deps unsatisfied.
    /// Validates: Requirements 4.2, 7.1, 7.2, 7.3, 7.4
    /// </summary>
    [Property]
    public Property RegisterPhase_RejectsUnsatisfiedDependencies()
    {
        var genPhaseName = Arb.Generate<NonEmptyString>().Select(s => "Phase_" + s.Get.Replace(" ", ""));
        var genDepName = Arb.Generate<NonEmptyString>().Select(s => "Dep_" + s.Get.Replace(" ", ""));

        var gen = from phaseName in genPhaseName
                  from depName in genDepName
                  where phaseName != depName
                  select (phaseName, depName);

        return Prop.ForAll(gen.ToArbitrary(), tuple =>
        {
            var (phaseName, depName) = tuple;
            var pipeline = new TransformationPipeline();
            var phase = new MockPhase
            {
                Name = phaseName,
                DependsOn = new[] { depName }
            };

            try
            {
                pipeline.RegisterPhase(phase);
                return false; // Should have thrown
            }
            catch (InvalidOperationException ex)
            {
                return ex.Message.Contains(depName);
            }
        });
    }

    /// <summary>
    /// Property 2 (complement): Phases with empty DependsOn always register successfully.
    /// Validates: Requirements 7.4
    /// </summary>
    [Property]
    public Property RegisterPhase_SucceedsWithEmptyDependsOn()
    {
        var genPhaseName = Arb.Generate<NonEmptyString>().Select(s => "Phase_" + s.Get.Replace(" ", ""));

        return Prop.ForAll(genPhaseName.ToArbitrary(), phaseName =>
        {
            var pipeline = new TransformationPipeline();
            var phase = new MockPhase
            {
                Name = phaseName,
                DependsOn = Array.Empty<string>()
            };

            try
            {
                pipeline.RegisterPhase(phase);
                return true;
            }
            catch
            {
                return false;
            }
        });
    }

    /// <summary>
    /// Property 2 (satisfied case): When a dependency IS provided by an earlier phase,
    /// registration succeeds.
    /// Validates: Requirements 7.1, 7.4
    /// </summary>
    [Property]
    public Property RegisterPhase_SucceedsWhenDependencySatisfied()
    {
        var genName = Arb.Generate<NonEmptyString>().Select(s => "N_" + s.Get.Replace(" ", ""));

        var gen = from providerName in genName
                  from capName in genName
                  from consumerName in genName
                  where providerName != consumerName
                  select (providerName, capName, consumerName);

        return Prop.ForAll(gen.ToArbitrary(), tuple =>
        {
            var (providerName, capName, consumerName) = tuple;
            var pipeline = new TransformationPipeline();

            pipeline.RegisterPhase(new MockPhase
            {
                Name = providerName,
                ProvidedCapabilities = new[] { capName }
            });

            try
            {
                pipeline.RegisterPhase(new MockPhase
                {
                    Name = consumerName,
                    DependsOn = new[] { capName }
                });
                return true;
            }
            catch
            {
                return false;
            }
        });
    }

    private class MockPhase : ICompilerPhase
    {
        public string Name { get; init; } = "Mock";
        public IReadOnlyList<string> DependsOn { get; init; } = Array.Empty<string>();
        public IReadOnlyList<string> ProvidedCapabilities { get; init; } = Array.Empty<string>();
        public PhaseResult Transform(AstThing ast, PhaseContext context) => PhaseResult.Ok(ast);
    }
}
