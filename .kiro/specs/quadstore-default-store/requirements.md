# Requirements Document

## Introduction

This feature replaces the current dotNetRDF-based store backend in the Fifth language runtime with the `aabs/QuadStore` package as the new default implementation of the `store` type. The KG setup commands defined in `Fifth.System.KG` will be updated to provide distinct store creation functions covering the valid permutations of locality and persistence. The single `sparql_store` function will be retired in favour of more descriptive functions. This is a breaking change for existing Fifth programs that use `sparql_store`.

## Glossary

- **Store**: The Fifth language wrapper class (`Fifth.System.Store`) that provides RDF dataset storage and retrieval operations to Fifth programs.
- **QuadStore**: The `QuadStore.Core` and `QuadStore.SparqlServer` NuGet packages that provide a quad-based RDF storage implementation and SPARQL server capabilities, to replace dotNetRDF's `InMemoryManager` and `IStorageProvider` as the default backend.
- **KG**: The `Fifth.System.KG` static class containing built-in functions (marked with `[BuiltinFunction]`) for knowledge graph operations in Fifth programs.
- **Graph**: The Fifth language wrapper class (`Fifth.System.Graph`) representing an RDF graph containing triples.
- **Triple**: The Fifth language wrapper class (`Fifth.System.Triple`) representing a single RDF triple (subject, predicate, object).
- **TriG_Literal**: A Fifth language literal syntax (`@<...>`) that embeds TriG-format RDF content inline in source code, lowered to `Store.LoadFromTriG()` calls by the compiler.
- **SPARQL_Endpoint**: A remote HTTP-based SPARQL Protocol endpoint that accepts SPARQL queries and updates.
- **Store_Path**: A file system path string identifying the location of a persistent QuadStore data directory or file.
- **remote_store**: A built-in function that creates a Store connected to a remote SPARQL endpoint via the SPARQL Protocol. Replaces the old `sparql_store` for remote endpoints.
- **local_store**: A built-in function that creates a Store backed by a persistent local QuadStore at a given file system path, accessed directly in-process.
- **mem_store**: A built-in function that creates a transient in-memory Store using dotNetRDF's `InMemoryManager`. Data does not persist across program executions.
- **Query_Application**: The Fifth language operator (`<-`) that executes a SPARQL query against a store, producing a Result discriminated union.

## Requirements

### Requirement 1: QuadStore Package Integration

**User Story:** As a Fifth language maintainer, I want the `QuadStore.Core` and `QuadStore.SparqlServer` packages added as dependencies of the `Fifth.System` project, so that the runtime can use QuadStore as its RDF storage backend.

#### Acceptance Criteria

1. THE Fifth.System project SHALL include NuGet package references to `QuadStore.Core` and `QuadStore.SparqlServer`.
2. WHEN the Fifth.System project is built, THE Build_System SHALL resolve and restore both QuadStore packages without errors.
3. WHEN a Fifth program or library is compiled, THE output SHALL include `QuadStore.Core` and `QuadStore.SparqlServer` as transitive dependencies so that QuadStore-backed stores function at runtime.
4. THE Fifth SDK project file (used by `5thproj` to build Fifth programs and libraries) SHALL propagate the QuadStore package references to compiled output, ensuring they are available without the end user needing to manually add package references.

### Requirement 2: Default Store Creation via QuadStore

**User Story:** As a Fifth developer, I want `KG.CreateStore()` to return a QuadStore-backed store by default, so that the standard store creation path uses the QuadStore engine for persistent storage.

#### Acceptance Criteria

1. WHEN `KG.CreateStore()` is called, THE KG SHALL return a Store instance backed by QuadStore.
2. THE Store SHALL support all existing operations (SaveGraph, LoadGraph, DeleteGraph) when backed by QuadStore.
3. WHEN a graph is saved to a QuadStore-backed store and then loaded by URI, THE Store SHALL return a graph containing the same triples that were saved.
4. WHEN `Store.CreateInMemory()` is called, THE Store SHALL continue to create an in-memory store using dotNetRDF's `InMemoryManager` (QuadStore is not an in-memory option).

### Requirement 3: File-Backed Persistent Store Creation

**User Story:** As a Fifth developer, I want to create a store backed by a file system path, so that my RDF data persists across program executions.

#### Acceptance Criteria

1. WHEN a valid Store_Path is provided to a new `Store.CreateFileStore(string path)` method, THE Store SHALL create a persistent QuadStore-backed store at the specified file system location.
2. WHEN a Store_Path points to an existing QuadStore data location, THE Store SHALL open and load the existing data from that location.
3. WHEN a Store_Path points to a non-existent location, THE Store SHALL create a new empty store and initialize the data location.
4. IF a Store_Path is null or empty, THEN THE Store SHALL throw an `ArgumentException` with a descriptive message.
5. IF a Store_Path points to a location where the process lacks read or write permissions, THEN THE Store SHALL throw an `IOException` with a descriptive message.

### Requirement 4: Store Creation Functions for Each Permutation

**User Story:** As a Fifth developer, I want distinct built-in functions for creating stores based on locality and persistence, so that the function name clearly communicates the kind of store being created.

#### Acceptance Criteria

1. THE KG class SHALL provide a `[BuiltinFunction]` named `remote_store(string endpointUri)` that creates a Store connected to a remote SPARQL endpoint via the SPARQL Protocol.
2. THE KG class SHALL provide a `[BuiltinFunction]` named `local_store(string path)` that creates a Store backed by a persistent local QuadStore at the specified file system path, accessed directly in-process via `QuadStoreStorageProvider`.
3. THE KG class SHALL provide a `[BuiltinFunction]` named `mem_store()` that creates a transient in-memory Store using dotNetRDF's `InMemoryManager`.
4. THE existing `sparql_store` function SHALL be marked as `[Obsolete]` and SHALL delegate to `remote_store` for backward compatibility during the transition period.
5. WHEN `local_store(path)` is called with a path pointing to an existing QuadStore data location, THE function SHALL open and load the existing data.
6. WHEN `local_store(path)` is called with a path pointing to a non-existent location, THE function SHALL create a new empty QuadStore at that location.
7. IF `local_store(path)` is called with a null or empty path, THEN THE function SHALL throw an `ArgumentException`.

### Requirement 5: Store Declaration Grammar Generalization

**User Story:** As a Fifth developer, I want the grammar to support store declarations that call any function returning a Store, so that new store creation functions can be added in the runtime library without requiring grammar changes.

#### Acceptance Criteria

1. THE `colon_store_decl` grammar rule SHALL be generalized to accept a function call expression on the right-hand side of the assignment, rather than being hardcoded to specific keywords.
2. THE FifthParser SHALL accept `name : store = <function_call>;` where `<function_call>` is any valid function call expression (e.g., `remote_store(...)`, `local_store(...)`, `mem_store()`, or any future store creation function).
3. THE AstBuilderVisitor SHALL produce a `VarDeclStatement` with a `StoreDecl` annotation for any store declaration, regardless of which function is called.
4. THE FifthParser SHALL continue to accept `sparql_store(<iri>)` declarations during the deprecation period.
5. NO new lexer keywords SHALL be added for `remote_store`, `local_store`, or `mem_store` — these are regular function identifiers resolved at the runtime level via `Fifth.System.KG`.

### Requirement 6: QuadStore Integration via dotNetRDF-Compatible Storage Provider

**User Story:** As a Fifth language maintainer, I want QuadStore to integrate with the existing `Store` class through its `QuadStoreStorageProvider` (which implements dotNetRDF's storage interfaces), so that no changes to the `Store` wrapper API are needed.

#### Acceptance Criteria

1. WHEN a store is created via `local_store(path)`, THE function SHALL instantiate a `QuadStoreStorageProvider` and pass it to the existing `Store` constructor, requiring no changes to the `Store` class public API.
2. THE `QuadStoreStorageProvider` SHALL be compatible with dotNetRDF's `IStorageProvider` interface, allowing the existing `Store` methods (SaveGraph, LoadGraph, DeleteGraph, operators) to work without modification.
3. THE Store immutable operators (`+` and `-`) SHALL function correctly with QuadStore-backed stores via the `QuadStoreStorageProvider`. NOTE: The `-` operator relies on graph deletion/retraction, which is not yet fully supported in QuadStore — it is expected to throw a `NotImplementedException` when invoked against a QuadStore-backed store.
4. THE Store mutating helpers (`AddGraphInPlace`, `RemoveGraphInPlace`) SHALL function correctly with QuadStore-backed stores via the `QuadStoreStorageProvider`. NOTE: `RemoveGraphInPlace` relies on graph deletion, which is not yet fully supported in QuadStore — it is expected to throw a `NotImplementedException` when invoked against a QuadStore-backed store.
5. WHEN `Store.LoadFromTriG(string trigContent)` is called, THE Store SHALL continue to parse TriG content into a functional store (using the existing in-memory path or QuadStore as appropriate).
6. KNOWN LIMITATION: `QuadStoreStorageProvider.DeleteGraph()` and triple retraction operations are not yet implemented in QuadStore. Any Fifth language feature that invokes these operations (e.g., `store -= graph`, `Store.DeleteGraph()`, `Store.RemoveGraphInPlace()`) SHALL throw a `NotImplementedException`. This is an accepted limitation for the initial integration and will be addressed in a future QuadStore release.

### Requirement 7: Query Execution Compatibility

**User Story:** As a Fifth developer, I want SPARQL query execution via the `<-` operator to work with QuadStore-backed stores, so that my existing query application code continues to function.

#### Acceptance Criteria

1. WHEN a Query_Application expression is evaluated against a QuadStore-backed store, THE QueryApplicationExecutor SHALL execute the SPARQL query and return a Result.
2. WHEN a SELECT query is applied to a QuadStore-backed store, THE QueryApplicationExecutor SHALL return a `Result.TabularResult` containing the matching variable bindings.
3. WHEN an ASK query is applied to a QuadStore-backed store, THE QueryApplicationExecutor SHALL return a `Result.BooleanResult` with the correct truth value.
4. WHEN a CONSTRUCT query is applied to a QuadStore-backed store, THE QueryApplicationExecutor SHALL return a `Result.GraphResult` containing the constructed graph.

### Requirement 8: Graph Interoperability with QuadStore

**User Story:** As a Fifth developer, I want the `Graph` class to work seamlessly with QuadStore-backed stores, so that graph creation, triple manipulation, and store operations remain consistent.

#### Acceptance Criteria

1. WHEN a Graph is created via `Store.CreateGraph()` on a QuadStore-backed store, THE Store SHALL return a valid Graph instance.
2. WHEN a Graph with triples is saved to a QuadStore-backed store, THE Store SHALL persist all triples from the graph.
3. WHEN a Graph is loaded from a QuadStore-backed store by URI, THE Graph SHALL contain all triples that were previously saved under that URI.
4. THE Graph binary operators (`+` and `-` for Triple and Graph operands) SHALL produce correct results when the graphs originate from QuadStore-backed stores.

### Requirement 9: Deprecation of sparql_store and Migration Path

**User Story:** As a Fifth developer with existing programs, I want a clear migration path from `sparql_store` to the new store creation functions, so that I can update my programs with minimal effort.

#### Acceptance Criteria

1. THE `sparql_store` function SHALL be marked as `[Obsolete]` with a message directing users to `remote_store`, `local_store`, or `mem_store`.
2. THE FifthParser SHALL continue to accept `name : store = sparql_store(<iri>);` declarations during the deprecation period, with the compiler emitting a deprecation warning.
3. WHEN `sparql_store(endpointUri)` is called during the deprecation period, THE KG SHALL delegate to `remote_store(endpointUri)` and return a Store connected to the specified SPARQL_Endpoint.
4. THE Store class SHALL maintain its existing public API surface (CreateInMemory, CreateSparqlStore, SaveGraph, LoadGraph, DeleteGraph, LoadFromTriG, operator+, operator-).
5. WHEN TriG_Literal syntax is used in existing programs, THE compiler lowering to `Store.LoadFromTriG()` SHALL continue to produce a functional store.

### Requirement 10: Default Store Declarations with New Functions

**User Story:** As a Fifth developer, I want to declare a default store using any of the new store creation functions, so that I can set the appropriate default store for my program.

#### Acceptance Criteria

1. THE FifthParser SHALL accept `store default = local_store("/data/store");` as a valid default store declaration that creates a local QuadStore-backed default store.
2. THE FifthParser SHALL accept `store default = remote_store(<iri>);` as a valid default store declaration connecting to a remote SPARQL endpoint.
3. THE FifthParser SHALL accept `store default = mem_store();` as a valid default store declaration creating a transient in-memory store.
4. WHEN a default store is declared with any store creation function, THE compiler SHALL treat the resulting store as the default store for query application and graph operations in the program.
5. THE default store declaration SHALL use the same generalized grammar rule as regular store declarations — no special-casing for specific function names.
