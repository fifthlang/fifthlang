namespace Fifth.Test.Runtime
{
    using System.Diagnostics.CodeAnalysis;
    using Fifth.Runtime;
    using Fifth.Runtime.LangProcessingPhases;
    using FluentAssertions;
    using NUnit.Framework;
    using Parser.LangProcessingPhases;
    using PrimitiveTypes;
    using Tests;

    [TestFixture]
    [SuppressMessage("Style", "IDE0022:Use expression body for methods", Justification = "<Pending>")]
    internal class StackGenVisitorTests : ParserTestBase
    {
        private readonly StackEmitter em = new StackEmitter();

        [Test]
        public void TestStackGenerationForBinaryExpressions()
        {
            TestExpressionEmission("5 + 5", em.WrapBinaryFunction<int, int, int>(PrimitiveInteger.add_int_int), em.WrapValue(5), em.WrapValue(5));
            TestExpressionEmission("5 - 5", em.WrapBinaryFunction<int, int, int>(PrimitiveInteger.subtract_int_int), em.WrapValue(5), em.WrapValue(5));
            TestExpressionEmission("5 * 5", em.WrapBinaryFunction<int, int, int>(PrimitiveInteger.multiply_int_int), em.WrapValue(5), em.WrapValue(5));
            TestExpressionEmission("5 / 5", em.WrapBinaryFunction<int, int, int>(PrimitiveInteger.divide_int_int), em.WrapValue(5), em.WrapValue(5));
        }

        [Test]
        [Category("WIP")]
        public void TestStackGenerationForAssignment()
        {
            TestExpressionEmission("int x = 5 * 1",
                em.WrapMetaFunction(MetaFunction.Assign),
                em.WrapMetaFunction(MetaFunction.DereferenceVariable),
                em.WrapVariableReference("x"),
                em.WrapBinaryFunction<int, int, int>(PrimitiveInteger.multiply_int_int),
                em.WrapValue(5),
                em.WrapValue(1)
                );
        }

        [Test]
        public void TestStackGenerationForValueExpressions()
        {
            TestExpressionEmission("5", em.WrapValue(5));
            TestExpressionEmission("0.2", em.WrapValue(0.2F));
            TestExpressionEmission("true", em.WrapValue(true));
            TestExpressionEmission("x", em.WrapMetaFunction(MetaFunction.DereferenceVariable), em.WrapValue("x"));
        }

        [TestCase("int x = 5 * 1, x", "x", 5)]
        public void TestCanAssignAndDereferenceValue(string code, string varName, object resolvedValue)
        {
            var astNode = ParseExpressionListToAst(code);
            var af = new ActivationFrame();
            var stack = af.Stack;
            var sut = new StackGeneratorVisitor {Stack = stack, Emit = new StackEmitter()};
            astNode.Accept(new TypeAnnotatorVisitor());
            astNode.Accept(sut);
            var dispatcher = new Dispatcher(af);
            dispatcher.Dispatch();
            stack.Count.Should().Be(1);
            var v = stack.Pop();
            v.Should().BeOfType<ValueStackElement>();
            ((ValueStackElement)v).Value.Should().Be(resolvedValue);
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
            var sut = new StackGeneratorVisitor {Stack = stack, Emit = new StackEmitter()};
            astNode.Accept(new TypeAnnotatorVisitor());
            astNode.Accept(sut);
            var dispatcher = new Dispatcher(af);
            dispatcher.Dispatch();
            Assert.That(stack, Has.Count.EqualTo(1));
            var v = stack.Pop();
            v.Should().BeOfType<ValueStackElement>();
            ((ValueStackElement)v).Value.Should().Be(resolvedValue);
        }

        private void TestExpressionEmission(string expression, params StackElement[] matchList)
        {
            var stack = ParseAndGenerateExpression(expression);
            stack.Matches(matchList).Should().BeTrue();
        }

        private static ActivationStack ParseAndGenerateExpression(string expressionString)
        {
            var astNode = ParseExpressionToAst(expressionString);
            var af = new ActivationFrame();
            var stack = af.Stack;
            var sut = new StackGeneratorVisitor {Stack = stack, Emit = new StackEmitter()};
            astNode.Accept(new TypeAnnotatorVisitor());
            astNode.Accept(sut);
            return stack;
        }
    }
}
