namespace Fifth.Test.Parser
{
    using fifth.parser.Parser.LangProcessingPhases;
    using Fifth.Tests;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture()]
    public class AstBuilderVisitorTest : ParserTestBase
    {
        [Test]
        public void TestCanBuildProgram(){
            var ctx = ParseProgram(@"main(int x) => x + 1;");
            var sut = new AstBuilderVisitor();
            var ast = sut.Visit(ctx);
            ast.Should().NotBeNull();
        }
    }
}
