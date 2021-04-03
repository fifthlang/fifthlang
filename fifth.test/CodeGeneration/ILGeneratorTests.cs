namespace Fifth.Test.CodeGeneration
{
    using AST;
    using Fifth.CodeGeneration;
    using Fifth.Runtime;
    using FluentAssertions;
    using NUnit.Framework;
    using TypeSystem;

    [TestFixture, Category("IL"), Category("Code Generation")]
    public class ILGeneratorTests
    {
        [Test]
        public void CanLoadTemplateCollection()
        {
            TemplateLoader.LoadTemplates().Should().NotBeNull();
        }
        [Test]
        public void CanReferenceMultipleTemplates()
        {
            var prog = @"int main()=>print('hello world'); int print(string s)=>5+6;";
            TypeRegistry.DefaultRegistry.LoadPrimitiveTypes();
            InbuiltOperatorRegistry.DefaultRegistry.LoadBuiltinOperators();
            var ast = FifthRuntime.ParseAndAnnotateProgram(prog, out var rootFrame) as FifthProgram;
            var code = CodeGenerator.GenerateCode(ast);
            code.Should().NotBeNullOrEmpty();
        }
    }
}
