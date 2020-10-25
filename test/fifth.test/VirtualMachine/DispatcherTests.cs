using Fifth;
using Fifth.VirtualMachine;
using NUnit.Framework;

namespace fifthlang_tests
{
    internal class DispatcherTests
    {
        [Test]
        public void TestCanDispatchAnAdd()
        {
            var f5 = 5.AsFun();
            var add = Fun.Wrap((int x, int y) => x + y);
            FuncStack stack = new FuncStack();
            var sut = new Dispatcher(stack);
            stack.Push(f5);
            stack.Push(f5);
            stack.Push(add);
            Assert.That(stack.Stack, Has.Count.EqualTo(3));
            sut.Dispatch();
            Assert.That(stack.Stack, Has.Count.EqualTo(1));
            var x = stack.Stack.Pop();
            Assert.That(x.IsValue, Is.True);
            Assert.That(x.Invoke(), Is.EqualTo(10));
        }

        [Test]
        public void TestCanDispatchNestedExpressions()
        {
            var add = Fun.Wrap((int x, int y) => x + y);
            var mul = Fun.Wrap((int x, int y) => x * y);
            FuncStack stack = new FuncStack();
            Dispatcher sut = new Dispatcher(stack);
            stack.Push(3.AsFun());
            stack.Push(5.AsFun());
            stack.Push(add);
            stack.Push(7.AsFun());
            stack.Push(mul);
            Assert.That(stack.Stack, Has.Count.EqualTo(5));
            sut.Dispatch();
            Assert.That(stack.Stack, Has.Count.EqualTo(1));
            var x = stack.Stack.Pop();
            Assert.That(x.IsValue, Is.True);
            Assert.That(x.Invoke(), Is.EqualTo(56));
        }

        [Test]
        public void TestCanDispatchStrings()
        {
            var add = Fun.Wrap((string x, string y) => $"{x} {y}");
            FuncStack stack = new FuncStack();
            var sut = new Dispatcher(stack);
            stack.Push("hello".AsFun());
            stack.Push("world".AsFun());
            stack.Push(add);
            Assert.That(stack.Stack, Has.Count.EqualTo(3));
            sut.Dispatch();
            Assert.That(stack.Stack, Has.Count.EqualTo(1));
            var x = stack.Stack.Pop();
            Assert.That(x.IsValue, Is.True);
            Assert.That(x.Invoke(), Is.EqualTo("hello world"));
        }
    }
}