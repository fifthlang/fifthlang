using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Storage;

namespace Fifth.System;

/// <summary>
/// Executes SPARQL queries against RDF stores and produces Result discriminated union.
/// </summary>
public static class QueryApplicationExecutor
{
    /// <summary>
    /// Execute a SPARQL query against a store.
    /// </summary>
    /// <param name="query">The SPARQL query to execute</param>
    /// <param name="store">The RDF store to query</param>
    /// <param name="cancellationToken">Optional cancellation token for long-running queries</param>
    /// <returns>Result discriminated union (TabularResult | GraphResult | BooleanResult)</returns>
    /// <exception cref="QueryExecutionException">Thrown when query execution fails</exception>
    public static Result Execute(Query query, Store store, CancellationToken? cancellationToken = null)
    {
        try
        {
            // Get the underlying dotNetRDF objects
            var sparqlQuery = query.UnderlyingQuery;
            
            // Get the TripleStore for in-memory querying
            var tripleStore = store.GetTripleStore();
            object results;

            if (tripleStore != null)
            {
                // In-memory path: use Leviathan query processor directly
                var processor = new LeviathanQueryProcessor(tripleStore);
                results = processor.ProcessQuery(sparqlQuery);
            }
            else if (store.ToVds() is IQueryableStorage queryableStorage)
            {
                // IQueryableStorage path: QuadStore and other providers that support direct querying
                results = queryableStorage.Query(sparqlQuery.ToString());
            }
            else
            {
                throw new NotSupportedException(
                    "Query execution requires either an in-memory store or a storage provider that implements IQueryableStorage.");
            }

            // Check for cancellation
            cancellationToken?.ThrowIfCancellationRequested();

            // Map results to the appropriate Result discriminated union variant
            return MapQueryResults(results);
        }
        catch (RdfParseException ex)
        {
            throw new QueryExecutionException(QueryErrorFactory.FromParseException(ex));
        }
        catch (RdfQueryTimeoutException ex)
        {
            throw new QueryExecutionException(QueryErrorFactory.FromTimeoutException(ex));
        }
        catch (RdfQueryException ex)
        {
            throw new QueryExecutionException(QueryErrorFactory.FromQueryException(ex));
        }
        catch (OperationCanceledException ex)
        {
            throw new QueryExecutionException(QueryErrorFactory.FromCancellation(ex));
        }
        catch (OutOfMemoryException ex)
        {
            throw new QueryExecutionException(QueryErrorFactory.FromMemoryException(ex));
        }
        catch (QueryExecutionException)
        {
            throw; // Re-throw our own exceptions
        }
        catch (Exception ex)
        {
            throw new QueryExecutionException(QueryErrorFactory.FromGenericException(ex));
        }
    }

    /// <summary>
    /// Maps raw query results (from Leviathan or IQueryableStorage) to the Result discriminated union.
    /// </summary>
    private static Result MapQueryResults(object results)
    {
        if (results is SparqlResultSet rs)
        {
            if (rs.ResultsType == SparqlResultsType.Boolean)
            {
                return new Result.BooleanResult(rs.Result);
            }
            else // VariableBindings
            {
                return new Result.TabularResult(rs);
            }
        }
        else if (results is IGraph graph)
        {
            return new Result.GraphResult(StoreFromGraph(graph));
        }
        else
        {
            throw new InvalidOperationException($"Unexpected query result type: {results?.GetType().Name ?? "null"}");
        }
    }

    /// <summary>
    /// Helper to create a Store from an IGraph result (for CONSTRUCT/DESCRIBE queries).
    /// </summary>
    private static Store StoreFromGraph(IGraph graph)
    {
        var tripleStore = new VDS.RDF.TripleStore();
        tripleStore.Add(graph);
        var storage = new InMemoryManager(tripleStore);
        return Store.FromVds(storage);
    }
}

/// <summary>
/// Factory for creating QueryError instances from various exception types.
/// </summary>
public static class QueryErrorFactory
{
    public static QueryError FromParseException(RdfParseException ex)
    {
        return new QueryError
        {
            Kind = ErrorKind.SyntaxError,
            Message = $"SPARQL syntax error at line {ex.StartLine}, column {ex.StartPosition}: {ex.Message}",
            SourceSpan = new SourceSpan(ex.StartLine, ex.StartPosition, 1),
            UnderlyingExceptionType = nameof(RdfParseException),
            Suggestion = "Check SPARQL syntax near the indicated position"
        };
    }

    public static QueryError FromQueryException(RdfQueryException ex)
    {
        var kind = ex.Message.Contains("variable", StringComparison.OrdinalIgnoreCase)
            ? ErrorKind.ValidationError
            : ErrorKind.ExecutionError;

        var suggestion = kind == ErrorKind.ValidationError
            ? "Ensure all variables in SELECT clause are bound in WHERE clause"
            : "Review query logic and verify function arguments match expected types";

        return new QueryError
        {
            Kind = kind,
            Message = ex.Message,
            UnderlyingExceptionType = nameof(RdfQueryException),
            Suggestion = suggestion
        };
    }

    public static QueryError FromTimeoutException(RdfQueryTimeoutException ex)
    {
        return new QueryError
        {
            Kind = ErrorKind.Timeout,
            Message = $"Query execution exceeded timeout limit: {ex.Message}",
            UnderlyingExceptionType = nameof(RdfQueryTimeoutException),
            Suggestion = "Simplify graph pattern, add more specific constraints, or increase timeout limit"
        };
    }

    public static QueryError FromCancellation(OperationCanceledException ex)
    {
        return new QueryError
        {
            Kind = ErrorKind.Cancellation,
            Message = "Query execution cancelled by user request",
            UnderlyingExceptionType = nameof(OperationCanceledException),
            Suggestion = null
        };
    }

    public static QueryError FromMemoryException(OutOfMemoryException ex)
    {
        return new QueryError
        {
            Kind = ErrorKind.ResourceLimit,
            Message = "Query result exceeded available memory",
            UnderlyingExceptionType = nameof(OutOfMemoryException),
            Suggestion = "Add LIMIT clause or enable result streaming"
        };
    }

    public static QueryError FromGenericException(Exception ex)
    {
        return new QueryError
        {
            Kind = ErrorKind.ExecutionError,
            Message = $"Query execution failed: {ex.Message}",
            UnderlyingExceptionType = ex.GetType().Name,
            Suggestion = "Review query logic and data constraints"
        };
    }

    public static QueryError SecurityWarning(string message, string suggestion)
    {
        return new QueryError
        {
            Kind = ErrorKind.SecurityWarning,
            Message = message,
            Suggestion = suggestion
        };
    }
}
