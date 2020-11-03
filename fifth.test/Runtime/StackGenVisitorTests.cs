namespace Fifth.Test.Runtime
{
    using System.Collections.Generic;
    using Fifth.Parser.LangProcessingPhases;
    using Fifth.Runtime;
    using Fifth.Runtime.LangProcessingPhases;
    using Fifth.Tests;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture()]
    internal class StackGenVisitorTests : ParserTestBase
    {
        [TestCase("int x = 5 * 1, x", "x", 5)]
        public void TestCanAssignAndDereferenceValue(string code, string varName, object resolvedValue)
        {
            var astNode = ParseExpressionListToAst(code);
            var af = new ActivationFrame();
            var stack = af.Stack;
            var sut = new StackGeneratorVisitor() { Stack = stack };
            astNode.Accept(visitor: new TypeAnnotatorVisitor());
            astNode.Accept(visitor: sut);
            var dispatcher = new Dispatcher(af);
            dispatcher.Dispatch();
            Assert.That(stack.Stack, Has.Count.EqualTo(1));
            var v = stack.Stack.Pop();
            v.IsValue.Should().BeTrue();
            v.Invoke().Should().Be(resolvedValue);
            af.Environment.Count.Should().Be(1);
        }

        [TestCase("8 + 5", 13)]
        [TestCase("8 - 5", 3)]
        [TestCase("8 * 5", 40)]
        [TestCase("100 / 5", 20)]
        public void TestCanExecuteStackGeneratedFromExpression(string code, object resolvedValue)
        {
            var astNode = ParseExpressionToAst(code);
            var af = new ActivationFrame();
            var stack = af.Stack;
            var sut = new StackGeneratorVisitor() { Stack = stack };
            astNode.Accept(visitor: new TypeAnnotatorVisitor());
            astNode.Accept(visitor: sut);
            var dispatcher = new Dispatcher(af);
            dispatcher.Dispatch();
            Assert.That(stack.Stack, Has.Count.EqualTo(1));
            var v = stack.Stack.Pop();
            v.IsValue.Should().BeTrue();
            v.Invoke().Should().Be(resolvedValue);
        }
    }
}
