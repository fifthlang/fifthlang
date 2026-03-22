# Implementation Plan: QuadStore Default Store

## Overview

Replace the default RDF storage backend with `aabs/QuadStore`, add three new store creation built-in functions (`remote_store`, `local_store`, `mem_store`), generalize the parser grammar for store declarations, deprecate `sparql_store`, and update query execution to handle QuadStore-backed stores. Implementation language: C#.

## Tasks

- [ ] 1. Add QuadStore NuGet package references
  - [ ] 1.1 Add `QuadStore.Core` and `QuadStore.SparqlServer` PackageReferences to `src/fifthlang.system/Fifth.System.csproj` alongside the existing `dotNetRdf` reference
    - Add `<PackageReference Include="QuadStore.Core" Version="*" />` and `<PackageReference Include="QuadStore.SparqlServer" Version="*" />`
    - Run `dotnet restore fifthlang.sln` to verify packages resolve without errors
    - _Requirements: 1.1, 1.2_
  - [ ] 1.2 Ensure QuadStore packages propagate as transitive dependencies via the Fifth SDK
    - Update `src/Fifth.Sdk/Sdk/Sdk.props` or `Sdk.targets` if needed so compiled Fifth programs resolve QuadStore assemblies at runtime
    - _Requirements: 1.3, 1.4_

- [ ] 2. Implement `Store.CreateFileStore` factory method and new KG built-in functions
  - [ ] 2.1 Add `Store.CreateFileStore(string path)` to `src/fifthlang.system/Store.cs`
    - Instantiate `QuadStoreStorageProvider(path)` and wrap in `Store` via the private constructor
    - Validate null/empty path throws `ArgumentException`
    - `_tripleStore` remains null for file-backed stores
    - _Requirements: 3.1, 3.2, 3.3, 3.4, 3.5, 6.1_
  - [ ] 2.2 Add `remote_store`, `local_store`, `mem_store` built-in functions to `src/fifthlang.system/KnowledgeGraphs.cs`
    - `remote_store(string endpointUri)` → `Store.CreateSparqlStore(endpointUri)`
    - `local_store(string path)` → `Store.CreateFileStore(path)`
    - `mem_store()` → `Store.CreateInMemory()`
    - All marked with `[BuiltinFunction]`
    - _Requirements: 4.1, 4.2, 4.3_
  - [ ] 2.3 Deprecate `sparql_store` and update `CreateStore()`
    - Mark existing `sparql_store(string)` with `[Obsolete("Use remote_store, local_store, or mem_store instead")]` and delegate to `remote_store`
    - Update `KG.CreateStore()` to return a QuadStore-backed store using a temp directory path
    - _Requirements: 2.1, 4.4, 9.1, 9.3_
  - [ ]* 2.4 Write unit tests for `Store.CreateFileStore` and new KG built-in functions
    - Test `CreateFileStore` with valid path, null path, empty path
    - Test `remote_store`, `local_store`, `mem_store` return correct store types
    - Test `sparql_store` delegates to `remote_store` and has `[Obsolete]` attribute (reflection)
    - Test `CreateStore()` returns QuadStore-backed store
    - Test `Store.CreateInMemory()` still returns InMemoryManager-backed store (regression)
    - Add tests to `test/runtime-integration-tests/` in a new `QuadStore_Integration_Tests.cs`
    - _Requirements: 2.2, 2.3, 2.4, 3.4, 4.7, 9.4_
  - [ ]* 2.5 Write property test: Save/Load round-trip on QuadStore-backed stores (Property 1)
    - **Property 1: Save/Load round-trip on QuadStore-backed stores**
    - Generate random RDF graphs with random triples, save to QuadStore-backed store, load back, assert triple set equality
    - Use FsCheck with `MaxTest = 100`
    - **Validates: Requirements 2.2, 2.3, 8.2, 8.3**
  - [ ]* 2.6 Write property test: Persistence round-trip across store instances (Property 2)
    - **Property 2: Persistence round-trip across store instances**
    - Generate random graphs, save to a temp-path QuadStore, close, reopen at same path, load, assert triple set equality
    - Use FsCheck with `MaxTest = 100`
    - **Validates: Requirements 3.1, 3.2, 4.5**

- [ ] 3. Checkpoint - Ensure all runtime tests pass
  - Ensure all tests pass, ask the user if questions arise.

- [ ] 4. Generalize parser grammar for store declarations
  - [ ] 4.1 Update `colon_store_decl` rule in `src/parser/grammar/FifthParser.g4`
    - Add `store_creation_expr` sub-rule with two alternatives: `store_sparql` (backward compat for `SPARQL` keyword token) and `store_func_call` (any `IDENTIFIER(expressionList?)`)
    - Replace the hardcoded `SPARQL L_PAREN iri R_PAREN` in `colon_store_decl` with `store_creation_expr`
    - No changes to `FifthLexer.g4` — no new keywords
    - _Requirements: 5.1, 5.2, 5.4, 5.5_
  - [ ] 4.2 Update `AstBuilderVisitor.VisitColon_store_decl` in `src/parser/AstBuilderVisitor.cs`
    - Dispatch on `store_creation_expr` alternative: `store_sparql` → emit `KG.sparql_store(uri)` call with deprecation warning diagnostic; `store_func_call` → emit `KG.<func_name>(args)` call
    - Both produce `VarDeclStatement` with `Kind = "StoreDecl"` annotation
    - Update the store collection logic in `VisitFifth` to handle the new grammar shape
    - _Requirements: 5.3, 9.2_
  - [ ] 4.3 Emit deprecation warning diagnostic `STORE_DEPRECATED_001` for `sparql_store` usage
    - Add diagnostic emission in `AstBuilderVisitor` when `store_sparql` alternative is matched
    - Warning level, code `STORE_DEPRECATED_001`, message directing to `remote_store`, `local_store`, or `mem_store`
    - _Requirements: 9.2_
  - [ ]* 4.4 Write parser tests for new store declaration forms
    - Extend `test/syntax-parser-tests/ValidDeclaration_SyntaxTests.cs` with tests for:
      - `name : store = remote_store(<http://example.com/>);`
      - `name : store = local_store("/data/store");`
      - `name : store = mem_store();`
      - `store default = local_store("/data/store");`
      - `store default = remote_store(<http://example.com/>);`
      - `store default = mem_store();`
      - `name : store = sparql_store(<http://example.com/>);` (regression)
    - _Requirements: 5.2, 5.4, 10.1, 10.2, 10.3, 10.5_
  - [ ]* 4.5 Write property test: Parser produces correct AST for all store creation functions (Property 3)
    - **Property 3: Parser produces correct AST for all store creation functions**
    - Generate store declarations for each function variant, parse, assert AST structure matches expected `VarDeclStatement` with `StoreDecl` annotation
    - Use FsCheck with `MaxTest = 100`
    - **Validates: Requirements 5.3**

- [ ] 5. Checkpoint - Ensure parser and runtime tests pass
  - Ensure all tests pass, ask the user if questions arise.

- [ ] 6. Update query execution for QuadStore-backed stores
  - [ ] 6.1 Update `QueryApplicationExecutor.Execute` in `src/fifthlang.system/QueryApplicationExecutor.cs`
    - When `store.GetTripleStore()` returns null, check if `store.ToVds()` implements `IQueryableStorage` and use `IQueryableStorage.Query()` directly
    - If not `IQueryableStorage`, fall back to HTTP-based query via `QuadStore.SparqlServer` local endpoint
    - Map results to `Result.TabularResult`, `Result.BooleanResult`, or `Result.GraphResult` as appropriate
    - _Requirements: 7.1, 7.2, 7.3, 7.4_
  - [ ]* 6.2 Write unit tests for query execution against QuadStore-backed stores
    - Test SELECT, ASK, CONSTRUCT queries against a QuadStore-backed store with loaded data
    - Verify correct Result discriminated union variant for each query form
    - Add to `test/runtime-integration-tests/QuadStore_Integration_Tests.cs`
    - _Requirements: 7.1, 7.2, 7.3, 7.4_
  - [ ]* 6.3 Write property test: Query result type matches query form (Property 4)
    - **Property 4: Query result type matches query form on QuadStore-backed stores**
    - Generate random triples, load into QuadStore store, execute SELECT/ASK/CONSTRUCT queries, assert result type matches query form
    - Use FsCheck with `MaxTest = 100`
    - **Validates: Requirements 7.1, 7.2, 7.3, 7.4**

- [ ] 7. Verify graph interoperability and TriG literal support
  - [ ] 7.1 Verify `Store.LoadFromTriG` continues to work and graph operators function with QuadStore-backed stores
    - Ensure `Graph` binary operators (`+`, `-` for Triple/Graph operands) produce correct results with graphs from QuadStore stores
    - Verify `Store.CreateGraph()` on QuadStore-backed stores returns valid Graph instances
    - Document known limitations: `DeleteGraph`, `RemoveGraphInPlace`, `-` operator throw `NotImplementedException` on QuadStore-backed stores
    - _Requirements: 6.2, 6.3, 6.4, 6.5, 6.6, 8.1, 8.2, 8.3, 8.4_
  - [ ]* 7.2 Write property test: TriG literal loading produces functional store (Property 5)
    - **Property 5: TriG literal loading produces functional store**
    - Generate random TriG content with random named graphs and triples, load via `Store.LoadFromTriG`, assert all graphs and triples are recoverable
    - Use FsCheck with `MaxTest = 100`
    - **Validates: Requirements 6.5, 9.5**
  - [ ]* 7.3 Write integration tests for graph interoperability with QuadStore
    - Test graph save/load round-trip on QuadStore-backed stores
    - Test `DeleteGraph` throws `NotImplementedException` on QuadStore-backed stores
    - Test `RemoveGraphInPlace` throws `NotImplementedException` on QuadStore-backed stores
    - Test immutable operators (`+`, `-`) behavior on QuadStore-backed stores
    - _Requirements: 6.3, 6.4, 6.6, 8.1, 8.2, 8.3, 8.4_

- [ ] 8. Update documentation
  - [ ] 8.1 Update `docs/Getting-Started/knowledge-graphs.md` with new store creation functions
    - Document `remote_store`, `local_store`, `mem_store` usage and examples
    - Add deprecation notice for `sparql_store`
    - Update store declaration examples to show generalized grammar
    - _Requirements: 9.1, 10.4_

- [ ] 9. Final checkpoint - Ensure all tests pass
  - Ensure all tests pass, ask the user if questions arise.

## Notes

- Tasks marked with `*` are optional and can be skipped for faster MVP
- Each task references specific requirements for traceability
- Checkpoints ensure incremental validation
- Property tests validate universal correctness properties from the design document using FsCheck
- Unit tests use xUnit + FluentAssertions
- The `SPARQL` lexer keyword token is retained for backward compatibility — no lexer changes needed
- Known limitation: `DeleteGraph` and triple retraction throw `NotImplementedException` on QuadStore-backed stores (accepted per Requirement 6.6)
