using Antlr4.Runtime;
using static FifthParser;

namespace fifth.Parser.Tests
{
    public class ParserTestBase
    {

        protected static FifthContext ParseProgram(string program)
        {
            FifthLexer lexer = new FifthLexer(new AntlrInputStream(program));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new ThrowingErrorListener<int>());

            FifthParser parser = new FifthParser(new CommonTokenStream(lexer));
            parser.RemoveErrorListeners();
            parser.AddErrorListener(new ThrowingErrorListener<IToken>());

            return parser.fifth();
        }
    }
}