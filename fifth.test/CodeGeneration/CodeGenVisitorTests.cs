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
            var prog = @"
int main(){
    return print('hello world');
}

long print(string s){
    long a = 5;
    long b = 6;
    return a+b;
}";
            var ast = FifthParserManager.ParseProgram(prog);
            var sb = new StringBuilder();
            var sut = new CodeGenVisitor(new StringWriter(sb));
            ast.Accept(sut);
            var generatedCode = sb.ToString();
            generatedCode.Should().NotBeNullOrWhiteSpace();
        }
    }
}
