using System.Collections.Generic;
using Antlr4.Runtime;
using Fifth.AST;
using Fifth.LangProcessingPhases;
using Fifth.Parser.LangProcessingPhases;
using Fifth.TypeSystem;

/*
 * ParseAndAnnotate -> ParseToAst<T> -> GetParserFor 
 */
public static class FifthParserManager
{

    public static bool TryParse<T>(string fragment, out T ast, out List<FifthCompilationError> errors)
        where T : IAstNode
    {
        TypeRegistry.DefaultRegistry.LoadPrimitiveTypes();
        InbuiltOperatorRegistry.DefaultRegistry.LoadBuiltinOperators();
        ast = (T) ParseAndAnnotate<T>(fragment);
        errors = new List<FifthCompilationError>();
        return true;
    }

    #region Core Drivers


    public static IAstNode ParseAndAnnotate<T>(string fragment)
        where T : IAstNode
    {
        var ast = ParseToAst<T>(fragment);

        if (ast != null)
        {
            ast.Accept(new VerticalLinkageVisitor());
            ast.Accept(new DesugaringVisitor());
            ast.Accept(new SymbolTableBuilderVisitor());
            ast.Accept(new TypeAnnotatorVisitor());
        }

        return ast;
    }
    public static IAstNode ParseToAst<T>(string fragment)
        where T : IAstNode
    {
        if (typeof(T) == typeof(FifthProgram))
        {
            return (T)ParseProgramToAst(fragment);
        }

        if (typeof(T) == typeof(ExpressionList))
        {
            return (T)ParseExpressionListToAst(fragment);
        }

        if (typeof(T) == typeof(Expression))
        {
            return (T)ParseExpressionToAst(fragment);
        }

        if (typeof(T) == typeof(FunctionDefinition))
        {
            return (T)ParseFunctionDeclToAst(fragment);
        }

        if (typeof(T) == typeof(Block))
        {
            return (T)ParseBlockToAst(fragment);
        }

        return null;
    }

    private static FifthParser GetParserFor(string fragment)
    {
        var lexer = new FifthLexer(new AntlrInputStream(fragment));
        lexer.RemoveErrorListeners();
        lexer.AddErrorListener(new ThrowingErrorListener<int>());

        var parser = new FifthParser(new CommonTokenStream(lexer));
        parser.RemoveErrorListeners();
        parser.AddErrorListener(new ThrowingErrorListener<IToken>());
        return parser;
    }

    #endregion Core Drivers

    #region Parsing into AST Node

    public static IAstNode ParseExpressionListToAst(string fragment)
    {
        var parseTree = ParseExpressionList(fragment);
        var visitor = new AstBuilderVisitor();
        return visitor.Visit(parseTree);
    }

    public static IAstNode ParseBlockToAst(string fragment)
    {
        var parseTree = ParseBlock(fragment);
        var visitor = new AstBuilderVisitor();
        var statementList = (StatementList)visitor.Visit(parseTree);
        return new Block(statementList);
    }

    public static IAstNode ParseFunctionDeclToAst(string fragment)
    {
        var parseTree = ParseFunctionDecl(fragment);
        var visitor = new AstBuilderVisitor();
        return visitor.Visit(parseTree);
    }

    public static IAstNode ParseExpressionToAst(string fragment)
    {
        var parseTree = ParseExpression(fragment);
        var visitor = new AstBuilderVisitor();
        return visitor.Visit(parseTree);
    }

    public static IAstNode ParseProgramToAst(string fragment)
    {
        var parseTree = ParseProgram(fragment);
        var visitor = new AstBuilderVisitor();
        var ast = visitor.Visit(parseTree);
        return ast;
    }

    #endregion Parsing into AST Node

    #region Parsing into Parse Tree

    public static FifthParser.FifthContext ParseProgram(string fragment)
        => GetParserFor(fragment).fifth();

    public static ParserRuleContext ParseExpression(string fragment)
        => GetParserFor(fragment).exp();

    public static ParserRuleContext ParseExpressionList(string fragment)
        => GetParserFor(fragment).explist();

    public static ParserRuleContext ParseBlock(string fragment)
        => GetParserFor(fragment).block();

    public static ParserRuleContext ParseFunctionDecl(string fragment)
        => GetParserFor(fragment).function_declaration();

    #endregion Parsing into Parse Tree
}
