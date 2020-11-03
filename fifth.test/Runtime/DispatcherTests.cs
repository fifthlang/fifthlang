namespace Fifth.Tests
{
    using Fifth.Runtime;
    using NUnit.Framework;

    internal class DispatcherTests
    {
        [Test]
        public void TestCanDispatchAnAdd()
        {
            var f5 = 5.AsFun();
            var add = Fun.Wrap((int x, int y) => x + y);
            var frame = new ActivationFrame();
            var sut = new Dispatcher(frame);
            frame.Stack.Push(f5);
            frame.Stack.Push(f5);
            frame.Stack.Push(add);
            Assert.That(frame.Stack.Stack, Has.Count.EqualTo(3));
            sut.Dispatch();
            Assert.That(frame.Stack.Stack, Has.Count.EqualTo(1));
            var x = frame.Stack.Stack.Pop();
            Assert.That(x.IsValue, Is.True);
            Assert.That(x.Invoke(), Is.EqualTo(10));
        }

        [Test]
        public void TestCanDispatchNestedExpressions()
        {
            var add = Fun.Wrap((int x, int y) => x + y);
            var mul = Fun.Wrap((int x, int y) => x * y);
            var frame = new ActivationFrame();
            var sut = new Dispatcher(frame);
            frame.Stack.Push(3.AsFun());
            frame.Stack.Push(5.AsFun());
            frame.Stack.Push(add);
            frame.Stack.Push(7.AsFun());
            frame.Stack.Push(mul);
            Assert.That(frame.Stack.Stack, Has.Count.EqualTo(5));
            sut.Dispatch();
            Assert.That(frame.Stack.Stack, Has.Count.EqualTo(1));
            var x = frame.Stack.Stack.Pop();
            Assert.That(x.IsValue, Is.True);
            Assert.That(x.Invoke(), Is.EqualTo(56));
        }

        [Test]
        public void TestCanDispatchStrings()
        {
            var add = Fun.Wrap((string x, string y) => $"{x} {y}");
            var frame = new ActivationFrame();
            var sut = new Dispatcher(frame);
            frame.Stack.Push("hello".AsFun());
            frame.Stack.Push("world".AsFun());
            frame.Stack.Push(add);
            Assert.That(frame.Stack.Stack, Has.Count.EqualTo(3));
            sut.Dispatch();
            Assert.That(frame.Stack.Stack, Has.Count.EqualTo(1));
            var x = frame.Stack.Stack.Pop();
            Assert.That(x.IsValue, Is.True);
            Assert.That(x.Invoke(), Is.EqualTo("hello world"));
        }
    }
}
