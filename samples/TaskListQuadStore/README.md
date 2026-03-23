# TaskListQuadStore Sample

A Fifth sample demonstrating persistent RDF storage using a local QuadStore.

## What it demonstrates

- `local_store()` for persistent file-backed QuadStore
- `store += graph` to persist graphs into the store
- Triple literals (`<s, p, o>`) for creating new entities
- Namespace aliases for concise prefixed IRIs

## Prerequisites

- .NET 10.0 SDK (QuadStore requires net10.0+)

## Build & Run

From the repository root:

```bash
# 1. Build the compiler and solution (QuadStore is unconditionally available on net10.0)
dotnet build fifthlang.sln

# 2. Pack the local SDK
dotnet pack src/Fifth.Sdk/Fifth.Sdk.csproj -c Debug --no-build -o dist/packages

# 3. Build and run the sample (all dependencies are copied automatically by the SDK)
dotnet run --project samples/TaskListQuadStore/src/TaskApp/TaskApp.5thproj
```

## Expected Output

```
=== TaskListQuadStore Demo ===

Adding tasks to persistent QuadStore...
Saved task to QuadStore:
Design data model
Saved task to QuadStore:
Implement repository
Saved task to QuadStore:
Write tests

All tasks persisted to ./quadstore_data
```

## Known Limitations

- Store declarations must be function-local (module-level store variables
  are not yet resolved by the compiler's symbol table)
- Variable interpolation in triple subjects/predicates is not yet supported
- SPARQL queries against QuadStore have a known "Unknown dictionary id" bug
- `local_store()` is only available on net10.0+
