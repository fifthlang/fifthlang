namespace Fifth.Test.Parser
{
    using AST;
    using Fifth.Parser.LangProcessingPhases;
    using FluentAssertions;
    using NUnit.Framework;
    using Tests;

    [TestFixture]
    public class TypeAnnotatorVisitorTests : ParserTestBase
    {
        [Test]
        public void IfBinaryExpressionElementsAreInt_TheExpressionIsInt()
        {
            var exp = "5 + 6";
            var astNode = ParseExpressionToAst(exp);
            astNode.HasAnnotation("type").Should().BeFalse();
            var sut = new TypeAnnotatorVisitor();
            astNode.Accept(sut);
            astNode.HasAnnotation("type").Should().BeTrue();
        }
    }
}
