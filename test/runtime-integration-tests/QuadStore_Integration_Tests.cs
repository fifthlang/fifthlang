using System.Reflection;
using FluentAssertions;
using Fifth.System;
using VDS.RDF.Storage;

namespace runtime_integration_tests;

/// <summary>
/// Integration tests for Store.CreateFileStore, new KG built-in functions,
/// sparql_store deprecation, CreateStore(), and CreateInMemory() regression.
/// Validates: Requirements 2.2, 2.3, 2.4, 3.4, 4.7, 9.4
/// </summary>
[Trait("Category", "QuadStore")]
public class QuadStore_Integration_Tests : IDisposable
{
    private readonly List<string> _tempDirs = new();

    private string CreateTempDir()
    {
        var path = Path.Combine(Path.GetTempPath(), $"fifth-qs-test-{Guid.NewGuid():N}");
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
    // Store.CreateFileStore tests (net10.0 only)
    // ========================================================================

#if NET10_0_OR_GREATER
    [Fact]
    public void CreateFileStore_WithValidPath_ReturnsStore()
    {
        var path = CreateTempDir();
        var store = Store.CreateFileStore(path);
        store.Should().NotBeNull();
    }

    [Fact]
    public void CreateFileStore_WithNullPath_ThrowsArgumentException()
    {
        var act = () => Store.CreateFileStore(null!);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CreateFileStore_WithEmptyPath_ThrowsArgumentException()
    {
        var act = () => Store.CreateFileStore("");
        act.Should().Throw<ArgumentException>();
    }
#endif

    // ========================================================================
    // KG built-in function return type tests
    // ========================================================================

    [Fact]
    public void MemStore_ReturnsStoreBackedByInMemoryManager()
    {
        var store = KG.mem_store();
        store.Should().NotBeNull();
        store.ToVds().Should().BeOfType<InMemoryManager>();
    }

    [Fact]
    public void RemoteStore_ReturnsStoreBackedBySparqlConnector()
    {
        var store = KG.remote_store("http://example.org/sparql");
        store.Should().NotBeNull();
        store.ToVds().Should().BeOfType<SparqlHttpProtocolConnector>();
    }

#if NET10_0_OR_GREATER
    [Fact]
    public void LocalStore_ReturnsStoreBackedByQuadStoreProvider()
    {
        var path = CreateTempDir();
        var store = KG.local_store(path);
        store.Should().NotBeNull();
        store.ToVds().Should().BeOfType<TripleStore.Core.QuadStoreStorageProvider>();
    }
#endif

    // ========================================================================
    // sparql_store deprecation tests
    // ========================================================================

    [Fact]
    public void SparqlStore_DelegatesToRemoteStore()
    {
        // sparql_store should return a Store wrapping SparqlHttpProtocolConnector,
        // same as remote_store
#pragma warning disable CS0618 // Obsolete
        var store = KG.sparql_store("http://example.org/sparql");
#pragma warning restore CS0618
        store.Should().NotBeNull();
        store.ToVds().Should().BeOfType<SparqlHttpProtocolConnector>();
    }

    [Fact]
    public void SparqlStore_HasObsoleteAttribute()
    {
        var method = typeof(KG).GetMethod(
            nameof(KG.sparql_store),
            BindingFlags.Public | BindingFlags.Static,
            null,
            new[] { typeof(string) },
            null);

        method.Should().NotBeNull("sparql_store method should exist on KG");

        var obsoleteAttr = method!.GetCustomAttribute<ObsoleteAttribute>();
        obsoleteAttr.Should().NotBeNull("sparql_store should be marked [Obsolete]");
        obsoleteAttr!.Message.Should().Contain("remote_store");
    }

    // ========================================================================
    // CreateStore() tests
    // ========================================================================

#if NET10_0_OR_GREATER
    [Fact]
    public void CreateStore_ReturnsQuadStoreBackedStore()
    {
        var store = KG.CreateStore();
        store.Should().NotBeNull();
        store.ToVds().Should().BeOfType<TripleStore.Core.QuadStoreStorageProvider>();
    }
#endif

    // ========================================================================
    // Query execution against QuadStore-backed stores (net10.0 only)
    // Validates: Requirements 7.1, 7.2, 7.3, 7.4
    // ========================================================================

#if NET10_0_OR_GREATER
    /// <summary>
    /// Helper: creates a QuadStore-backed store with a graph containing known triples.
    /// Returns the store and the graph URI used.
    /// </summary>
    private (Store store, Uri graphUri) CreateStoreWithTestData()
    {
        var path = CreateTempDir();
        var store = Store.CreateFileStore(path);

        var graphUri = new Uri("http://example.org/test-graph");
        var graph = store.CreateGraph(graphUri);
        var vdsGraph = graph.ToVds();

        var subj = vdsGraph.CreateUriNode(new Uri("http://example.org/alice"));
        var pred = vdsGraph.CreateUriNode(new Uri("http://example.org/name"));
        var obj = vdsGraph.CreateLiteralNode("Alice");
        vdsGraph.Assert(new VDS.RDF.Triple(subj, pred, obj));

        var pred2 = vdsGraph.CreateUriNode(new Uri("http://example.org/age"));
        var obj2 = vdsGraph.CreateLiteralNode("30", new Uri("http://www.w3.org/2001/XMLSchema#integer"));
        vdsGraph.Assert(new VDS.RDF.Triple(subj, pred2, obj2));

        store.SaveGraph(Graph.FromVds(vdsGraph));
        return (store, graphUri);
    }

    [Fact]
    public void SelectQuery_AgainstQuadStore_ReturnsTabularResult()
    {
        // Validates: Requirement 7.1, 7.2
        var (store, _) = CreateStoreWithTestData();

        var query = Query.Parse("SELECT ?s ?p ?o WHERE { ?s ?p ?o }");
        var result = QueryApplicationExecutor.Execute(query, store);

        result.Should().BeOfType<Result.TabularResult>();
        var tabular = (Result.TabularResult)result;
        tabular.ResultSet.Should().NotBeNull();
        tabular.ResultSet.Count.Should().BeGreaterOrEqualTo(2,
            "the store contains at least 2 triples");
    }

    [Fact]
    public void AskQuery_WithMatch_AgainstQuadStore_ReturnsBooleanResultTrue()
    {
        // Validates: Requirement 7.3
        var (store, _) = CreateStoreWithTestData();

        var query = Query.Parse(
            "ASK WHERE { <http://example.org/alice> <http://example.org/name> ?name }");
        var result = QueryApplicationExecutor.Execute(query, store);

        result.Should().BeOfType<Result.BooleanResult>();
        var boolean = (Result.BooleanResult)result;
        boolean.Value.Should().BeTrue("alice has a name triple in the store");
    }

    [Fact]
    public void AskQuery_WithNoMatch_AgainstQuadStore_ReturnsBooleanResultFalse()
    {
        // Validates: Requirement 7.3
        var (store, _) = CreateStoreWithTestData();

        var query = Query.Parse(
            "ASK WHERE { <http://example.org/nonexistent> <http://example.org/name> ?name }");
        var result = QueryApplicationExecutor.Execute(query, store);

        result.Should().BeOfType<Result.BooleanResult>();
        var boolean = (Result.BooleanResult)result;
        boolean.Value.Should().BeFalse("no triple matches the nonexistent subject");
    }

    [Fact]
    public void ConstructQuery_AgainstQuadStore_ReturnsGraphResult()
    {
        // Validates: Requirement 7.4
        var (store, _) = CreateStoreWithTestData();

        var query = Query.Parse(
            "CONSTRUCT { ?s <http://example.org/knows> ?s } WHERE { ?s <http://example.org/name> ?name }");
        var result = QueryApplicationExecutor.Execute(query, store);

        result.Should().BeOfType<Result.GraphResult>();
        var graphResult = (Result.GraphResult)result;
        graphResult.GraphStore.Should().NotBeNull();
    }

    // ========================================================================
    // Task 7.1: Graph interoperability and TriG literal support (net10.0)
    // Validates: Requirements 6.2, 6.3, 6.4, 6.6, 8.1, 8.2, 8.3, 8.4
    // ========================================================================

    [Fact]
    public void CreateGraph_OnQuadStoreBackedStore_ReturnsValidGraphInstance()
    {
        // Validates: Requirement 8.1
        var path = CreateTempDir();
        var store = Store.CreateFileStore(path);

        var graph = store.CreateGraph(new Uri("http://example.org/new-graph"));
        graph.Should().NotBeNull();
        graph.Count.Should().Be(0, "newly created graph should be empty");
        graph.BaseUri.Should().Be(new Uri("http://example.org/new-graph"));
    }

    [Fact]
    public void CreateGraph_NoUri_OnQuadStoreBackedStore_ReturnsValidGraphInstance()
    {
        // Validates: Requirement 8.1
        var path = CreateTempDir();
        var store = Store.CreateFileStore(path);

        var graph = store.CreateGraph();
        graph.Should().NotBeNull();
        graph.Count.Should().Be(0, "newly created graph should be empty");
    }

    [Fact]
    public void GraphPlusTriple_WithGraphFromQuadStore_ProducesCorrectResult()
    {
        // Validates: Requirement 8.4
        // Graph binary operators work on Graph wrapper objects directly,
        // independent of the store backend
        var path = CreateTempDir();
        var store = Store.CreateFileStore(path);

        var graphUri = new Uri("http://example.org/op-test");
        var graph = store.CreateGraph(graphUri);

        var factory = new VDS.RDF.NodeFactory();
        var subj = factory.CreateUriNode(new Uri("http://example.org/s1"));
        var pred = factory.CreateUriNode(new Uri("http://example.org/p1"));
        var obj = factory.CreateLiteralNode("value1");
        var triple = Triple.Create(subj, pred, obj);

        // graph + triple should return a new graph with the triple added
        var result = graph + triple;
        result.Should().NotBeNull();
        result.Count.Should().Be(1);
        // Original graph should be unchanged (non-mutating)
        graph.Count.Should().Be(0);
    }

    [Fact]
    public void GraphPlusGraph_WithGraphsFromQuadStore_ProducesCorrectResult()
    {
        // Validates: Requirement 8.4
        var path = CreateTempDir();
        var store = Store.CreateFileStore(path);

        var graph1 = store.CreateGraph(new Uri("http://example.org/g1"));
        var graph2 = store.CreateGraph(new Uri("http://example.org/g2"));

        var factory = new VDS.RDF.NodeFactory();
        var subj1 = factory.CreateUriNode(new Uri("http://example.org/s1"));
        var pred1 = factory.CreateUriNode(new Uri("http://example.org/p1"));
        var obj1 = factory.CreateLiteralNode("v1");
        graph1.Add(Triple.Create(subj1, pred1, obj1));

        var subj2 = factory.CreateUriNode(new Uri("http://example.org/s2"));
        var pred2 = factory.CreateUriNode(new Uri("http://example.org/p2"));
        var obj2 = factory.CreateLiteralNode("v2");
        graph2.Add(Triple.Create(subj2, pred2, obj2));

        // graph + graph should return a new graph with triples from both
        var result = graph1 + graph2;
        result.Should().NotBeNull();
        result.Count.Should().Be(2);
        // Originals should be unchanged
        graph1.Count.Should().Be(1);
        graph2.Count.Should().Be(1);
    }

    [Fact]
    public void GraphMinusTriple_WithGraphFromQuadStore_ProducesCorrectResult()
    {
        // Validates: Requirement 8.4
        // Graph - Triple operator works on Graph wrapper objects
        var path = CreateTempDir();
        var store = Store.CreateFileStore(path);

        var graph = store.CreateGraph(new Uri("http://example.org/sub-test"));

        var factory = new VDS.RDF.NodeFactory();
        var subj = factory.CreateUriNode(new Uri("http://example.org/s1"));
        var pred = factory.CreateUriNode(new Uri("http://example.org/p1"));
        var obj = factory.CreateLiteralNode("value1");
        var triple = Triple.Create(subj, pred, obj);

        graph.Add(triple);
        graph.Count.Should().Be(1);

        // graph - triple should return a new graph without the triple
        var result = graph - triple;
        result.Should().NotBeNull();
        result.Count.Should().Be(0);
        // Original should be unchanged
        graph.Count.Should().Be(1);
    }

    // ========================================================================
    // Task 7.3: Graph save/load round-trip on QuadStore-backed stores
    // Validates: Requirements 8.2, 8.3
    // ========================================================================

    [Fact]
    public void GraphSaveLoadRoundTrip_OnQuadStoreBackedStore_PreservesTriples()
    {
        // Validates: Requirements 8.2, 8.3
        // Create a QuadStore-backed store, add known triples to a graph,
        // save it, load it back by URI, and assert the triples match.
        var path = CreateTempDir();
        var store = Store.CreateFileStore(path);

        var graphUri = new Uri("http://example.org/roundtrip-graph");
        var graph = store.CreateGraph(graphUri);
        var vdsGraph = graph.ToVds();

        // Add three known triples
        var alice = vdsGraph.CreateUriNode(new Uri("http://example.org/alice"));
        var namePred = vdsGraph.CreateUriNode(new Uri("http://example.org/name"));
        var aliceName = vdsGraph.CreateLiteralNode("Alice");
        vdsGraph.Assert(new VDS.RDF.Triple(alice, namePred, aliceName));

        var agePred = vdsGraph.CreateUriNode(new Uri("http://example.org/age"));
        var aliceAge = vdsGraph.CreateLiteralNode("30", new Uri("http://www.w3.org/2001/XMLSchema#integer"));
        vdsGraph.Assert(new VDS.RDF.Triple(alice, agePred, aliceAge));

        var knowsPred = vdsGraph.CreateUriNode(new Uri("http://example.org/knows"));
        var bob = vdsGraph.CreateUriNode(new Uri("http://example.org/bob"));
        vdsGraph.Assert(new VDS.RDF.Triple(alice, knowsPred, bob));

        // Save the graph to the store
        store.SaveGraph(Graph.FromVds(vdsGraph));

        // Load it back by URI
        var loaded = store.LoadGraph(graphUri);

        // Assert the loaded graph contains exactly the same triples
        loaded.Should().NotBeNull();
        loaded.Count.Should().Be(3, "the graph was saved with 3 triples");

        var loadedVds = loaded.ToVds();
        var loadedTriples = loadedVds.Triples.ToList();

        // Verify each triple is present
        loadedTriples.Should().Contain(t =>
            t.Subject.ToString() == "http://example.org/alice" &&
            t.Predicate.ToString() == "http://example.org/name" &&
            t.Object.ToString() == "Alice");

        loadedTriples.Should().Contain(t =>
            t.Subject.ToString() == "http://example.org/alice" &&
            t.Predicate.ToString() == "http://example.org/age");

        loadedTriples.Should().Contain(t =>
            t.Subject.ToString() == "http://example.org/alice" &&
            t.Predicate.ToString() == "http://example.org/knows" &&
            t.Object.ToString() == "http://example.org/bob");
    }

    // ========================================================================
    // Known limitations: DeleteGraph, RemoveGraphInPlace, Store - operator
    // throw NotImplementedException on QuadStore-backed stores.
    // Validates: Requirement 6.3, 6.4, 6.6
    //
    // KNOWN LIMITATION: QuadStoreStorageProvider.DeleteGraph() and triple
    // retraction operations are not yet implemented in QuadStore. Any Fifth
    // language feature that invokes these operations (e.g., store -= graph,
    // Store.DeleteGraph(), Store.RemoveGraphInPlace()) will throw
    // NotImplementedException. This is an accepted limitation for the initial
    // integration and will be addressed in a future QuadStore release.
    // ========================================================================

    [Fact]
    public void DeleteGraph_OnQuadStoreBackedStore_ThrowsNotImplementedException()
    {
        // Validates: Requirement 6.6
        // Known limitation: QuadStore does not yet support DeleteGraph
        var path = CreateTempDir();
        var store = Store.CreateFileStore(path);

        var graphUri = new Uri("http://example.org/to-delete");
        var graph = store.CreateGraph(graphUri);
        store.SaveGraph(graph);

        var act = () => store.DeleteGraph(graphUri);
        act.Should().Throw<NotImplementedException>(
            "QuadStore does not yet implement DeleteGraph");
    }

    [Fact]
    public void RemoveGraphInPlace_OnQuadStoreBackedStore_ThrowsNotImplementedException()
    {
        // Validates: Requirement 6.4, 6.6
        // Known limitation: RemoveGraphInPlace delegates to DeleteGraph,
        // which is not implemented on QuadStore
        var path = CreateTempDir();
        var store = Store.CreateFileStore(path);

        var graphUri = new Uri("http://example.org/to-remove");
        var graph = store.CreateGraph(graphUri);
        store.SaveGraph(graph);

        var act = () => store.RemoveGraphInPlace(graph);
        act.Should().Throw<NotImplementedException>(
            "QuadStore does not yet implement DeleteGraph, so RemoveGraphInPlace fails");
    }

    [Fact]
    public void StoreMinusOperator_OnQuadStoreBackedStore_ThrowsException()
    {
        // Validates: Requirement 6.3, 6.6
        // Known limitation: Store - operator calls CloneForImmutableOperations
        // which throws InvalidOperationException because _tripleStore is null
        // for QuadStore-backed stores (immutable store operators are only
        // supported for in-memory stores)
        var path = CreateTempDir();
        var store = Store.CreateFileStore(path);

        var graphUri = new Uri("http://example.org/to-subtract");
        var graph = store.CreateGraph(graphUri);

        var act = () => { var _ = store - graph; };
        act.Should().Throw<InvalidOperationException>(
            "Immutable store operators are only supported for in-memory stores");
    }

    [Fact]
    public void StorePlusOperator_OnQuadStoreBackedStore_ThrowsInvalidOperationException()
    {
        // Validates: Requirement 6.3
        // Known limitation: Store + operator calls CloneForImmutableOperations
        // which throws InvalidOperationException because _tripleStore is null
        // for QuadStore-backed stores
        var path = CreateTempDir();
        var store = Store.CreateFileStore(path);

        var graph = store.CreateGraph(new Uri("http://example.org/to-add"));

        var act = () => { var _ = store + graph; };
        act.Should().Throw<InvalidOperationException>(
            "Immutable store operators are only supported for in-memory stores");
    }
#endif

    // ========================================================================
    // Task 7.1: LoadFromTriG verification (all targets)
    // Store.LoadFromTriG uses the in-memory path and works on all targets.
    // Validates: Requirement 6.5
    // ========================================================================

    [Fact]
    public void LoadFromTriG_ProducesFunctionalStore()
    {
        // Validates: Requirement 6.5
        // Store.LoadFromTriG uses the in-memory path and should continue to work
        var trigContent = @"
@prefix ex: <http://example.org/> .

ex:graph1 {
    ex:alice ex:name ""Alice"" .
    ex:alice ex:age ""30"" .
}

ex:graph2 {
    ex:bob ex:name ""Bob"" .
}
";
        var store = Store.LoadFromTriG(trigContent);
        store.Should().NotBeNull();

        // Verify we can load graphs from the TriG-loaded store
        var graph1 = store.LoadGraph(new Uri("http://example.org/graph1"));
        graph1.Should().NotBeNull();
        graph1.Count.Should().Be(2, "graph1 has two triples");

        var graph2 = store.LoadGraph(new Uri("http://example.org/graph2"));
        graph2.Should().NotBeNull();
        graph2.Count.Should().Be(1, "graph2 has one triple");
    }

    // ========================================================================
    // CreateInMemory regression test (all targets)
    // ========================================================================

    [Fact]
    public void CreateInMemory_ReturnsInMemoryManagerBackedStore()
    {
        var store = Store.CreateInMemory();
        store.Should().NotBeNull();
        store.ToVds().Should().BeOfType<InMemoryManager>();
    }
}
