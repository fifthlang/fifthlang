using System;
using System.Collections.Generic;
using System.Linq;
using Fifth.System;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using VDS.RDF;

namespace runtime_integration_tests;

/// <summary>
/// Property-based tests for QuadStore-backed stores.
/// Uses FsCheck to generate random RDF graphs and validate universal properties.
/// </summary>
[Trait("Category", "QuadStore")]
[Trait("Category", "PBT")]
public class QuadStore_Property_Tests : IDisposable
{
    private readonly List<string> _tempDirs = new();

    private string CreateTempDir()
    {
        var path = Path.Combine(Path.GetTempPath(), $"fifth-qs-pbt-{Guid.NewGuid():N}");
        Directory.CreateDirectory(path);
        _tempDirs.Add(path);
        return path;
    }

    public void Dispose()
    {
        foreach (var dir in _tempDirs)
        {
            try { if (Directory.Exists(dir)) Directory.Delete(dir, true); } catch { }
        }
    }

    // ========================================================================
    // Custom FsCheck Generators
    // ========================================================================

    /// <summary>
    /// Generates a valid IRI string like http://example.org/{guid}
    /// </summary>
    private static Gen<string> GenIri()
    {
        return Gen.Fresh(() => $"http://example.org/{Guid.NewGuid():N}");
    }

    /// <summary>
    /// Generates a random literal string value (non-empty, alphanumeric).
    /// </summary>
    private static Gen<string> GenLiteralValue()
    {
        return Arb.Generate<NonEmptyString>().Select(s =>
        {
            // Sanitize to simple alphanumeric to avoid encoding issues
            var clean = new string(s.Get.Where(c => char.IsLetterOrDigit(c) || c == ' ').ToArray());
            return string.IsNullOrWhiteSpace(clean) ? "value" : clean;
        });
    }

    /// <summary>
    /// Represents a generated RDF triple as simple string components.
    /// </summary>
    public record GeneratedTriple(string SubjectIri, string PredicateIri, string ObjectLiteral);

    /// <summary>
    /// Generates a random RDF triple (subject IRI, predicate IRI, object literal).
    /// </summary>
    private static Gen<GeneratedTriple> GenTriple()
    {
        return from subj in GenIri()
               from pred in GenIri()
               from obj in GenLiteralValue()
               select new GeneratedTriple(subj, pred, obj);
    }

    /// <summary>
    /// Represents a generated RDF graph with a base URI and a set of triples.
    /// </summary>
    public record GeneratedGraph(string BaseUri, GeneratedTriple[] Triples);

    /// <summary>
    /// Generates a random RDF graph with 1-10 triples and a unique base URI.
    /// </summary>
    private static Gen<GeneratedGraph> GenGraph()
    {
        return from baseUri in GenIri()
               from tripleCount in Gen.Choose(1, 10)
               from triples in Gen.ArrayOf(tripleCount, GenTriple())
               select new GeneratedGraph(baseUri, triples);
    }

    /// <summary>
    /// Represents the three SPARQL query forms we test for result type matching.
    /// </summary>
    public enum QueryForm { Select, Ask, Construct }

    /// <summary>
    /// Generates a random QueryForm (SELECT, ASK, or CONSTRUCT).
    /// </summary>
    private static Gen<QueryForm> GenQueryForm()
    {
        return Gen.Elements(QueryForm.Select, QueryForm.Ask, QueryForm.Construct);
    }

    /// <summary>
    /// Custom Arbitrary registrations for FsCheck [Property] attribute.
    /// FsCheck discovers Arbitrary instances via public static properties on this class.
    /// </summary>
    public class GraphArbitrary
    {
        public static Arbitrary<GeneratedGraph> GeneratedGraph() =>
            Arb.From(GenGraph());

        public static Arbitrary<QueryForm> QueryForm() =>
            Arb.From(GenQueryForm());
    }

    // ========================================================================
    // Helper: materialize a GeneratedGraph into a Fifth.System.Graph
    // ========================================================================

    private static Fifth.System.Graph MaterializeGraph(GeneratedGraph gen)
    {
        var vdsGraph = new VDS.RDF.Graph(new Uri(gen.BaseUri));
        var factory = new NodeFactory();

        foreach (var t in gen.Triples)
        {
            var s = vdsGraph.CreateUriNode(new Uri(t.SubjectIri));
            var p = vdsGraph.CreateUriNode(new Uri(t.PredicateIri));
            var o = vdsGraph.CreateLiteralNode(t.ObjectLiteral);
            vdsGraph.Assert(new VDS.RDF.Triple(s, p, o));
        }

        return Fifth.System.Graph.FromVds(vdsGraph);
    }

    /// <summary>
    /// Extracts a comparable set of (subject, predicate, object) string tuples from a graph.
    /// </summary>
    private static HashSet<(string S, string P, string O)> ExtractTripleSet(Fifth.System.Graph graph)
    {
        var set = new HashSet<(string, string, string)>();
        foreach (var t in graph.Triples)
        {
            set.Add((t.Subject.ToString(), t.Predicate.ToString(), t.Object.ToString()));
        }
        return set;
    }

    // ========================================================================
    // Property 1: Save/Load round-trip on QuadStore-backed stores
    // Validates: Requirements 2.2, 2.3, 8.2, 8.3
    // ========================================================================

    /// <summary>
    /// For any valid RDF graph with an assigned URI and any set of triples,
    /// saving that graph to a QuadStore-backed store and then loading it back
    /// by the same URI should produce a graph containing exactly the same set of triples.
    ///
    /// **Validates: Requirements 2.2, 2.3, 8.2, 8.3**
    /// </summary>
    [Property(MaxTest = 100, Arbitrary = new[] { typeof(GraphArbitrary) })]
    public void SaveLoad_RoundTrip_PreservesTriples(GeneratedGraph generated)
    {
        // Arrange
        var tempDir = CreateTempDir();
        var store = Store.CreateFileStore(tempDir);
        var graph = MaterializeGraph(generated);
        var graphUri = new Uri(generated.BaseUri);

        // Act: save then load
        store.SaveGraph(graph);
        var loaded = store.LoadGraph(graphUri);

        // Assert: triple sets must be equal
        var originalTriples = ExtractTripleSet(graph);
        var loadedTriples = ExtractTripleSet(loaded);

        loadedTriples.Should().BeEquivalentTo(originalTriples,
            because: "saving and loading a graph should preserve all triples");
    }

    // ========================================================================
    // Property 2: Persistence round-trip across store instances
    // Validates: Requirements 3.1, 3.2, 4.5
    // ========================================================================

    /// <summary>
    /// For any valid file system path, any RDF graph with an assigned URI, and any set of triples:
    /// saving the graph to a QuadStore-backed store at that path, closing the store, then reopening
    /// a new store at the same path and loading the graph by URI should produce a graph containing
    /// exactly the same set of triples.
    ///
    /// **Validates: Requirements 3.1, 3.2, 4.5**
    /// </summary>
    [Property(MaxTest = 100, Arbitrary = new[] { typeof(GraphArbitrary) })]
    public void Persistence_RoundTrip_AcrossStoreInstances_PreservesTriples(GeneratedGraph generated)
    {
        // Arrange
        var tempDir = CreateTempDir();
        var graph = MaterializeGraph(generated);
        var graphUri = new Uri(generated.BaseUri);

        // Act: save to first store instance, then persist to disk
        var quadStore1 = new TripleStore.Core.QuadStore(tempDir);
        var provider1 = new TripleStore.Core.QuadStoreStorageProvider(quadStore1);
        var store1 = Store.FromVds(provider1);
        store1.SaveGraph(graph);
        // QuadStore requires explicit SaveAll() to flush data to disk
        quadStore1.SaveAll();
        quadStore1.Dispose();

        // Reopen a NEW store at the same path and load the graph
        var quadStore2 = new TripleStore.Core.QuadStore(tempDir);
        quadStore2.LoadAll();
        var provider2 = new TripleStore.Core.QuadStoreStorageProvider(quadStore2);
        var store2 = Store.FromVds(provider2);
        var loaded = store2.LoadGraph(graphUri);

        // Assert: triple sets must be equal
        var originalTriples = ExtractTripleSet(graph);
        var loadedTriples = ExtractTripleSet(loaded);

        loadedTriples.Should().BeEquivalentTo(originalTriples,
            because: "persisted data should survive across store instances at the same path");
    }

    // ========================================================================
    // Property 4: Query result type matches query form on QuadStore-backed stores
    // Validates: Requirements 7.1, 7.2, 7.3, 7.4
    // ========================================================================

    /// <summary>
    /// For any SPARQL query executed against a QuadStore-backed store containing data,
    /// the result type should match the query form: SELECT queries produce Result.TabularResult,
    /// ASK queries produce Result.BooleanResult, and CONSTRUCT queries produce Result.GraphResult.
    ///
    /// **Validates: Requirements 7.1, 7.2, 7.3, 7.4**
    /// </summary>
    [Property(MaxTest = 100, Arbitrary = new[] { typeof(GraphArbitrary) })]
    public void QueryResultType_MatchesQueryForm(GeneratedGraph generated, QueryForm queryForm)
    {
        // Arrange: create a QuadStore-backed store and load random triples
        var tempDir = CreateTempDir();
        var store = Store.CreateFileStore(tempDir);
        var graph = MaterializeGraph(generated);
        store.SaveGraph(graph);

        // Build the SPARQL query string based on the generated query form
        var sparqlText = queryForm switch
        {
            QueryForm.Select => "SELECT ?s ?p ?o WHERE { ?s ?p ?o }",
            QueryForm.Ask => "ASK WHERE { ?s ?p ?o }",
            QueryForm.Construct => "CONSTRUCT { ?s ?p ?o } WHERE { ?s ?p ?o }",
            _ => throw new ArgumentOutOfRangeException(nameof(queryForm))
        };

        // Act: parse and execute the query
        var query = Query.Parse(sparqlText);
        var result = QueryApplicationExecutor.Execute(query, store);

        // Assert: result type must match the query form
        switch (queryForm)
        {
            case QueryForm.Select:
                result.Should().BeOfType<Fifth.System.Result.TabularResult>(
                    because: "SELECT queries must produce TabularResult");
                break;
            case QueryForm.Ask:
                result.Should().BeOfType<Fifth.System.Result.BooleanResult>(
                    because: "ASK queries must produce BooleanResult");
                break;
            case QueryForm.Construct:
                result.Should().BeOfType<Fifth.System.Result.GraphResult>(
                    because: "CONSTRUCT queries must produce GraphResult");
                break;
        }
    }
}
