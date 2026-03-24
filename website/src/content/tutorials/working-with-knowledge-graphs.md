---
title: "Working with Knowledge Graphs"
description: "Learn how to use Fifth's built-in knowledge graph types and operations"
readingTime: "15 min"
prerequisites:
  - "learn-fifth-in-y-minutes"
order: 2
---

This tutorial covers the canonical store declaration syntax and graph operations, and how they use the built-in `Fifth.System.KG` helpers.

## Store Creation Functions

Fifth provides three built-in functions for creating stores, each covering a different combination of locality and persistence:

| Function | Description | Persistence | Locality |
|----------|-------------|-------------|----------|
| `remote_store(uri)` | Connect to a remote SPARQL endpoint | Remote-managed | Remote |
| `local_store(path)` | Create a persistent local QuadStore at a file path | Persistent (disk) | Local |
| `mem_store()` | Create a transient in-memory store | Transient (RAM) | Local |

### Store Declaration Syntax

Use the colon form for named stores and the `store default` form for the default store. The right-hand side accepts any store creation function call:

```
name : store = <function_call>;
store default = <function_call>;
```

Examples:

- `db : store = remote_store(<http://example.org/sparql>);`
- `db : store = local_store("/data/my-store");`
- `db : store = mem_store();`
- `store default = local_store("/data/store");`
- `store default = remote_store(<http://example.org/sparql>);`
- `store default = mem_store();`

> **Deprecation Notice:** `sparql_store` is deprecated. Use `remote_store`, `local_store`, or `mem_store` instead. The compiler emits warning `STORE_DEPRECATED_001` when `sparql_store` is used. During the transition period, `sparql_store` continues to work — it delegates to `remote_store` internally.

## Graph Operations

### Creating Graphs

```fifth
main(): int {
    // Create an empty graph
    g: graph = KG.CreateGraph();
    
    // Add triples to the graph
    g += <http://ex/s, http://ex/p, "o">;
    g += <http://ex/s2, http://ex/p2, 42>;
    
    return g.CountTriples();
}
```

### Saving to Stores

```fifth
store default = local_store("/data/store");

main(): int {
    g: graph = KG.CreateGraph();
    g += <http://ex/s, http://ex/p, "o">;
    
    // Save graph to default store
    default += g;
    
    return 0;
}
```


## Triple Literals

Fifth supports concise triple literal syntax for constructing individual RDF triples:

```fifth
// Basic triple literal syntax: <subject, predicate, object>
personType: triple = <ex:Person, rdf:type, rdfs:Class>;
age: triple = <ex:Alice, ex:age, 42>;
```

### Triple Literal Syntax Rules

- **Form**: `<subject, predicate, object>` with exactly three comma-separated components
- **Subject/Predicate**: Must be IRIs (either full `<http://...>` or prefixed `ex:name`)
- **Object**: Can be an IRI, primitive literal (string, number, boolean), or variable reference

### List Expansion

Triple literals support list expansion in the object position:

```fifth
// List in object position expands to multiple triples
labels: [triple] = <ex:Alice, rdfs:label, ["Alice", "Ally"]>; 
// Expands to two triples: <ex:Alice, rdfs:label, "Alice"> and <ex:Alice, rdfs:label, "Ally">

// Empty list produces warning and zero triples
emptyLabels: [triple] = <ex:Alice, ex:nothing, []>; // Warning: TRPL004
```

> **Note**: Nested lists are not allowed and will produce a compile error (TRPL006).

### Triple Operations

Triples compose with graphs using `+` and `-` operators:

```fifth
base: graph = KG.CreateGraph();
base += <ex:Alice, rdf:type, ex:Person>;

// Add a triple to a graph (returns new graph)
extended: graph = base + <ex:Alice, ex:age, 42>;

// Chaining operations
g2: graph = base + personType + age;

// Combine triples into a graph
g3: graph = <ex:s1, ex:p1, ex:o1> + <ex:s2, ex:p2, ex:o2>;

// Remove a triple from a graph
g4: graph = extended - <ex:Alice, ex:age, 42>;
```

### Mutating Assignment Operators

Triple literals support compound assignment operators for graphs:

```fifth
base: graph = KG.CreateGraph();
base += <ex:Alice, rdf:type, ex:Person>;

// Add triple to existing graph
base += <ex:Alice, ex:age, 42>;

// Remove triple from graph
base -= <ex:Alice, ex:age, 42>;
```

### Triple Literals with Graphs

Triple literals can be added to graphs using the += operator:

```fifth
g: graph = KG.CreateGraph();
g += <ex:Alice, rdf:type, ex:Person>;  // Add triple to graph
g += <ex:Alice, ex:age, 42>;
```

### Escaping in Serialization

When triple literals are serialized (e.g., in debugging or logging), special characters are escaped:
- The characters `>` and `,` inside string literal objects are preceded by a backslash
- Exactly one space follows each comma
- Example: `<ex:s, ex:p, "value\, with comma">`

## Literal Support in Object Position

Triple literals accept object literals for:
- Strings, booleans, chars
- Signed/unsigned integers: `sbyte`, `byte`, `short`, `ushort`, `int`, `uint`, `long`, `ulong`
- Floating point: `float`, `double`
- Precise decimals: `decimal`

Literals are lowered to typed RDF literals using the appropriate XSD datatype (e.g., `xsd:int`, `xsd:decimal`).

## Built-in Functions

Graph operations use `Fifth.System.KG` functions:

### Store Creation

- `remote_store(endpointUri)`: Connect to a remote SPARQL endpoint
- `local_store(path)`: Create a persistent local QuadStore at a file path
- `mem_store()`: Create a transient in-memory store
- `sparql_store(endpointUri)`: *(Deprecated)* Delegates to `remote_store`

### Graph Operations

- `CreateGraph()`: Create an empty graph
- `CreateUri(string)`: Create an IRI node
- `CreateLiteral(value)`: Create a literal node
- `CreateTriple(subject, predicate, object)`: Create a triple
- `Assert(graph, triple)`: Add a triple to a graph
- `SaveGraph(store, graph[, uri])`: Save graph to a store

## Raw API Quickstart

You can also use the raw API directly:

```fifth
main(): int {
    KG.SaveGraph(
        KG.remote_store("http://example.org/store"),
        KG.Assert(
            KG.CreateGraph(),
            KG.CreateTriple(
                KG.CreateUri(KG.CreateGraph(), "http://ex/s"),
                KG.CreateUri(KG.CreateGraph(), "http://ex/p"),
                KG.CreateLiteral(KG.CreateGraph(), 1.23m)
            )
        ),
        "http://example.org/graph"
    );
    return 0;
}
```

See tests under `test/runtime-integration-tests/*GraphAssertionBlock*` for more examples.

## Diagnostics

### Triple Literal Diagnostics (TRPL001-TRPL006)

The compiler emits specific diagnostic codes for triple literal errors:

| Code | Severity | Description |
|------|----------|-------------|
| TRPL001 | Error | Triple literal must have exactly three components (subject, predicate, object) |
| TRPL002 | Error | Triple literal subject must be an IRI |
| TRPL003 | Error | Triple literal predicate must be an IRI |
| TRPL004 | Warning | Triple literal with empty list object expands to zero triples |
| TRPL005 | Error | Invalid type in triple literal object position |
| TRPL006 | Error | Nested lists are not allowed in triple literal object position |

### Store Deprecation Diagnostics

| Code | Severity | Description |
|------|----------|-------------|
| STORE_DEPRECATED_001 | Warning | `sparql_store` is deprecated; use `remote_store`, `local_store`, or `mem_store` |
