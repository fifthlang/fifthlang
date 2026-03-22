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
# 1. Build the compiler and solution
dotnet build fifthlang.sln

# 2. Build Fifth.System for net10.0 (enables local_store / QuadStore)
dotnet build src/fifthlang.system/Fifth.System.csproj -c Debug -p:EnableNet10=true

# 3. Pack the local SDK (needed because the published SDK doesn't support net10.0 yet)
dotnet pack src/Fifth.Sdk/Fifth.Sdk.csproj -c Debug --no-build -o dist/packages

# 4. Build the sample
dotnet build samples/TaskListQuadStore/src/TaskApp/TaskApp.5thproj

# 5. Copy QuadStore runtime dependencies (not yet automated by the SDK)
cp src/compiler/bin/Debug/net10.0/QuadStore.Core.dll samples/TaskListQuadStore/src/TaskApp/bin/Debug/net8.0/
cp src/compiler/bin/Debug/net10.0/Roaring.Net.dll samples/TaskListQuadStore/src/TaskApp/bin/Debug/net8.0/
cp src/fifthlang.system/bin/Debug/net10.0/Fifth.System.dll samples/TaskListQuadStore/src/TaskApp/bin/Debug/net8.0/
# Native roaring bitmap library (adjust path for your OS/arch):
cp ~/.nuget/packages/roaring.net/1.0.0/runtimes/win-x64/native/roaring.dll samples/TaskListQuadStore/src/TaskApp/bin/Debug/net8.0/

# 6. Run
dotnet samples/TaskListQuadStore/src/TaskApp/bin/Debug/net8.0/TaskApp.exe
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

- QuadStore runtime dependencies must be manually copied (step 5 above)
- Store declarations must be function-local (module-level store variables
  are not yet resolved by the compiler's symbol table)
- Variable interpolation in triple subjects/predicates is not yet supported
- SPARQL queries against QuadStore have a known "Unknown dictionary id" bug
- `local_store()` is only available on net10.0+
