namespace Fifth.Tests.Parser
{
    using System;
    using Fifth;
    using Fifth.AST;
    using Fifth.Parser.LangProcessingPhases;
    using Fifth.Tests;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture()]
    public class AstBuilderVisitorTest : ParserTestBase
    {
        [TestCase(@"2 + 1", typeof(IntValueExpression), typeof(IntValueExpression), Operator.Add)]
        [TestCase(@"2 - 1", typeof(IntValueExpression), typeof(IntValueExpression), Operator.Subtract)]
        [TestCase(@"2 * 1", typeof(IntValueExpression), typeof(IntValueExpression), Operator.Multiply)]
        [TestCase(@"2 / 1", typeof(IntValueExpression), typeof(IntValueExpression), Operator.Divide)]
        [TestCase(@"1 + x", typeof(IntValueExpression), typeof(IdentifierExpression), Operator.Add)]
        [TestCase(@"1 - x", typeof(IntValueExpression), typeof(IdentifierExpression), Operator.Subtract)]
        [TestCase(@"1 * x", typeof(IntValueExpression), typeof(IdentifierExpression), Operator.Multiply)]
        [TestCase(@"1 / x", typeof(IntValueExpression), typeof(IdentifierExpression), Operator.Divide)]
        [TestCase(@"x + 1", typeof(IdentifierExpression), typeof(IntValueExpression), Operator.Add)]
        [TestCase(@"x - 1", typeof(IdentifierExpression), typeof(IntValueExpression), Operator.Subtract)]
        [TestCase(@"x * 1", typeof(IdentifierExpression), typeof(IntValueExpression), Operator.Multiply)]
        [TestCase(@"x / 1", typeof(IdentifierExpression), typeof(IntValueExpression), Operator.Divide)]
        [TestCase(@"y + x", typeof(IdentifierExpression), typeof(IdentifierExpression), Operator.Add)]
        [TestCase(@"y - x", typeof(IdentifierExpression), typeof(IdentifierExpression), Operator.Subtract)]
        [TestCase(@"y * x", typeof(IdentifierExpression), typeof(IdentifierExpression), Operator.Multiply)]
        [TestCase(@"y / x", typeof(IdentifierExpression), typeof(IdentifierExpression), Operator.Divide)]
        public void TestCanBuildBinaryExpression(string fragment, Type leftOperandType, Type rightOperandType, Operator op)
        {
            var ctx = ParseExpression(fragment);
            var sut = new AstBuilderVisitor();
            var ast = sut.Visit(ctx);
            _ = ast.Should().BeOfType(typeof(BinaryExpression));
            var binexp = ast as BinaryExpression;
            _ = binexp.Should().NotBeNull();
            _ = binexp.Left.Should().NotBeNull().And.BeOfType(leftOperandType);
            _ = binexp.Right.Should().NotBeNull().And.BeOfType(rightOperandType);
            _ = binexp.Op.Should().Be(op);
        }

        [TestCase(@"void main(int x) => x + 1;")]
        [TestCase(@"void main(int x) => int x = 234, x + 1;")]
        [TestCase(@"void main(int x) => int x = 234, x + 1;int foo()=>43;", 2)]
        [TestCase(@"void main(int x) => int x = 234, x + 1;
void foo()=>43;", 2)]
        public void TestCanBuildProgram(string programText, int funcCount = 1)
        {
            var ctx = ParseProgram(programText);
            var sut = new AstBuilderVisitor();
            var ast = sut.VisitFifth(ctx) as FifthProgram;
            _ = ast.Should().NotBeNull();
            _ = ast.Functions.Should().NotBeNull().And.HaveCount(funcCount);
        }

        [TestCase(@"alias <http://tempuri.com/blah/> as tu; void main(int x) => int x = 234, x + 1;", 1)]
        [TestCase(@"
alias <http://tempuri.com/blah/> as a1;
alias <http://tempuri.com/bob> as a2;
void main(int x) => int x = 234, x + 1;", 2)]
        public void TestCanConstructAliasesFromProgram(string programText, int aliasCount)
        {
            var ctx = ParseProgram(programText);
            var sut = new AstBuilderVisitor();
            var ast = sut.VisitFifth(ctx) as FifthProgram;
            _ = ast.Should().NotBeNull();
            _ = ast.Functions.Should().NotBeNull().And.HaveCount(1);
            _ = ast.Aliases.Should().NotBeNull().And.HaveCount(aliasCount);
        }

        [TestCase(@":blah")]
        [TestCase(@"p:blah")]
        [TestCase(@"http://tempuri.com/blah/")]
        [TestCase(@"http://tempuri.com/blah#")]
        [TestCase(@"http://tempuri.com?blah=value")]
        [TestCase(@"http://tempuri.com?blah=value+vakgkjhg")]
        [TestCase(@"http://tempuri.com#fragid?blah=value+vakgkjhg")]
        [TestCase(@"http://tempuri.com#fragid?blah=value%20vakgkjhg")]
        [TestCase(@"http://tempuri.com#fragid")]
        [TestCase(@"http://tempuri.com/blah#fragid")]
        public void TestCanParseIri(string iriText) => _ = ParseIri(iriText).Should().NotBeNull();
    }
}
