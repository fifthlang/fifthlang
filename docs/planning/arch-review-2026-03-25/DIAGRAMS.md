# Diagrams
**Date:** 2026-03-25 | **Repository:** aabs/fifthlang

---

## Diagram 1 — Top-Level Component Architecture

```mermaid
graph TD
    subgraph "Source Input"
        SRC[".5th source files"]
    end

    subgraph "Front-End (src/parser/)"
        LEXER["FifthLexer.g4\n(ANTLR 4.8)"]
        PARSER["FifthParser.g4\n(ANTLR 4.8)"]
        ABV["AstBuilderVisitor\n(Parse Tree → AST)"]
        SRC --> LEXER --> PARSER --> ABV
    end

    subgraph "AST Model (src/ast-model/)"
        AST_MODEL["AstMetamodel.cs\n~105 record types"]
        TYPE_SYS["TypeSystem/\nFifthType, TypeRegistry,\nTypeInference"]
        AST_MODEL --- TYPE_SYS
    end

    subgraph "Generated Infrastructure (src/ast-generated/)"
        BUILDERS["builders.generated.cs\n(Builder pattern)"]
        VISITORS["visitors.generated.cs\n(Visitor pattern)"]
        REWRITERS["rewriter.generated.cs\n(Rewriter pattern)"]
    end

    subgraph "Code Generator (src/ast_generator/)"
        GEN["ast_generator CLI\n(RazorLight templates)"]
        GEN -->|generates| BUILDERS
        GEN -->|generates| VISITORS
        GEN -->|generates| REWRITERS
        GEN -->|reads| AST_MODEL
    end

    ABV --> AST_MODEL

    subgraph "Compiler Pipeline (src/compiler/)"
        PM["ParserManager\nApplyLanguageAnalysisPhases\n(33 sequential phases)"]
        XFORMS["LanguageTransformations/\n44 visitor/rewriter files"]
        ROSLYN_T["LoweredAstToRoslynTranslator\n(Roslyn CSharp codegen)"]
        COMPILE["Compiler.cs\nOrchestrator"]
        PM --> XFORMS
        COMPILE --> PM
        COMPILE --> ROSLYN_T
    end

    ABV -->|AssemblyDef| COMPILE
    XFORMS --> VISITORS
    XFORMS --> REWRITERS

    subgraph "Runtime Support (src/fifthlang.system/)"
        SYS["Fifth.System\nBuiltins, KG, SPARQL Store"]
    end

    subgraph "Language Server (src/language-server/)"
        LSP["Fifth.LanguageServer\n(OmniSharp LSP 0.19.9)"]
    end

    ROSLYN_T -->|C# SyntaxTrees| PE["PE Assembly (.dll/.exe)"]
    COMPILE --> SYS
    LSP --> COMPILE
```

---

## Diagram 2 — Compilation Pipeline Flow

```mermaid
flowchart TD
    A["Source .5th file(s)"] --> B["ANTLR Lexer+Parser"]
    B --> C["AstBuilderVisitor\n(parse tree → AssemblyDef)"]
    C --> D["Phase 1: TreeLinkageVisitor\n(parent-child links)"]
    D --> E["Phase 2-3: Builtins + ClassCtor injection"]
    E --> F["Phase 4-8: Constructor validation\n+ SymbolTable (initial)\n+ DefiniteAssignment"]
    F --> G["Phase 9-10: PropertyToField\n+ DestructuringVisitor"]
    G --> H["Phase 11-13: Overload gathering\n+ guard validation\n+ overload transform"]
    H --> I["Phase 14-18: Lowering passes\n(destructuring, unary ops,\nSPARQL, TriG, augmented assignment)"]
    I --> J["Phase 19: TreeRelink\n(2nd TreeLinkageVisitor)"]
    J --> K["Phase 20-22: KG/Triple passes\n(diagnostics, expansion, graph addition)"]
    K --> L["Phase 23-25: SymbolTable (final)\n+ VarRefResolver\n+ TypeAnnotation"]
    L --> M["Phase 26-27: Query application\ntype-check + lowering"]
    M --> N["Phase 28-29: ListComprehension\nlowering + SPARQL validation"]
    N --> O["Phase 30-33: Lambda validation\n+ closure conversion\n+ defunctionalisation\n+ tail-call optimisation"]
    O --> P["LoweredAstToRoslynTranslator\n(AssemblyDef → C# SyntaxTrees)"]
    P --> Q["Roslyn CSharpCompilation\n(Emit PE + PDB)"]
    Q --> R["PE Assembly\n+ .runtimeconfig.json\n+ runtime deps"]

    style I fill:#ffe0b2
    style K fill:#e1f5fe
    style L fill:#e8f5e9
    style P fill:#f3e5f5
```

---

## Diagram 3 — Subsystem Dependency Graph

```mermaid
graph LR
    AST_MODEL["ast_model"]
    AST_GEN["ast_generated"]
    AST_GENERATOR["ast_generator\n(CLI tool)"]
    PARSER["parser"]
    COMPILER["compiler"]
    SYS["Fifth.System"]
    SDK["Fifth.Sdk"]
    LSP["Fifth.LanguageServer"]

    AST_MODEL --> AST_GEN
    AST_MODEL --> AST_GENERATOR
    AST_MODEL --> PARSER
    AST_GEN --> PARSER
    AST_GEN --> COMPILER
    AST_MODEL --> COMPILER
    PARSER --> COMPILER
    SYS --> COMPILER
    COMPILER --> LSP
    PARSER --> LSP
    COMPILER --> SDK

    subgraph "Test Projects"
        T1["ast-tests"]
        T2["runtime-integration-tests"]
        T3["syntax-parser-tests"]
        T4["fifth-runtime-tests"]
        T5["kg-smoke-tests"]
        T6["language-server-smoke"]
    end

    COMPILER --> T2
    COMPILER --> T4
    COMPILER --> T5
    PARSER --> T3
    AST_GEN --> T1
    LSP --> T6
```

---

## Diagram 4 — Core AST Data Model (Selected Key Types)

```mermaid
classDiagram
    class AstThing {
        +Parent: AstThing?
        +SymbolTable: SymTab?
        +Annotations: Dict
        +Accept(visitor)
    }
    class ScopeAstThing {
        +Symbols: List~Symbol~
    }
    class Definition {
        +Name: string
    }
    class ScopedDefinition {
        +Statements: List~Statement~
    }
    class AssemblyDef {
        +Modules: List~ModuleDef~
        +AssemblyName: string
    }
    class ModuleDef {
        +NamespaceDecl
        +Functions: List~FunctionDef~
        +Classes: List~ClassDef~
    }
    class FunctionDef {
        +ParameterDefinitions: List~ParamDef~
        +ReturnType: FifthType
        +Body: Statement
    }
    class ClassDef {
        +Fields: List~FieldDef~
        +Methods: List~MethodDef~
    }
    class Statement
    class Expression {
        +ExpressionType: FifthType?
    }
    class FuncCallExp {
        +FunctionDef: FunctionDef
        +InvocationArguments: List~Expression~
    }
    class BinaryExp {
        +Op: Operator
        +LHS: Expression
        +RHS: Expression
    }

    AstThing <|-- ScopeAstThing
    AstThing <|-- Definition
    AstThing <|-- Statement
    AstThing <|-- Expression
    ScopeAstThing <|-- ScopedDefinition
    Definition <|-- ScopedDefinition
    ScopedDefinition <|-- AssemblyDef
    ScopedDefinition <|-- ModuleDef
    ScopedDefinition <|-- FunctionDef
    ScopedDefinition <|-- ClassDef
    AssemblyDef "1" *-- "many" ModuleDef
    ModuleDef "1" *-- "many" FunctionDef
    ModuleDef "1" *-- "many" ClassDef
    Expression <|-- FuncCallExp
    Expression <|-- BinaryExp
```

---

## Diagram 5 — CI Pipeline

```mermaid
flowchart LR
    PUSH["git push / PR"] --> CI["ci.yml\nmatrix: ubuntu/macos/windows"]
    CI --> RESTORE["dotnet restore\nfifthlang.sln"]
    RESTORE --> BUILD["dotnet build\n--configuration Release"]
    BUILD --> VALIDATE[".5th parser-check\n(validate-examples tool)"]
    VALIDATE --> TESTS["dotnet test\nfifthlang.sln\n+ coverage collection"]
    TESTS --> REPORT["Coverage report\n(ReportGenerator)\nuploaded as artifact"]
    TESTS --> UPLOAD_TRX["TRX results\nuploaded as artifact"]
```
