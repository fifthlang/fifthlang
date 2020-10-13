using Antlr4.Runtime;
using NUnit.Framework;
using static FifthParser;

namespace fifth.Parser.Tests
{
    [TestFixture()]
    public class ParserTests
    {
        private static string TestProgram => @"use std;
            main(int x, int y) {myprint(x + y);}
            myprint(int x) {std.print(""the answer is "" + x);}";

        [Test]
        public void TestCanParseFullProgram()
        {
            var ctx = ParseProgram(TestProgram);
            var symtab = new SymbolTable();
            var x = new SymbolTableBuilderVisitor(symtab).Visit(ctx);
            Assert.That(symtab.Count, Is.GreaterThan(0));
        }

        private static FifthContext ParseProgram(string program)
        {
         FifthLexer lexer = new FifthLexer(new AntlrInputStream(TestProgram));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new ThrowingErrorListener<int>());

            FifthParser parser = new FifthParser(new CommonTokenStream(lexer));
            parser.RemoveErrorListeners();
            parser.AddErrorListener(new ThrowingErrorListener<IToken>());

            return parser.fifth();
        }
    }
}