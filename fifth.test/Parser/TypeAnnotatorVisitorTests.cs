namespace Fifth.Test.Parser
{
    using Fifth.Parser.LangProcessingPhases;
    using FluentAssertions;
    using NUnit.Framework;
    using Tests;

    [TestFixture, Category("Type Checking"), Category("Visitors")]
    public class TypeAnnotatorVisitorTests : ParserTestBase
    {
        [Test, Ignore("Requires massive refactoring to work out of context")]
        public void IfBinaryExpressionElementsAreInt_TheExpressionIsInt()
        {
            var exp = "5 + 6";
            var astNode = ParseExpressionToAst(exp);
            _ = astNode.HasAnnotation("type").Should().BeFalse();
            var sut = new TypeAnnotatorVisitor();
            astNode.Accept(sut);
            _ = astNode.HasAnnotation("type").Should().BeTrue();
        }
    }
}
