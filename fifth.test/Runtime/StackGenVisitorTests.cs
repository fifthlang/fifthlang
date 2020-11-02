namespace Fifth.Test.Runtime
{
    using System.Collections.Generic;
    using Fifth.Parser.LangProcessingPhases;
    using Fifth.Runtime;
    using Fifth.Runtime.LangProcessingPhases;
    using Fifth.Tests;
    using NUnit.Framework;

    [TestFixture()]
    internal class StackGenVisitorTests : ParserTestBase
    {
        public static IEnumerable<(string, object)> ExpressionCases()
        {
            yield return ("8 + 5", 13);
            yield return ("8 - 5", 3);
            yield return ("8 * 5", 40);
            yield return ("100 / 5", 20);
        }

        [TestCaseSource("ExpressionCases")]
        public void TestCanExecuteStackGeneratedFromExpression((string code, object resolvedValue) x)
        {
            var astNode = ParseExpressionToAst(x.code);
            var stack = new FuncStack();
            var sut = new StackGeneratorVisitor() { Stack = stack };
            astNode.Accept(visitor: new TypeAnnotatorVisitor());
            astNode.Accept(visitor: sut);
            var dispatcher = new Dispatcher(stack);
            dispatcher.Dispatch();
            Assert.That(stack.Stack, Has.Count.EqualTo(1));
            var v = stack.Stack.Pop();
            Assert.That(v.IsValue, Is.True);
            Assert.That(v.Invoke(), Is.EqualTo(x.resolvedValue));
        }
    }
}
