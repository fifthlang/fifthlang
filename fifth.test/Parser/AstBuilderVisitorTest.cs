namespace Fifth.Tests.Parser;

using System;
using Antlr4.Runtime;
using Fifth;
using Fifth.AST;
using Fifth.Parser.LangProcessingPhases;
using Fifth.Tests;
using FluentAssertions;
using LangProcessingPhases;
using NUnit.Framework;
using TypeSystem;

[TestFixture, Category("AST"), Category("Visitors")]
public class AstBuilderVisitorTest : ParserTestBase
{
    [TestCase(@"2 + 1", typeof(IntValueExpression), typeof(IntValueExpression), Operator.Add)]
    [TestCase(@"2 - 1", typeof(IntValueExpression), typeof(IntValueExpression), Operator.Subtract)]
    [TestCase(@"2 * 1", typeof(IntValueExpression), typeof(IntValueExpression), Operator.Multiply)]
    [TestCase(@"2 / 1", typeof(IntValueExpression), typeof(IntValueExpression), Operator.Divide)]
    [TestCase(@"1 + x", typeof(IntValueExpression), typeof(VariableReference), Operator.Add)]
    [TestCase(@"1 - x", typeof(IntValueExpression), typeof(VariableReference), Operator.Subtract)]
    [TestCase(@"1 * x", typeof(IntValueExpression), typeof(VariableReference), Operator.Multiply)]
    [TestCase(@"1 / x", typeof(IntValueExpression), typeof(VariableReference), Operator.Divide)]
    [TestCase(@"x + 1", typeof(VariableReference), typeof(IntValueExpression), Operator.Add)]
    [TestCase(@"x - 1", typeof(VariableReference), typeof(IntValueExpression), Operator.Subtract)]
    [TestCase(@"x * 1", typeof(VariableReference), typeof(IntValueExpression), Operator.Multiply)]
    [TestCase(@"x / 1", typeof(VariableReference), typeof(IntValueExpression), Operator.Divide)]
    [TestCase(@"y + x", typeof(VariableReference), typeof(VariableReference), Operator.Add)]
    [TestCase(@"y - x", typeof(VariableReference), typeof(VariableReference), Operator.Subtract)]
    [TestCase(@"y * x", typeof(VariableReference), typeof(VariableReference), Operator.Multiply)]
    [TestCase(@"y / x", typeof(VariableReference), typeof(VariableReference), Operator.Divide)]
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

    [TestCase(@"main(x:int):void {return x + 1;}")]
    [TestCase(@"main(x:int):void {x:int = 234; return x + 1;}")]
    [TestCase(@"main(x:int):void {x:int = 234; return x + 1;}  foo():int{return 43;}", 5)]
    [TestCase(@"main(x:int):void {x:int = 234; return x + 1;}  foo():void{return 43;}", 5)]
    public void TestCanBuildProgram(string programText, int funcCount = 4)
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

    [TestCase(@"alias tu as http://tempuri.com/blah/; main(x: int):void{x: int = 234; return x + 1;}", 1)]
    [TestCase(@"alias a1 as http://tempuri.com/blah/;
                    alias a2 as http://tempuri.com/bob;
                    main(x: int):void{x: int = 234; return x + 1;}", 2)]
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
 main(x: int):void{
a: int = 5;
b: long  = 490;
return (long)a + b;
}")]
    public void TestCanCast(string programText)
    {
        var ast = FifthParserManager.ParseProgram(CharStreams.fromString(programText));
        ast.Should().NotBeNull();
    }
}
