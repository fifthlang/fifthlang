# .NET 10.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that an .NET 10.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10.0 upgrade.
3. Upgrade src/ast-model/ast_model.csproj
4. Upgrade src/ast-generated/ast_generated.csproj
5. Upgrade src/fifthlang.system/Fifth.System.csproj
6. Upgrade src/parser/parser.csproj
7. Upgrade src/compiler/compiler.csproj
8. Upgrade src/Fifth.Sdk/Fifth.Sdk.csproj
9. Upgrade test/fifth-runtime-tests/fifth-runtime-tests.csproj
10. Upgrade test/syntax-parser-tests/syntax-parser-tests.csproj
11. Upgrade test/runtime-integration-tests/runtime-integration-tests.csproj
12. Upgrade test/ast-tests/ast_tests.csproj
13. Upgrade src/ast_generator/ast_generator.csproj
14. Run unit tests to validate upgrade in the projects listed below:

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

Table below contains projects that do belong to the dependency graph for selected projects and should not be included in the upgrade.

| Project name                                   | Description                 |
|:-----------------------------------------------|:---------------------------:|

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name               | Current Version | New Version | Description                                      |
|:---------------------------|:---------------:|:-----------:|:-------------------------------------------------|
| System.Reflection.Metadata | 9.0.0           | 10.0.0      | Recommended for .NET 10.0                        |

### Project upgrade details
This section contains details about each project upgrade and modifications that need to be done in the project.

#### src/ast-model/ast_model.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

#### src/ast-generated/ast_generated.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

#### src/fifthlang.system/Fifth.System.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

#### src/parser/parser.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

#### src/compiler/compiler.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

#### src/Fifth.Sdk/Fifth.Sdk.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

#### test/fifth-runtime-tests/fifth-runtime-tests.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

#### test/syntax-parser-tests/syntax-parser-tests.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

#### test/runtime-integration-tests/runtime-integration-tests.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - System.Reflection.Metadata should be updated from `9.0.0` to `10.0.0` (*recommended for .NET 10.0*)

#### test/ast-tests/ast_tests.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

#### src/ast_generator/ast_generator.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`
