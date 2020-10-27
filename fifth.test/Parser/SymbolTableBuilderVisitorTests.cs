namespace Fifth.Tests
{
    using System.Linq;
    using Antlr4.Runtime;
    using Antlr4.Runtime.Tree;
    using Fifth.AST;
    using Fifth.Parser;
    using Fifth.Parser.LangProcessingPhases;
    using NUnit.Framework;

    [TestFixture()]
    public class SymbolTableBuilderVisitorTests : ParserTestBase
    {
        [Test]
        public void TestCanAccessScopeForMain()
        {
            var TestProgram = @"use std;
            main(int x, int y) => myprint(x + y);
            myprint(int x) => std.print(""the answer is "" + x);";

            var ast = ParseProgramToAst(TestProgram) as FifthProgram;
            var annotatedAst = new AnnotatedSyntaxTree(ast);
            var visitor = new SymbolTableBuilderVisitor(annotatedAst);
            ast.Accept(visitor);
            Assert.That(visitor.GlobalScope.SymbolTable.Count, Is.EqualTo(2));
            var mainfuncdecl = ast.Functions.First(f => f.Name == "main");
            var mainScope = annotatedAst.ScopeLookupTable[mainfuncdecl];
            Assert.That(mainScope, Is.Not.Null);
            Assert.That(mainScope.SymbolTable.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestCanGatherMultipleDefinitions()
        {
            var TestProgram = @"main() => myprint(""hello world"");
            myprint(string x) => std.print(x);
            blah() => int result = 5, result;";

            var ast = ParseProgramToAst(TestProgram) as FifthProgram;
            var annotatedAst = new AnnotatedSyntaxTree(ast);
            var visitor = new SymbolTableBuilderVisitor(annotatedAst);
            ast.Accept(visitor);
            var symtab = visitor.GlobalScope.SymbolTable;
            Assert.That(symtab.Count, Is.EqualTo(3));
            foreach (var v in symtab.Values)
            {
                Assert.That(v.SymbolKind, Is.EqualTo(SymbolKind.FunctionDeclaration));
            }
        }

        [Test]
        public void TestCanGatherSingleFunctionDefinitions()
        {
            var TestProgram = @"main() => myprint(""hello world"");";

            var ast = ParseProgramToAst(TestProgram) as FifthProgram;
            var annotatedAst = new AnnotatedSyntaxTree(ast);
            var visitor = new SymbolTableBuilderVisitor(annotatedAst);
            ast.Accept(visitor);
            var symtab = visitor.GlobalScope.SymbolTable;
            Assert.That(symtab.Count, Is.EqualTo(1));
            Assert.That(symtab.Resolve("main"), Is.Not.Null);
        }

        [Test]
        public void TestCanParseFullProgram()
        {
            var TestProgram = @"use std;
            main(int x, int y) => myprint(x + y);
            myprint(int x) => std.print(""the answer is "" + x);";

            var ast = ParseProgramToAst(TestProgram) as FifthProgram;
            var annotatedAst = new AnnotatedSyntaxTree(ast);
            var visitor = new SymbolTableBuilderVisitor(annotatedAst);
            ast.Accept(visitor);
            Assert.That(visitor.GlobalScope.SymbolTable.Count, Is.EqualTo(2));
            var astRoot = annotatedAst.AstRoot as FifthProgram;
            var mainfuncdecl = astRoot.Functions.First(f => f.Name == "main");
            Assert.That(annotatedAst.ScopeLookupTable.ContainsKey(mainfuncdecl), Is.True);
        }
    }
}
