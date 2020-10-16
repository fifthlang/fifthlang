using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using NUnit.Framework;
using static FifthParser;

namespace fifth.parser.Parser.Tests
{
    [TestFixture()]
    public class ParserTests : ParserTestBase
    {
        private static string TestProgram => @"use std;
            main(int x, int y) {myprint(x + y);}
            myprint(int x) {std.print(""the answer is "" + x);}";

        [Test]
        public void TestCanParseFullProgram()
        {
            var ctx = ParseProgram(TestProgram);
            var visitor = new SymbolTableBuilderVisitor();
            ParseTreeWalker.Default.Walk(visitor, ctx);
            Assert.That( visitor.GlobalScope.SymbolTable.Count, Is.GreaterThan(0));
        }

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
        public void TestCanRecogniseIntegers(string sample, string expectedType){
            FifthLexer lexer = new FifthLexer(new AntlrInputStream(sample));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new ThrowingErrorListener<int>());

            var token = lexer.NextToken();
            Assert.That(token.Type, Is.EqualTo(lexer.TokenTypeMap[expectedType]));
            Assert.That(token.Text, Is.EqualTo(sample));
        }
    }
}