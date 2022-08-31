namespace Fifth;

using System.Collections.Generic;
using Antlr4.Runtime;
using AST;
using LangProcessingPhases;
using Parser.LangProcessingPhases;
using TypeSystem;

public static class FifthParserManager
{
    public static bool TryParse<T>(string source, out T ast, out List<FifthCompilationError> errors)
        where T : IAstNode
    {
        TypeRegistry.DefaultRegistry.LoadPrimitiveTypes();
        InbuiltOperatorRegistry.DefaultRegistry.LoadBuiltinOperators();
        ast = (T)ParseAndAnnotate<T>(CharStreams.fromString(source));
        errors = new List<FifthCompilationError>();
        return true;
    }

    public static bool TryParseFile<T>(string fileName, out T ast, out List<FifthCompilationError> errors)
        where T : IAstNode
    {
        TypeRegistry.DefaultRegistry.LoadPrimitiveTypes();
        InbuiltOperatorRegistry.DefaultRegistry.LoadBuiltinOperators();
        ast = (T)ParseAndAnnotate<T>(CharStreams.fromPath(fileName));
        errors = new List<FifthCompilationError>();
        return true;
    }

    #region Core Drivers

    public static IAstNode ParseAndAnnotate<T>(ICharStream source)
        where T : IAstNode
    {
        var ast = ParseToAst<T>(source);

        if (ast == null)
        {
            throw new Fifth.CompilationException("Unable to parse source to AST");
        }

        ast.Accept(new BuiltinInjectorVisitor());
        ast.Accept(new VerticalLinkageVisitor());
        ast.Accept(new CompoundVariableSplitterVisitor());
        ast.Accept(new OverloadGatheringVisitor());
        ast.Accept(new OverloadTransformingVisitor());
        ast.Accept(new VerticalLinkageVisitor());
        ast.Accept(new SymbolTableBuilderVisitor());
        ast.Accept(new TypeAnnotatorVisitor());
        var (visitor, ctx) = DestructuringPatternFlattenerVisitor.CreateVisitor();
        ast = visitor.Process(ast as AstNode, ctx);
        //ast.Accept(new StringifyVisitor(Console.Out));
        return ast;
    }

    public static IAstNode ParseToAst<T>(ICharStream source)
        where T : IAstNode
    {
        if (typeof(T) == typeof(Assembly))
        {
            var ast = ParseProgramToAst(source);
            // embed the program in an assembly to generate code for
            var (name, publicKey, ver) = GetAssemblyDetails();
            ast = new Assembly(name, publicKey, ver)
            {
                Program = (FifthProgram)ast
            };
            AddCommonAssemblyReferences(ast as Assembly);
            return (T)ast;
        }

        if (typeof(T) == typeof(FifthProgram))
        {
            return (T)ParseProgramToAst(source);
        }

        if (typeof(T) == typeof(ExpressionList))
        {
            return (T)ParseExpressionListToAst(source);
        }

        if (typeof(T) == typeof(Expression))
        {
            return (T)ParseExpressionToAst(source);
        }

        if (typeof(T) == typeof(FunctionDefinition))
        {
            return (T)ParseFunctionDeclToAst(source);
        }

        if (typeof(T) == typeof(Block))
        {
            return (T)ParseBlockToAst(source);
        }

        return null;
    }

    private static void AddCommonAssemblyReferences(AST.Assembly ast)
    {
        if (ast != null)
        {
            // ilasm automatically adds [mscorlib]
            ast.References.Add(new AssemblyRef("System.Runtime", "B0 3F 5F 7F 11 D5 0A 3A", "4:0:0:0"));
            ast.References.Add(new AssemblyRef("System.Console", "B0 3F 5F 7F 11 D5 0A 3A", "4:0:0:0"));
        }
    }

    private static (string, string, string) GetAssemblyDetails()
                => ("fifth", "", "");

    private static FifthParser GetParserFor(ICharStream source)
    {
        var lexer = new FifthLexer(source);
        lexer.RemoveErrorListeners();
        lexer.AddErrorListener(new ThrowingErrorListener<int>());

        var parser = new FifthParser(new CommonTokenStream(lexer));
        parser.RemoveErrorListeners();
        parser.AddErrorListener(new ThrowingErrorListener<IToken>());
        return parser;
    }

    #endregion Core Drivers

    #region Parsing into AST Node

    public static IAstNode ParseBlockToAst(ICharStream source)
    {
        var parseTree = ParseBlock(source);
        var visitor = new AstBuilderVisitor();
        var statementList = (StatementList)visitor.Visit(parseTree);
        return new Block(statementList);
    }

    public static IAstNode ParseExpressionListToAst(ICharStream source)
    {
        var parseTree = ParseExpressionList(source);
        var visitor = new AstBuilderVisitor();
        return visitor.Visit(parseTree);
    }

    public static IAstNode ParseExpressionToAst(ICharStream source)
    {
        var parseTree = ParseExpression(source);
        var visitor = new AstBuilderVisitor();
        return visitor.Visit(parseTree);
    }

    public static IAstNode ParseFunctionDeclToAst(ICharStream source)
    {
        var parseTree = ParseFunctionDecl(source);
        var visitor = new AstBuilderVisitor();
        return visitor.Visit(parseTree);
    }

    public static IAstNode ParseProgramToAst(ICharStream source)
    {
        var parseTree = ParseProgram(source);
        var visitor = new AstBuilderVisitor();
        var ast = visitor.Visit(parseTree);
        return ast;
    }

    #endregion Parsing into AST Node

    #region Parsing into Parse Tree

    public static ParserRuleContext ParseBlock(ICharStream source)
        => GetParserFor(source).block();

    public static ParserRuleContext ParseExpression(ICharStream source)
        => GetParserFor(source).exp();

    public static ParserRuleContext ParseExpressionList(ICharStream source)
        => GetParserFor(source).explist();

    public static ParserRuleContext ParseFunctionDecl(ICharStream source)
        => GetParserFor(source).function_declaration();

    public static FifthParser.FifthContext ParseProgram(ICharStream source)
                        => GetParserFor(source).fifth();

    #endregion Parsing into Parse Tree
}
