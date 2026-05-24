---
description: csharp-foundations-and-tooling
inclusion: always
---
## Development
- CFT-01 [MANDATORY]: Set `TargetFramework` to `net10.0` in every production `.csproj` unless a documented compatibility constraint requires a different target.
- CFT-02 [MANDATORY]: Enable nullable reference types in every C# project by setting `<Nullable>enable</Nullable>` in the project file.
- CFT-03 [MANDATORY]: Set `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>` and allow suppression only for explicitly documented warning IDs.
- CFT-04 [MANDATORY]: Enable .NET analyzers in all projects and define analyzer severity centrally in a source-controlled root `.editorconfig`.
- CFT-05 [MANDATORY]: Define repository-wide C# style in a root `.editorconfig` and enforce it in CI (for example, `dotnet format --verify-no-changes`).
- CFT-06 [MANDATORY]: Apply consistent naming conventions through `.editorconfig` naming rules and use intention-revealing names for types, members, parameters, and locals.
- CFT-07 [MANDATORY]: Express nullability intent explicitly in API signatures using nullable annotations (for example, `string?`) and related nullability attributes where needed.
- CFT-08 [MANDATORY]: Do not suppress nullable warnings (for example, with `!` or pragma directives) unless the exact reason is documented at the call site.
- CFT-09 [MANDATORY]: Default to the narrowest visibility (`private` or `internal`) and widen to `public` only when a real consumer requires it.
