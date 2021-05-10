using System.Collections.Generic;
using Antlr4.Runtime;
using Fifth.AST;
using Fifth.LangProcessingPhases;
using Fifth.Parser.LangProcessingPhases;
using Fifth.TypeSystem;

/*
 * ParseAndAnnotate -> ParseToAst<T> -> GetParserFor
 */
namespace Fifth
{
    public static class FifthParserManager
    {

        public static bool TryParseFile<T>(string fileName, out T ast, out List<FifthCompilationError> errors)
            where T : IAstNode
        {
            TypeRegistry.DefaultRegistry.LoadPrimitiveTypes();
            InbuiltOperatorRegistry.DefaultRegistry.LoadBuiltinOperators();
            ast = (T)ParseAndAnnotate<T>(CharStreams.fromPath(fileName));
            errors = new List<FifthCompilationError>();
            return true;

        }

        public static bool TryParse<T>(string source, out T ast, out List<FifthCompilationError> errors)
            where T : IAstNode
        {
            TypeRegistry.DefaultRegistry.LoadPrimitiveTypes();
            InbuiltOperatorRegistry.DefaultRegistry.LoadBuiltinOperators();
            ast = (T)ParseAndAnnotate<T>(CharStreams.fromString(source));
            errors = new List<FifthCompilationError>();
            return true;
        }

        #region Core Drivers


        public static IAstNode ParseAndAnnotate<T>(ICharStream source)
            where T : IAstNode
        {
            var ast = ParseToAst<T>(source);

            if (ast != null)
            {
                ast.Accept(new BuiltinInjectorVisitor());
                ast.Accept(new VerticalLinkageVisitor());
                ast.Accept(new CompoundVariableSplitterVisitor());
                ast.Accept(new OverloadGatheringVisitor());
                ast.Accept(new SymbolTableBuilderVisitor());
                ast.Accept(new TypeAnnotatorVisitor());
            }

            return ast;
        }
        public static IAstNode ParseToAst<T>(ICharStream source)
            where T : IAstNode
        {
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

        public static IAstNode ParseExpressionListToAst(ICharStream source)
        {
            var parseTree = ParseExpressionList(source);
            var visitor = new AstBuilderVisitor();
            return visitor.Visit(parseTree);
        }

        public static IAstNode ParseBlockToAst(ICharStream source)
        {
            var parseTree = ParseBlock(source);
            var visitor = new AstBuilderVisitor();
            var statementList = (StatementList)visitor.Visit(parseTree);
            return new Block(statementList);
        }

        public static IAstNode ParseFunctionDeclToAst(ICharStream source)
        {
            var parseTree = ParseFunctionDecl(source);
            var visitor = new AstBuilderVisitor();
            return visitor.Visit(parseTree);
        }

        public static IAstNode ParseExpressionToAst(ICharStream source)
        {
            var parseTree = ParseExpression(source);
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

        public static FifthParser.FifthContext ParseProgram(ICharStream source)
            => GetParserFor(source).fifth();

        public static ParserRuleContext ParseExpression(ICharStream source)
            => GetParserFor(source).exp();

        public static ParserRuleContext ParseExpressionList(ICharStream source)
            => GetParserFor(source).explist();

        public static ParserRuleContext ParseBlock(ICharStream source)
            => GetParserFor(source).block();

        public static ParserRuleContext ParseFunctionDecl(ICharStream source)
            => GetParserFor(source).function_declaration();

        #endregion Parsing into Parse Tree
    }
}
