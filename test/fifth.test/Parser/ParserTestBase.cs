using Antlr4.Runtime;
using static FifthParser;

namespace fifth.parser.Parser.Tests
{
    public class ParserTestBase
    {

        protected static FifthContext ParseProgram(string fragment)
            => GetParserFor(fragment).fifth();
        
        protected static ParserRuleContext ParseDeclAssignment(string fragment)
            => GetParserFor(fragment).statement();

        protected static FifthParser GetParserFor(string fragment)
        {
            FifthLexer lexer = new FifthLexer(new AntlrInputStream(fragment));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new ThrowingErrorListener<int>());

            FifthParser parser = new FifthParser(new CommonTokenStream(lexer));
            parser.RemoveErrorListeners();
            parser.AddErrorListener(new ThrowingErrorListener<IToken>());
            return parser;
        }
    }
}