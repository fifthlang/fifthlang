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
            main(x: int, y: int):void { myprint(x + y);}
            myprint(x: int): void {std.print(""the answer is "" + x);}";

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
            var TestProgram = @"main():void {myprint(""hello world"");}
            myprint(x: string):void { print(x);}
            blah():void{ result: int = 5; return result;}";

            if (!FifthParserManager.TryParse<FifthProgram>(TestProgram, out var ast, out var errors))
                Assert.Fail();
            var globalScope = ast.NearestScope();

            var visitor = new SymbolTableBuilderVisitor();
            ast.Accept(visitor);
            var symtab = globalScope.SymbolTable;
            Assert.That(symtab.Count, Is.EqualTo(5)); // the three above plus two builtins
            foreach (var v in symtab.Values.ToArray()[0..2])
            {
                Assert.That(v.SymbolKind, Is.EqualTo(SymbolKind.FunctionDeclaration));
            }
            foreach (var v in symtab.Values.ToArray()[3..4])
            {
                Assert.That(v.SymbolKind, Is.EqualTo(SymbolKind.BuiltinFunctionDeclaration));
            }
        }

        [Test]
        public void TestCanGatherSingleFunctionDefinitions()
        {
            var TestProgram = @"main():void{myprint(""hello world"");}";

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
            main(x:int, y: int):void{myprint(x + y);}
            myprint(x:int):void{std.print(""the answer is "" + x);}";

            var ast = ParseProgramToAst(TestProgram) as FifthProgram;
            var globalScope = ast.NearestScope();

            var visitor = new SymbolTableBuilderVisitor();
            ast.Accept(visitor);
            Assert.That(globalScope.SymbolTable.Count, Is.EqualTo(2));
            Assert.That(globalScope.SymbolTable.ContainsKey("main"), Is.True);
        }
    }
}
