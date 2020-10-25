namespace Fifth.Test.Parser
{
    using System;
    using Fifth.AST;
    using Fifth.Parser.LangProcessingPhases;
    using Fifth.Tests;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture()]
    public class AstBuilderVisitorTest : ParserTestBase
    {
        [TestCase(@"2 + 1", typeof(IntValueExpression), typeof(IntValueExpression), Operator.Plus)]
        [TestCase(@"2 - 1", typeof(IntValueExpression), typeof(IntValueExpression), Operator.Minus)]
        [TestCase(@"2 * 1", typeof(IntValueExpression), typeof(IntValueExpression), Operator.Times)]
        [TestCase(@"2 / 1", typeof(IntValueExpression), typeof(IntValueExpression), Operator.Divide)]
        [TestCase(@"1 + x", typeof(IntValueExpression), typeof(IdentifierExpression), Operator.Plus)]
        [TestCase(@"1 - x", typeof(IntValueExpression), typeof(IdentifierExpression), Operator.Minus)]
        [TestCase(@"1 * x", typeof(IntValueExpression), typeof(IdentifierExpression), Operator.Times)]
        [TestCase(@"1 / x", typeof(IntValueExpression), typeof(IdentifierExpression), Operator.Divide)]
        [TestCase(@"x + 1", typeof(IdentifierExpression), typeof(IntValueExpression), Operator.Plus)]
        [TestCase(@"x - 1", typeof(IdentifierExpression), typeof(IntValueExpression), Operator.Minus)]
        [TestCase(@"x * 1", typeof(IdentifierExpression), typeof(IntValueExpression), Operator.Times)]
        [TestCase(@"x / 1", typeof(IdentifierExpression), typeof(IntValueExpression), Operator.Divide)]
        [TestCase(@"y + x", typeof(IdentifierExpression), typeof(IdentifierExpression), Operator.Plus)]
        [TestCase(@"y - x", typeof(IdentifierExpression), typeof(IdentifierExpression), Operator.Minus)]
        [TestCase(@"y * x", typeof(IdentifierExpression), typeof(IdentifierExpression), Operator.Times)]
        [TestCase(@"y / x", typeof(IdentifierExpression), typeof(IdentifierExpression), Operator.Divide)]
        public void TestCanBuildBinaryExpression(string fragment, Type leftOperandType, Type rightOperandType, Operator op)
        {
            var ctx = ParseExpression(fragment);
            var sut = new AstBuilderVisitor();
            var ast = sut.Visit(ctx);
            ast.Should().BeOfType(typeof(BinaryExpression));
            var binexp = ast as BinaryExpression;
            binexp.Should().NotBeNull();
            binexp.Left.Should().NotBeNull().And.BeOfType(leftOperandType);
            binexp.Right.Should().NotBeNull().And.BeOfType(rightOperandType);
            binexp.Op.Should().Be(op);
        }

        [Test]
        public void TestCanBuildProgram()
        {
            var ctx = ParseProgram(@"main(int x) => x + 1;");
            var sut = new AstBuilderVisitor();
            var ast = sut.Visit(ctx);
            _ = ast.Should().NotBeNull();
        }
    }
}
