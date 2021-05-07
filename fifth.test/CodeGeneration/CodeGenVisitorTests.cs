namespace Fifth.Test.CodeGeneration
{
    using System;
    using System.IO;
    using System.Text;
    using AST;
    using Fifth.CodeGeneration;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    [Category("Code Generation")]
    [Category("CIL")]
    public class CodeGenVisitorTests
    {
        [Test]
        public void CanGenerateFromAst()
        {
            var prog = @"
int main(){
    return print('hello world');
}

long print(string s){
    long a = 5;
    long b = 6;
    return a+b;
}";
            if (FifthParserManager.TryParse<FifthProgram>(prog, out var ast, out var errors))
            {
                var sb = new StringBuilder();
                var sut = new CodeGenVisitor(new StringWriter(sb));
                ast.Accept(sut);
                var generatedCode = sb.ToString();
                generatedCode.Should().NotBeNullOrWhiteSpace();
                Console.WriteLine(generatedCode);
            }
        }

        [Test, Category("WIP")]
        public void CopesWithPatternMatchInFuncDef()
        {
            using var f = TestUtilities.LoadTestResource("Fifth.Test.TestSampleCode.pattern-matching.5th"); 
            if (FifthParserManager.TryParseFile<FifthProgram>(f.Path, out var ast, out var errors))
            {
                var sb = new StringBuilder();
                var sut = new CodeGenVisitor(new StringWriter(sb));
                ast.Accept(sut);
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
                ast.Accept(sut);
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
                ast.Accept(sut);
                var generatedCode = sb.ToString();
                generatedCode.Should().NotBeNullOrWhiteSpace();
                Console.WriteLine(generatedCode);
            }
        }
    }
}
