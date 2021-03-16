namespace Fifth.Tests
{
    using Antlr4.Runtime;
    using Fifth.AST;
    using Fifth.Parser.LangProcessingPhases;
    using Fifth.Runtime;
    using Fifth.Runtime.LangProcessingPhases;
    using static FifthParser;

    public class ParserTestBase
    {
        #region Core Drivers
        protected static IAstNode ParseToAst<T>(string fragment)
            where T : IAstNode
        {
            if (typeof(T) == typeof(ExpressionList))
            {
                return (T)ParseExpressionListToAst(fragment);
            }
            if (typeof(T) == typeof(Expression))
            {
                return (T)ParseExpressionToAst(fragment);
            }
            if (typeof(T) == typeof(AstFunctionDefinition))
            {
                return (T)ParseFunctionDeclToAst(fragment);
            }

            return null;
        }

        protected static IAstNode ParseAndAnnotate<T>(string fragment)
            where T : IAstNode
        {
            var ast = ParseToAst<T>(fragment);

            if (ast != null)
            {
                ast.Accept(new TypeAnnotatorVisitor());
            }
            return ast;
        }

        protected static ActivationFrame ParseAndGenerate<T>(string expressionString)
            where T : IAstNode
        {
            var astNode = ParseAndAnnotate<T>(expressionString);
            var af = new ActivationFrame();
            ISpecialFormEmitter specialFormEmitter = astNode switch
            {
                Expression e => new ExpressionStackEmitter(e),
                ExpressionList el => new ExpressionListStackEmitter(el),
                AstFunctionDefinition fd => new FunctionDefinitionEmitter(fd),
                _ => throw new System.NotImplementedException(),
            };
            specialFormEmitter.Emit(new StackEmitter(), af);
            return af;
        }

        protected static FifthParser GetParserFor(string fragment)
        {
            var lexer = new FifthLexer(new AntlrInputStream(fragment));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new ThrowingErrorListener<int>());

            var parser = new FifthParser(new CommonTokenStream(lexer));
            parser.RemoveErrorListeners();
            parser.AddErrorListener(new ThrowingErrorListener<IToken>());
            return parser;
        }

        #endregion

        #region Parsing into Parse Tree
        protected static ParserRuleContext ParseIri(string fragment)
                    => GetParserFor(fragment).iri();

        protected static FifthContext ParseProgram(string fragment)
            => GetParserFor(fragment).fifth();
        protected static ParserRuleContext ParseExpression(string fragment)
            => GetParserFor(fragment).exp();

        protected static ParserRuleContext ParseExpressionList(string fragment)
            => GetParserFor(fragment).explist();

        protected static ParserRuleContext ParseFunctionDecl(string fragment)
            => GetParserFor(fragment).function_declaration();
        protected static ParserRuleContext ParseDeclAssignment(string fragment)
            => GetParserFor(fragment).exp();

        #endregion

        #region Parsing into AST Node

        protected static IAstNode ParseDeclAssignmentToAst(string fragment)
        {
            var parseTree = ParseDeclAssignment(fragment);
            var visitor = new AstBuilderVisitor();
            return visitor.Visit(parseTree);
        }


        protected static IAstNode ParseExpressionListToAst(string fragment)
        {
            var parseTree = ParseExpressionList(fragment);
            var visitor = new AstBuilderVisitor();
            return visitor.Visit(parseTree);
        }

        protected static IAstNode ParseFunctionDeclToAst(string fragment)
        {
            var parseTree = ParseFunctionDecl(fragment);
            var visitor = new AstBuilderVisitor();
            return visitor.Visit(parseTree);
        }

        protected static IAstNode ParseExpressionToAst(string fragment)
        {
            var parseTree = ParseExpression(fragment);
            var visitor = new AstBuilderVisitor();
            return visitor.Visit(parseTree);
        }


        protected static IAstNode ParseProgramToAst(string fragment)
        {
            var parseTree = ParseProgram(fragment);
            var visitor = new AstBuilderVisitor();
            return visitor.Visit(parseTree);
        }
        

        #endregion

        #region Parsing and Generation
        protected static ActivationFrame ParseAndGenerateFunctionDecl(string functionString)
            => ParseAndGenerate<AstFunctionDefinition>(functionString);
        protected static ActivationFrame ParseAndGenerateProgram(string functionString)
            => ParseAndGenerate<FifthProgram>(functionString);
        protected static ActivationStack ParseAndGenerateExpression(string expressionString)
            => ParseAndGenerate<Expression>(expressionString).Stack;
        protected static ActivationStack ParseAndGenerateExpressionFragment(string expressionString)
            => ParseAndGenerate<Expression>(expressionString).Stack;
        #endregion
    }
}
