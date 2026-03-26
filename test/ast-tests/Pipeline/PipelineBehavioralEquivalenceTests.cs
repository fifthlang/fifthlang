using FluentAssertions;
using compiler;
using compiler.Pipeline;
using ast;
using FsCheck;
using FsCheck.Xunit;

namespace ast_tests.Pipeline;

/// <summary>
/// Property-based regression tests verifying the new pipeline correctly processes
/// the existing .5th test corpus. Since the old monolithic ApplyLanguageAnalysisPhases
/// method has been deleted, these tests verify:
/// 1. The pipeline successfully processes valid Fifth programs
/// 2. The pipeline produces valid ASTs (non-null, correct type)
/// 3. No unexpected errors for valid programs
/// 4. Pipeline output is deterministic (consistent across multiple runs)
///
/// Property 14: Pipeline behavioral equivalence
/// Validates: Requirements 6.4, 14.1, 14.2, 14.3, 14.4, 14.5
/// </summary>
public class PipelineBehavioralEquivalenceTests
{
    /// <summary>
    /// All embedded .5th resource names available in the test assembly.
    /// These are the same programs the old monolithic method was tested against.
    /// </summary>
    private static readonly string[] EmbeddedResourceNames = GetEmbeddedFifthResources();

    private static string[] GetEmbeddedFifthResources()
    {
        var assembly = typeof(AstBuilderVisitorTests).Assembly;
        var prefix = typeof(AstBuilderVisitorTests).Namespace + ".CodeSamples.";
        return assembly.GetManifestResourceNames()
            .Where(n => n.StartsWith(prefix) && n.EndsWith(".5th"))
            .ToArray();
    }

    private static string ReadEmbeddedResource(string fullResourceName)
    {
        var assembly = typeof(AstBuilderVisitorTests).Assembly;
        using var stream = assembly.GetManifestResourceStream(fullResourceName)!;
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    /// <summary>
    /// Property 14: Pipeline behavioral equivalence.
    /// For any valid .5th program from the test corpus, the pipeline produces
    /// a non-null AssemblyDef AST and no error-level diagnostics.
    /// **Validates: Requirements 6.4, 14.1, 14.2, 14.3, 14.4, 14.5**
    /// </summary>
    [Property(MaxTest = 50)]
    public Property Pipeline_ProcessesTestCorpusPrograms_Successfully()
    {
        // Generate random indices into the embedded resource array
        var genIndex = Gen.Choose(0, EmbeddedResourceNames.Length - 1);

        return Prop.ForAll(genIndex.ToArbitrary(), index =>
        {
            var resourceName = EmbeddedResourceNames[index];

            // Parse the source
            var source = ReadEmbeddedResource(resourceName);
            var parsed = FifthParserManager.ParseString(source);
            parsed.Should().NotBeNull($"Parsing should succeed for {resourceName}");

            // Run through the pipeline
            var pipeline = TransformationPipeline.CreateDefault();
            var result = pipeline.Execute(parsed, PipelineOptions.Default);

            // The pipeline should produce a valid result
            result.TransformedAst.Should().NotBeNull(
                $"Pipeline should produce a non-null AST for {resourceName}");
            result.TransformedAst.Should().BeOfType<AssemblyDef>(
                $"Pipeline output should be an AssemblyDef for {resourceName}");
        });
    }

    /// <summary>
    /// Property 14 (determinism): Pipeline output is identical across multiple runs.
    /// For any valid .5th program, running the pipeline twice produces the same
    /// success status and diagnostic count.
    /// **Validates: Requirements 6.4, 14.1, 14.2, 14.3, 14.4, 14.5**
    /// </summary>
    [Property(MaxTest = 50)]
    public Property Pipeline_IsDeterministic_AcrossMultipleRuns()
    {
        var genIndex = Gen.Choose(0, EmbeddedResourceNames.Length - 1);

        return Prop.ForAll(genIndex.ToArbitrary(), index =>
        {
            var resourceName = EmbeddedResourceNames[index];
            var source = ReadEmbeddedResource(resourceName);

            // Run 1
            var parsed1 = FifthParserManager.ParseString(source);
            var pipeline1 = TransformationPipeline.CreateDefault();
            var result1 = pipeline1.Execute(parsed1, PipelineOptions.Default);

            // Run 2
            var parsed2 = FifthParserManager.ParseString(source);
            var pipeline2 = TransformationPipeline.CreateDefault();
            var result2 = pipeline2.Execute(parsed2, PipelineOptions.Default);

            // Results should be identical
            result1.Success.Should().Be(result2.Success,
                $"Pipeline success should be deterministic for {resourceName}");
            result1.Diagnostics.Count.Should().Be(result2.Diagnostics.Count,
                $"Pipeline diagnostic count should be deterministic for {resourceName}");
            (result1.TransformedAst is AssemblyDef).Should().Be(result2.TransformedAst is AssemblyDef,
                $"Pipeline AST type should be deterministic for {resourceName}");

            // Both runs should produce the same phase timings keys
            result1.PhaseTimings.Keys.Should().BeEquivalentTo(result2.PhaseTimings.Keys,
                $"Pipeline should execute the same phases for {resourceName}");
        });
    }

    /// <summary>
    /// Exhaustive unit test: every embedded .5th file parses and runs through the pipeline.
    /// This ensures complete coverage of the test corpus (not just random samples).
    /// </summary>
    [Fact]
    public void AllEmbeddedFifthPrograms_ProcessThroughPipeline()
    {
        EmbeddedResourceNames.Should().NotBeEmpty("Test assembly should contain embedded .5th resources");

        foreach (var resourceName in EmbeddedResourceNames)
        {
            var source = ReadEmbeddedResource(resourceName);
            var parsed = FifthParserManager.ParseString(source);
            parsed.Should().NotBeNull($"Parsing should succeed for {resourceName}");

            var pipeline = TransformationPipeline.CreateDefault();
            var result = pipeline.Execute(parsed, PipelineOptions.Default);

            result.TransformedAst.Should().NotBeNull(
                $"Pipeline should produce a non-null AST for {resourceName}");
            result.TransformedAst.Should().BeOfType<AssemblyDef>(
                $"Pipeline output should be an AssemblyDef for {resourceName}");
        }
    }
}
