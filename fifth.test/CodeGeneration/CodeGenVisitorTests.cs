namespace Fifth.Test.CodeGeneration
{
    using System;
    using System.IO;
    using System.Net.Security;
    using System.Text;
    using System.Threading.Tasks;
    using AST;
    using Fifth.CodeGeneration;
    using Fifth.CodeGeneration.LangProcessingPhases;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    [Category("Code Generation")]
    [Category("CIL")]
    public class CodeGenVisitorTests
    {
        [Category("WIP")]
        [TestCase("long", "0", "0", "0")]

        [TestCase("long", "0", "1", "1")]
        [TestCase("long", "1", "1", "2")]
        [TestCase("long", "1", "0", "1")]

        [TestCase("long", "0", "-1", "-1")]
        [TestCase("long", "1", "-1", "0")]
        [TestCase("long", "-1", "0", "-1")]

        [TestCase("long", "5", "6", "11")]
        public async Task CanGenerateFromAst(string numberType, string leftNumber, string rightNumber, string expectedResult)
        {
            var prog = $@"
main():int{{
    print(sum());
    return 0;
}}

sum(): {numberType}{{
    a: {numberType} = {leftNumber};
    b: {numberType} = {rightNumber};
    return a+b;
}}";
            var outputs = await TestUtilities.BuildRunAndTestProgramInString(prog);
            outputs.Should().NotBeEmpty().And.Contain(expectedResult);
        }

        [Test]
        public void CopesWithOverloading()
        {
            using var f = TestUtilities.LoadTestResource("Fifth.Test.TestSampleCode.overloading.5th");
            if (FifthParserManager.TryParseFile<FifthProgram>(f.Path, out var ast, out var errors))
            {
                var sb = new StringBuilder();
                var sut = new CodeGenVisitor(new StringWriter(sb));
                sut.VisitFifthProgram(ast);
                var generatedCode = sb.ToString();
                generatedCode.Should().NotBeNullOrWhiteSpace();
                Console.WriteLine(generatedCode);
            }
        }

        [Test]
        public void CopesWithPatternMatchInFuncDef()
        {
            using var f = TestUtilities.LoadTestResource("Fifth.Test.TestSampleCode.destructuring.5th");
            if (FifthParserManager.TryParseFile<FifthProgram>(f.Path, out var ast, out var errors))
            {
                var sb = new StringBuilder();
                var sut = new CodeGenVisitor(new StringWriter(sb));
                sut.VisitFifthProgram(ast);
                var generatedCode = sb.ToString();
                generatedCode.Should().NotBeNullOrWhiteSpace();
                Console.WriteLine(generatedCode);
            }
        }

        [Test]
        public void CopesWithClassDefinition()
        {
            using var f = TestUtilities.LoadTestResource("Fifth.Test.TestSampleCode.class-definition.5th");
            if (FifthParserManager.TryParseFile<FifthProgram>(f.Path, out var ast, out var errors))
            {
                var sb = new StringBuilder();
                var sut = new CodeGenVisitor(new StringWriter(sb));
                sut.VisitFifthProgram(ast);
                var generatedCode = sb.ToString();
                generatedCode.Should().NotBeNullOrWhiteSpace();
                Console.WriteLine(generatedCode);
            }
        }

        [Test]
        public void CopesWithPropertyAccess()
        {
            using var f = TestUtilities.LoadTestResource("Fifth.Test.TestSampleCode.property-access.5th");
            if (FifthParserManager.TryParseFile<FifthProgram>(f.Path, out var ast, out var errors))
            {
                var sb = new StringBuilder();
                var sut = new CodeGenVisitor(new StringWriter(sb));
                sut.VisitFifthProgram(ast);
                var generatedCode = sb.ToString();
                generatedCode.Should().NotBeNullOrWhiteSpace();
                Console.WriteLine(generatedCode);
            }
        }
    }
}
