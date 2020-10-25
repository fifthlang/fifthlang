namespace Fifth.Tests
{
    using Antlr4.Runtime;
    using Fifth.Parser;
    using static FifthParser;

    public class ParserTestBase
    {
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

        protected static ParserRuleContext ParseDeclAssignment(string fragment)
            => GetParserFor(fragment).statement();

        protected static ParserRuleContext ParseExpression(string fragment)
            => GetParserFor(fragment).exp();

        protected static FifthContext ParseProgram(string fragment)
                                    => GetParserFor(fragment).fifth();
    }
}
