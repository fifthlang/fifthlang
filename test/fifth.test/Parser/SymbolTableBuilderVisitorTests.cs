using Antlr4.Runtime;
using NUnit.Framework;
using static FifthParser;

namespace fifth.Parser.Tests
{
    [TestFixture()]
    public class SymbolTableBuilderVisitorTests : ParserTestBase
    {
        [Test]
        public void TestCanGatherSingleFunctionDefinitions()
        {
            string TestProgram = @"main() {myprint(""hello world"");}";

            var ctx = ParseProgram(TestProgram);
            var symtab = new SymbolTable();
            var x = new SymbolTableBuilderVisitor(symtab).Visit(ctx);
            Assert.That(symtab.Count, Is.EqualTo(1));
            Assert.That(symtab.Resolve("main"), Is.Not.Null);
        }
                [Test]
        public void TestCanGatherMultipleDefinitions()
        {
            string TestProgram = @"main() {myprint(""hello world"");}
            myprint(string x) {std.print(x);}
            blah() {int result = 5; return result;}";

            var ctx = ParseProgram(TestProgram);
            var symtab = new SymbolTable();
            var x = new SymbolTableBuilderVisitor(symtab).Visit(ctx);
            Assert.That(symtab.Count, Is.EqualTo(3));
            Assert.That(symtab.All(), Has.Property("Kind").EqualTo(SymbolKind.FunctionDeclaration));
        }
    }
}