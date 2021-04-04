namespace Fifth.Test.CodeGeneration
{
    using System.IO;
    using System.Text;
    using AST;
    using Fifth.CodeGeneration;
    using Fifth.Runtime;
    using FluentAssertions;
    using NUnit.Framework;
    using TypeSystem;

    [TestFixture]
    public class CodeGenVisitorTests
    {
        [Test]
        public void CanGenerateFromAst()
        {
            var prog = @"int main()=>print('hello world'); int print(string s)=>5+6;";
            TypeRegistry.DefaultRegistry.LoadPrimitiveTypes();
            InbuiltOperatorRegistry.DefaultRegistry.LoadBuiltinOperators();
            var ast = FifthRuntime.ParseAndAnnotateProgram(prog, out var rootFrame) as FifthProgram;
            var sb = new StringBuilder();
            var sut = new CodeGenVisitor(new StringWriter(sb));
            ast.Accept(sut);
            var generatedCode = sb.ToString();
            generatedCode.Should().NotBeNullOrWhiteSpace();
        }
    }
}
