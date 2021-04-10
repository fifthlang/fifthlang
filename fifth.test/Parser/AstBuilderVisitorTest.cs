namespace Fifth.Tests.Parser
{
    using System;
    using Fifth;
    using Fifth.AST;
    using Fifth.Parser.LangProcessingPhases;
    using Fifth.Tests;
    using FluentAssertions;
    using NUnit.Framework;
    using TypeSystem;

    [TestFixture, Category("AST"), Category("Visitors")]
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
            if (FifthParserManager.TryParse<Expression>(fragment, out var ast, out var errors))
            {
                errors.Should().NotBeNull().And.BeEmpty();
                ast.Should().NotBeNull();
                _ = ast.Should().BeOfType(typeof(BinaryExpression));
                var binexp = ast as BinaryExpression;
                _ = binexp.Should().NotBeNull();
                _ = binexp.Left.Should().NotBeNull().And.BeOfType(leftOperandType);
                _ = binexp.Right.Should().NotBeNull().And.BeOfType(rightOperandType);
                _ = binexp.Op.Should().Be(op);
            }
            else
            {
                Assert.Fail("failed to parse expression");
            }
        }

        [TestCase(@"void main(int x){return x + 1;}")]
        [TestCase(@"void main(int x){int x = 234; return x + 1;}")]
        [TestCase(@"void main(int x){int x = 234; return x + 1;} int foo(){return 43;}", 2)]
        [TestCase(@"void main(int x){int x = 234; return x + 1;}
void foo(){return 43;}", 2)]
        public void TestCanBuildProgram(string programText, int funcCount = 1)
        {
            if (FifthParserManager.TryParse<FifthProgram>(programText, out var ast, out var errors))
            {
                errors.Should().NotBeNull().And.BeEmpty();
                ast.Should().NotBeNull();
                _ = ast.Should().NotBeNull();
                _ = ast.Functions.Should().NotBeNull().And.HaveCount(funcCount);
            }
            else
            {
                Assert.Fail("failed to parse expression");
            }
        }

        [TestCase(@"alias tu as http://tempuri.com/blah/; void main(int x){int x = 234; return x + 1;}", 1)]
        [TestCase(@"alias a1 as http://tempuri.com/blah/;
                    alias a2 as http://tempuri.com/bob;
                    void main(int x){int x = 234; return x + 1;}", 2)]
        public void TestCanConstructAliasesFromProgram(string programText, int aliasCount)
        {
            TypeRegistry.DefaultRegistry.LoadPrimitiveTypes();
            InbuiltOperatorRegistry.DefaultRegistry.LoadBuiltinOperators();
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


        [TestCase(@"
void main(int x){
int a = 5;
long b = 490;
return (long)a + b;
}")]
        public void TestCanCast(string programText)
        {
            var ast = FifthParserManager.ParseProgram(programText);
            ast.Should().NotBeNull();
        }
    }
}
