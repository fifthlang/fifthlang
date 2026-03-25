# Architecture Overview
**Date:** 2026-03-25 | **Repository:** aabs/fifthlang

---

## 1  Solution Map

| Project (`.csproj`) | Directory | Role |
|----------------------|-----------|------|
| `ast_model` | `src/ast-model/` | Core AST node hierarchy, type system, metamodel |
| `ast_generated` | `src/ast-generated/` | Auto-generated builder/visitor/rewriter infrastructure |
| `ast_generator` | `src/ast_generator/` | CLI code-generator reading metamodel → generated files |
| `parser` | `src/parser/` | ANTLR 4.8 grammar + `AstBuilderVisitor` (parse tree → AST) |
| `compiler` | `src/compiler/` | 33-phase transformation pipeline + Roslyn emission |
| `Fifth.System` | `src/fifthlang.system/` | Built-in runtime functions, KG/SPARQL store types |
| `Fifth.Sdk` | `src/Fifth.Sdk/` | Packaging SDK surface |
| `Fifth.LanguageServer` | `src/language-server/` | LSP server (OmniSharp 0.19.9) |
| `ast_tests` | `test/ast-tests/` | TUnit tests for AST builders/visitors |
| `runtime-integration-tests` | `test/runtime-integration-tests/` | End-to-end compiler integration tests |
| `syntax-parser-tests` | `test/syntax-parser-tests/` | Grammar/parser acceptance tests |
| `fifth-runtime-tests` | `test/fifth-runtime-tests/` | Runtime behaviour tests |
| `fifth-sdk-tests` | `test/fifth-sdk-tests/` | SDK packaging tests |
| `kg-smoke-tests` | `test/kg-smoke-tests/` | Knowledge-graph smoke tests |
| `LanguageServerSmoke` | `test/language-server-smoke/` | LSP smoke tests |
| `perf` | `test/perf/` | Performance tests |
| `validate-examples` | `src/tools/validate-examples/` | CI parser-check tool |

---

## 2  Compiler Pipeline Stages

### Stage 1 — Lexical Analysis & Parsing
- **Entry:** Source file path(s) from `CompilerOptions`
- **Tools:** ANTLR 4.13.1 runtime (`Antlr4.Runtime.Standard 4.13.1`); grammar tool jar is `antlr-4.8-complete.jar` (see CONFORMANCE item V-5)
- **Lexer:** `src/parser/grammar/FifthLexer.g4` — tokenises `.5th` source
- **Parser:** `src/parser/grammar/FifthParser.g4` — builds ANTLR parse tree
- **AST Builder:** `src/parser/AstBuilderVisitor.cs` (2 192 lines) — transforms parse tree into high-level `AstThing` hierarchy
- **Output:** `AssemblyDef` root (high-level AST)

### Stage 2 — Language Analysis & Transformation (33 phases)
- **Orchestrator:** `src/compiler/ParserManager.cs` → `FifthParserManager.ApplyLanguageAnalysisPhases()`
- **Controlled by:** `AnalysisPhase` enum with 33 named milestones (0–33)
- **All passes in order:**

| # | Phase Name | Class | Category |
|---|-----------|-------|----------|
| 1 | TreeLink | `TreeLinkageVisitor` | Structural |
| 2 | Builtins | `BuiltinInjectorVisitor` | Injection |
| 3 | ClassCtors | `ClassCtorInserter` | Injection |
| 4 | ConstructorValidation | `ConstructorValidator` | Validation |
| 5 | SymbolTableInitial | `SymbolTableBuilderVisitor` | Symbol |
| 6 | ConstructorResolution | `ConstructorResolver` | Semantic |
| 7 | DefiniteAssignment | `DefiniteAssignmentAnalyzer` | Validation |
| 8 | BaseConstructorValidation | `BaseConstructorValidator` | Validation |
| 9 | PropertyToField | `PropertyToFieldExpander` | Lowering |
| 10 | DestructurePatternFlatten | `DestructuringVisitor` | Lowering |
| 11 | OverloadGroup | `OverloadGatheringVisitor` | Structural |
| 12 | GuardValidation | Guard validators | Validation |
| 13 | OverloadTransform | `OverloadTransformingVisitor` | Lowering |
| 14 | DestructuringLowering | `DestructuringLoweringRewriter` | Lowering |
| 15 | UnaryOperatorLowering | `UnaryOperatorLoweringRewriter` | Lowering |
| 16 | SparqlLiteralLowering | `SparqlLiteralLoweringRewriter` | KG/Lowering |
| 17 | TriGLiteralLowering | `TriGLiteralLoweringRewriter` | KG/Lowering |
| 18 | AugmentedAssignmentLowering | `AugmentedAssignmentLoweringRewriter` | Lowering |
| 19 | TreeRelink | `TreeLinkageVisitor` (2nd) | Structural |
| 20 | TripleDiagnostics | `TripleDiagnosticsVisitor` | KG/Validation |
| 21 | TripleExpansion | `TripleExpansionVisitor` | KG/Lowering |
| 22 | GraphTripleOperatorLowering | `TripleGraphAdditionLoweringRewriter` | KG/Lowering |
| 23 | SymbolTableFinal | `SymbolTableBuilderVisitor` (2nd) | Symbol |
| 24 | VarRefResolver | `VarRefResolverVisitor` | Symbol |
| 25 | TypeAnnotation | `TypeAnnotationVisitor` | Type |
| 26 | QueryApplicationTypeCheck | `QueryApplicationTypeCheckRewriter` | KG/Type |
| 27 | QueryApplicationLowering | `QueryApplicationLoweringRewriter` | KG/Lowering |
| 28 | ListComprehensionLowering | `ListComprehensionLoweringRewriter` | Lowering |
| 29 | ListComprehensionValidation | `SparqlComprehensionValidationVisitor` | Validation |
| 30 | LambdaValidation | `LambdaValidationVisitor` | Validation |
| 31 | LambdaClosureConversion | `LambdaClosureConversionRewriter` | Lowering |
| 32 | Defunctionalisation | `DefunctionalisationRewriter` | Lowering |
| 33 | TailCallOptimization | `TailCallOptimizationRewriter` | Optimisation |

> **Note:** `TreeLinkageVisitor`, `SymbolTableBuilderVisitor`, and `VarRefResolverVisitor` are invoked multiple times (see AI Slop Register §3 and Risk Register).

### Stage 3 — Roslyn Emission
- **Entry:** Lowered `AssemblyDef` (post-33-phase AST)
- **Translator:** `src/compiler/LoweredToRoslyn/LoweredAstToRoslynTranslator.cs` (2 712 lines)
- **Interface:** `IBackendTranslator` with `Translate(AssemblyDef)`
- **Options:** `TranslatorOptions` (debug info, additional references)
- **Registry:** `TranslatorRegistry.Current` (static, mutable — see Risk R3)
- **Output:** `TranslationResult` containing C# `SyntaxTree`s and a `MappingTable`
- **PE emission:** Roslyn `CSharpCompilation.Emit()` → native PE/PDB

### Stage 4 — Runtime Support
- **Config generation:** `Compiler.GenerateRuntimeConfigAsync()` writes `.runtimeconfig.json`
- **Dependency copy:** `CopyRuntimeDependenciesAsync()` copies `.dll` files to output dir
- **Execution:** `RunPhase()` uses `IProcessRunner` to invoke the compiled executable

---

## 3  Subsystem Responsibilities

### `ast-model`
- **Responsibility:** Defines all node types in the high-level AST hierarchy (`AstThing` → `ScopedDefinition` → `AssemblyDef`/`ModuleDef`/`FunctionDef`/etc.)
- **Types:** ~105 `record` / `abstract record` declarations in `AstMetamodel.cs`
- **Source of Truth:** `AstMetamodel.cs` — do not hand-edit generated output
- **Key interfaces:** `IAstThing`, `IScope`, `IAnnotated`, `IOverloadableFunction`
- **Type system:** `TypeSystem/` subdirectory with `FifthType`, `TypeRegistry`, `TypeInference`, `Maybe<T>`, `FunctionSignature`
- **No IL metamodel:** `ILMetamodel.cs` referenced in constitution does not exist; the IL pipeline was removed during Roslyn migration

### `ast-generated`
- **Responsibility:** Auto-generated structural code: builders (`builders.generated.cs`, 2 787 lines), visitors (`visitors.generated.cs`, 1 189 lines), rewriters (`rewriter.generated.cs`, 1 280 lines), type inference support (`typeinference.generated.cs`, 301 lines)
- **No IL builders:** `il.builders.generated.cs` and `il.rewriter.generated.cs` mentioned in constitution do not exist
- **Extension points:** `BaseAstVisitor`, `DefaultRecursiveDescentVisitor`, `DefaultAstRewriter`

### `parser`
- **Responsibility:** Lexing + parsing + parse-tree → AST transformation
- **Key file:** `AstBuilderVisitor.cs` (2 192 lines) — the largest non-generated source file
- **Grammar:** Split `FifthLexer.g4` + `FifthParser.g4`; ANTLR 4.13.1 runtime
- **Key issue:** Contains a `TODO: need ways to define this` at line 701 regarding `WithPublicKeyToken`, and `TODO: Support fully nested types` at line 1545
- **Dependencies:** `ast-model`, `ast-generated`

### `compiler`
- **Responsibility:** Pipeline orchestration, 33 transformation passes, Roslyn emission, CLI entry
- **Key files:** `Compiler.cs` (967), `ParserManager.cs` (613), `LoweredAstToRoslynTranslator.cs` (2 712)
- **Subdirectories:** `LanguageTransformations/` (44 files), `LoweredToRoslyn/`, `SemanticAnalysis/`, `NamespaceResolution/`, `TypeSystem/`, `Validation/`
- **Extension points:** `IBackendTranslator`, `TranslatorRegistry`
- **Diagnostics:** `compiler.Diagnostic` record — but see duplication issue

### `fifthlang.system`
- **Responsibility:** Built-in functions, SPARQL store integration, knowledge graph types
- **Key issue:** `Store.cs:159` contains `throw new NotImplementedException("SPARQL query execution needs integration with Fifth type system")`

### `language-server`
- **Responsibility:** LSP server providing hover, completion, diagnostics for Fifth files in VS Code
- **Key dependency:** OmniSharp.Extensions.LanguageServer 0.19.9 (may be outdated)
- **Dependencies:** `compiler`, `parser`

---

## 4  Configuration & Feature Flags

| Mechanism | Location | Purpose |
|-----------|----------|---------|
| `FIFTH_DEBUG` env var | `src/compiler/DebugLog.cs` | Enables verbose diagnostic logging to stderr |
| `AnalysisPhase upTo` param | `FifthParserManager.ApplyLanguageAnalysisPhases` | Partial pipeline execution (used in tests) |
| `CompilerOptions.Diagnostics` | `src/compiler/CompilerOptions.cs` | Enables timing diagnostics |
| `TranslatorOptions.EmitDebugInfo` | `LoweredAstToRoslynTranslator.cs:18` | Enables PDB/debug info emission |
| `TranslatorOptions.AdditionalReferences` | `LoweredAstToRoslynTranslator.cs:20` | Additional assembly references in output |
| `CompilerOptions.TargetFramework` | `CompilerOptions.cs` | Target TFM for runtime config |

---

## 5  Test Strategy Summary

| Test Project | Framework | Focus |
|-------------|-----------|-------|
| `ast-tests` | TUnit + FluentAssertions | AST builder/visitor/rewriter correctness |
| `syntax-parser-tests` | TUnit | Grammar acceptance |
| `runtime-integration-tests` | TUnit | End-to-end `.5th` → PE compilation + execution |
| `fifth-runtime-tests` | TUnit | Runtime behaviour |
| `kg-smoke-tests` | TUnit | SPARQL/TriG knowledge graph end-to-end |
| `language-server-smoke` | TUnit | LSP protocol smoke tests |
| `perf` | TUnit | Performance regression |

CI runs on Ubuntu, macOS, Windows via GitHub Actions matrix (`ci.yml`). A dedicated parser-check step runs `src/tools/validate-examples/` to validate all `.5th` samples parse with the current grammar.
