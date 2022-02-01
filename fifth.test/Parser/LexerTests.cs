namespace Fifth.Tests
{
    using Antlr4.Runtime;
    using Fifth.Parser.LangProcessingPhases;
    using NUnit.Framework;

    [TestFixture, Category("Lexer")]
    public class LexerTests : ParserTestBase
    {
        [TestCase("alias", "ALIAS")]
        [TestCase("as", "AS")]
        [TestCase("else", "ELSE")]
        [TestCase("if", "IF")]
        [TestCase("new", "NEW")]
        [TestCase("with", "WITH")]
        [TestCase("return", "RETURN")]
        [TestCase("use", "USE")]
        [TestCase("\"a\"", "STRING")]
        [TestCase("'a'", "STRING")]
        [TestCase("\"\"", "STRING")]
        [TestCase("''", "STRING")]
        [TestCase("a", "IDENTIFIER")]
        [TestCase("abc", "IDENTIFIER")]
        [TestCase("aBc", "IDENTIFIER")]
        [TestCase("_aBc", "IDENTIFIER")]
        [TestCase("5", "INT")]
        [TestCase("-5", "INT")]
        [TestCase("5.0", "FLOAT")]
        [TestCase("5.5e12", "FLOAT")]
        [TestCase("5.5e-12", "FLOAT")]
        [TestCase("5.5E-12", "FLOAT")]
        [TestCase("5.5E12", "FLOAT")]
        [TestCase("50", "INT")]
        [TestCase("-55", "INT")]
        [TestCase("55.0", "FLOAT")]
        [TestCase("55.5e12", "FLOAT")]
        [TestCase("55.5e-12", "FLOAT")]
        [TestCase("55.5E-12", "FLOAT")]
        [TestCase("55.05e12", "FLOAT")]
        public void TestCanRecogniseIntegers(string sample, string expectedType)
        {
            var lexer = new FifthLexer(new AntlrInputStream(sample));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new ThrowingErrorListener<int>());

            var token = lexer.NextToken();
            Assert.That(token.Type, Is.EqualTo(lexer.TokenTypeMap[expectedType]));
            Assert.That(token.Text, Is.EqualTo(sample));
        }
    }
}
