using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fifth.System;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using VDS.RDF;

namespace runtime_integration_tests;

/// <summary>
/// Property-based tests for TriG literal loading.
/// These tests do NOT require NET10_0_OR_GREATER since Store.LoadFromTriG uses the in-memory path.
/// </summary>
[Trait("Category", "PBT")]
public class TriG_Property_Tests
{
    // ========================================================================
    // Custom FsCheck Generators for TriG content
    // ========================================================================

    /// <summary>
    /// Represents a generated triple with simple IRI components and a literal object.
    /// </summary>
    public record TriGTriple(int SubjectId, int PredicateId, string ObjectValue);

    /// <summary>
    /// Represents a named graph with an ID and a set of triples.
    /// </summary>
    public record TriGGraph(int GraphId, TriGTriple[] Triples);

    /// <summary>
    /// Represents a complete TriG dataset with multiple named graphs.
    /// </summary>
    public record TriGDataset(TriGGraph[] Graphs);

    /// <summary>
    /// Generates a simple alphanumeric literal value safe for TriG syntax.
    /// </summary>
    private static Gen<string> GenSafeLiteral()
    {
        return Gen.Elements(
            "alpha", "beta", "gamma", "delta", "epsilon",
            "value1", "value2", "value3", "hello", "world",
            "foo", "bar", "baz", "test", "data"
        );
    }

    /// <summary>
    /// Generates a TriG triple with unique numeric IDs for subject/predicate and a safe literal object.
    /// </summary>
    private static Gen<TriGTriple> GenTriGTriple()
    {
        return from subjId in Gen.Choose(1, 1000)
               from predId in Gen.Choose(1, 1000)
               from obj in GenSafeLiteral()
               select new TriGTriple(subjId, predId, obj);
    }

    /// <summary>
    /// Generates a named graph with 1-5 triples and a unique graph ID.
    /// </summary>
    private static Gen<TriGGraph> GenTriGGraph(int graphId)
    {
        return from tripleCount in Gen.Choose(1, 5)
               from triples in Gen.ArrayOf(tripleCount, GenTriGTriple())
               select new TriGGraph(graphId, triples);
    }

    /// <summary>
    /// Generates a complete TriG dataset with 1-5 named graphs.
    /// </summary>
    private static Gen<TriGDataset> GenTriGDataset()
    {
        return from graphCount in Gen.Choose(1, 5)
               from graphs in Gen.Sequence(
                   Enumerable.Range(1, graphCount).Select(i => GenTriGGraph(i))
               )
               select new TriGDataset(graphs.ToArray());
    }

    /// <summary>
    /// Custom Arbitrary registration for FsCheck [Property] attribute.
    /// </summary>
    public class TriGArbitrary
    {
        public static Arbitrary<TriGDataset> TriGDataset() =>
            Arb.From(GenTriGDataset());
    }

    // ========================================================================
    // Helpers
    // ========================================================================

    private static string GraphUri(int graphId) => $"http://example.org/g{graphId}";
    private static string SubjectUri(int id) => $"http://example.org/s{id}";
    private static string PredicateUri(int id) => $"http://example.org/p{id}";

    /// <summary>
    /// Builds a valid TriG content string from a generated dataset.
    /// </summary>
    private static string BuildTriGContent(TriGDataset dataset)
    {
        var sb = new StringBuilder();
        foreach (var graph in dataset.Graphs)
        {
            sb.AppendLine($"<{GraphUri(graph.GraphId)}> {{");
            foreach (var triple in graph.Triples)
            {
                sb.AppendLine($"    <{SubjectUri(triple.SubjectId)}> <{PredicateUri(triple.PredicateId)}> \"{triple.ObjectValue}\" .");
            }
            sb.AppendLine("}");
        }
        return sb.ToString();
    }

    /// <summary>
    /// Computes the expected set of unique (subject, predicate, object) string tuples for a graph,
    /// accounting for RDF set semantics (duplicate triples are collapsed).
    /// </summary>
    private static HashSet<(string S, string P, string O)> ExpectedTriples(TriGGraph graph)
    {
        var set = new HashSet<(string, string, string)>();
        foreach (var t in graph.Triples)
        {
            set.Add((SubjectUri(t.SubjectId), PredicateUri(t.PredicateId), t.ObjectValue));
        }
        return set;
    }

    /// <summary>
    /// Extracts a comparable set of (subject, predicate, object) string tuples from a loaded graph.
    /// For literal objects, extracts just the lexical value (without the XSD datatype suffix).
    /// </summary>
    private static HashSet<(string S, string P, string O)> ExtractTripleSet(Fifth.System.Graph graph)
    {
        var set = new HashSet<(string, string, string)>();
        foreach (var t in graph.Triples)
        {
            var objValue = t.Object is VDS.RDF.ILiteralNode literal
                ? literal.Value
                : t.Object.ToString();
            set.Add((t.Subject.ToString(), t.Predicate.ToString(), objValue));
        }
        return set;
    }

    // ========================================================================
    // Property 5: TriG literal loading produces functional store
    // Validates: Requirements 6.5, 9.5
    // ========================================================================

    /// <summary>
    /// For any valid TriG content string with 1-5 named graphs each containing 1-5 triples,
    /// Store.LoadFromTriG(trigContent) should produce a store from which all named graphs
    /// can be loaded, and each loaded graph should contain the triples specified in the
    /// TriG content for that graph.
    ///
    /// **Validates: Requirements 6.5, 9.5**
    /// </summary>
    [Property(MaxTest = 100, Arbitrary = new[] { typeof(TriGArbitrary) })]
    public void TriGLiteral_Loading_ProducesFunctionalStore(TriGDataset dataset)
    {
        // Arrange: build TriG content from the generated dataset
        var trigContent = BuildTriGContent(dataset);

        // Act: load the TriG content into a store
        var store = Store.LoadFromTriG(trigContent);

        // Assert: each named graph is recoverable with the correct triples
        foreach (var expectedGraph in dataset.Graphs)
        {
            var graphUri = new Uri(GraphUri(expectedGraph.GraphId));
            var loadedGraph = store.LoadGraph(graphUri);

            var expected = ExpectedTriples(expectedGraph);
            var actual = ExtractTripleSet(loadedGraph);

            actual.Should().BeEquivalentTo(expected,
                because: $"graph <{graphUri}> should contain all triples from the TriG content");
        }
    }
}
