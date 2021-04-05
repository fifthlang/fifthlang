using System;
using Antlr4.Runtime;
using Fifth.AST;
using Fifth.LangProcessingPhases;
using Fifth.Parser.LangProcessingPhases;
using Fifth.TypeSystem;

public static class FifthParserManager
{
    public static FifthProgram ParseProgram(string fifthProgram)
    {
        TypeRegistry.DefaultRegistry.LoadPrimitiveTypes();
        InbuiltOperatorRegistry.DefaultRegistry.LoadBuiltinOperators();
        var astNode = ParseAndAnnotateProgram(fifthProgram);
        return (FifthProgram) astNode;
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

    private static IAstNode ParseAndAnnotateProgram(string fifthProgram)
    {
        var parseTree = GetParserFor(fifthProgram).fifth();
        var visitor = new AstBuilderVisitor();
        var astNode = visitor.Visit(parseTree);
        astNode.Accept(new VerticalLinkageVisitor());
        astNode.Accept(new DesugaringVisitor());
        astNode.Accept(new SymbolTableBuilderVisitor());
        astNode.Accept(new TypeAnnotatorVisitor());
        return astNode;
    }
}
