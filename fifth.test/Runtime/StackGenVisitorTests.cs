namespace Fifth.Test.Runtime
{
    using System.Diagnostics.CodeAnalysis;
    using AST;
    using Fifth.Parser.LangProcessingPhases;
    using Fifth.Runtime;
    using Fifth.Runtime.LangProcessingPhases;
    using FluentAssertions;
    using NUnit.Framework;
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
            TestExpressionFragmentEmission("5 + 5", em.WrapValue(5), em.WrapValue(5), em.WrapBinaryFunction<int, int, int>(PrimitiveInteger.add_int_int));
            TestExpressionFragmentEmission("5 - 5", em.WrapValue(5), em.WrapValue(5), em.WrapBinaryFunction<int, int, int>(PrimitiveInteger.subtract_int_int));
            TestExpressionFragmentEmission("5 * 5", em.WrapValue(5), em.WrapValue(5), em.WrapBinaryFunction<int, int, int>(PrimitiveInteger.multiply_int_int));
            TestExpressionFragmentEmission("5 / 5", em.WrapValue(5), em.WrapValue(5), em.WrapBinaryFunction<int, int, int>(PrimitiveInteger.divide_int_int));
        }

        [Test]
        public void TestVariableDeclaration()
        {
            TestExpressionEmission("string x",
                em.WrapValue("string"),
                em.WrapValue("x"),
                em.WrapMetaFunction(MetaFunction.DeclareVariable)
            );
        }

        [Test]
        public void TestStackGenerationForAssignment1()
        {
            TestExpressionEmission("int x = 5 * 1",
                // bind part
                em.WrapValue(5),
                em.WrapValue(1),
                em.WrapBinaryFunction<int, int, int>(PrimitiveInteger.multiply_int_int),
                em.WrapValue("x"),
                em.WrapMetaFunction(MetaFunction.BindVariable),
                // decl part
                em.WrapValue("int"),
                em.WrapValue("x"),
                em.WrapMetaFunction(MetaFunction.DeclareVariable)
            );
        }

        [Test]
        public void TestStackGenerationForAssignment2()
        {
            TestExpressionEmission("float a = 0.1",
                em.WrapValue(0.1F),
                em.WrapValue("a"),
                em.WrapMetaFunction(MetaFunction.BindVariable),

                em.WrapValue("float"),
                em.WrapValue("a"),
                em.WrapMetaFunction(MetaFunction.DeclareVariable)
            );
        }

        [Test]
        public void TestStackGenerationForValueExpressions()
        {
            TestExpressionFragmentEmission("5", em.WrapValue(5));
            TestExpressionFragmentEmission("0.2", em.WrapValue(0.2F));
            TestExpressionFragmentEmission("true", em.WrapValue(true));
            TestExpressionFragmentEmission("x", em.WrapValue("x"), em.WrapMetaFunction(MetaFunction.DereferenceVariable));
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
            dispatcher.Dispatch(); // first for the decl
            dispatcher.Dispatch(); // then for the assignment
            dispatcher.Dispatch(); // finally for the dereference
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
            var sut = new ExpressionStackEmitter(astNode as Expression);
            astNode.Accept(new TypeAnnotatorVisitor());
            sut.Emit(new StackEmitter(), stack);
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

        private void TestExpressionFragmentEmission(string expression, params StackElement[] matchList)
        {
            var stack = ParseAndGenerateExpressionFragment(expression);
            stack.Matches(matchList).Should().BeTrue();
        }

        private static ActivationStack ParseAndGenerateExpression(string expressionString)
        {
            var astNode = ParseExpressionListToAst(expressionString);
            var af = new ActivationFrame();
            var stack = af.Stack;
            var sut = new StackGeneratorVisitor { Stack = stack, Emit = new StackEmitter() };
            astNode.Accept(new TypeAnnotatorVisitor());
            astNode.Accept(sut);
            return stack;
        }
        private static ActivationStack ParseAndGenerateExpressionFragment(string expressionString)
        {
            var astNode = ParseExpressionToAst(expressionString);
            var af = new ActivationFrame();
            var stack = af.Stack;
            var sut = new ExpressionStackEmitter(astNode as Expression);
            astNode.Accept(new TypeAnnotatorVisitor());
            sut.Emit(new StackEmitter(), stack);
            return stack;
        }
    }
}
