namespace Fifth.Tests
{
    using System.Linq;
    using Fifth.AST;
    using Fifth.Parser.LangProcessingPhases;
    using NUnit.Framework;
    using Symbols;
    using TypeSystem;

    [TestFixture, Category("Symbol Table"), Category("Parsing")]
    public class SymbolTableBuilderVisitorTests : ParserTestBase
    {
        [Test]
        public void TestCanAccessScopeForMain()
        {
            var TestProgram = @"use std;
            void main(int x, int y) => myprint(x + y);
            void myprint(int x) => std.print(""the answer is "" + x);";

            var ast = ParseProgramToAst(TestProgram) as FifthProgram;
            var globalScope = ast.NearestScope();
            var visitor = new SymbolTableBuilderVisitor();
            ast.Accept(visitor);
            Assert.That(globalScope.SymbolTable.Count, Is.EqualTo(2));
            var mainfuncdecl = ast.Functions.First(f => f.Name == "main");
            var mainScope = mainfuncdecl.NearestScope();
            Assert.That(mainScope, Is.Not.Null);
            Assert.That(mainScope.SymbolTable.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestCanGatherMultipleDefinitions()
        {
            var TestProgram = @"void main() => myprint(""hello world"");
            void myprint(string x) => std.print(x);
            void blah() => int result = 5, result;";

            var ast = ParseProgramToAst(TestProgram) as FifthProgram;
            var globalScope = ast.NearestScope();

            var visitor = new SymbolTableBuilderVisitor();
            ast.Accept(visitor);
            var symtab = globalScope.SymbolTable;
            Assert.That(symtab.Count, Is.EqualTo(3));
            foreach (var v in symtab.Values)
            {
                Assert.That(v.SymbolKind, Is.EqualTo(SymbolKind.FunctionDeclaration));
            }
        }

        [Test]
        public void TestCanGatherSingleFunctionDefinitions()
        {
            var TestProgram = @"void main() => myprint(""hello world"");";

            var ast = ParseProgramToAst(TestProgram) as FifthProgram;
            var globalScope = ast.NearestScope();

            var visitor = new SymbolTableBuilderVisitor();
            ast.Accept(visitor);
            var symtab = globalScope.SymbolTable;
            Assert.That(symtab.Count, Is.EqualTo(1));
            Assert.That(symtab.Resolve("main"), Is.Not.Null);
        }

        [Test]
        public void TestCanParseFullProgram()
        {
            var TestProgram = @"use std;
            void main(int x, int y) => myprint(x + y);
            void myprint(int x) => std.print(""the answer is "" + x);";

            var ast = ParseProgramToAst(TestProgram) as FifthProgram;
            var globalScope = ast.NearestScope();

            var visitor = new SymbolTableBuilderVisitor();
            ast.Accept(visitor);
            Assert.That(globalScope.SymbolTable.Count, Is.EqualTo(2));
            Assert.That(globalScope.SymbolTable.ContainsKey("main"), Is.True);
        }
    }
}
