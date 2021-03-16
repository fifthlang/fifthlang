#pragma warning disable IDE0058 // Expression value is never used
namespace Fifth.Tests
{
    using Fifth.Runtime;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture(Category = "Dispatching")]
    public class DispatcherTests
    {
        [Test]
        public void TestCanDispatchAnAdd()
        {
            var add = Fun.Wrap((int x, int y) => x + y);
            var frame = new ActivationFrame();
            var sut = new Dispatcher(frame);
            frame.Stack.PushConstantValue(5);
            frame.Stack.PushConstantValue(5);
            frame.Stack.PushFunction(add);
            Assert.That(frame.Stack, Has.Count.EqualTo(3));
            sut.Dispatch();
            Assert.That(frame.Stack, Has.Count.EqualTo(1));
            var x = frame.Stack.Pop();
            x.Should().BeOfType<ValueStackElement>();
            var y = x as ValueStackElement;
            y.Should().NotBeNull();
            y.Value.Should().Be(10);
        }

        [Test]
        public void TestCanDispatchNestedExpressions()
        {
            var add = Fun.Wrap((int x, int y) => x + y);
            var mul = Fun.Wrap((int x, int y) => x * y);
            var frame = new ActivationFrame();
            var sut = new Dispatcher(frame);
            frame.Stack.PushConstantValue(3)
                .PushConstantValue(5)
                .PushFunction(add)
                .PushConstantValue(7)
                .PushFunction(mul);
            frame.Stack.Count.Should().Be(5);
            sut.Dispatch();
            frame.Stack.Count.Should().Be(1);
            Assert.That(frame.Stack, Has.Count.EqualTo(1));
            var x = frame.Stack.Pop();
            x.Should().BeOfType<ValueStackElement>();
            var y = x as ValueStackElement;
            y.Should().NotBeNull();
            y.Value.Should().Be(56);
        }

        [Test]
        public void TestCanDispatchStrings()
        {
            var add = Fun.Wrap((string x, string y) => $"{x} {y}");
            var frame = new ActivationFrame();
            var sut = new Dispatcher(frame);
            frame.Stack.PushConstantValue("hello")
                .PushConstantValue("world")
                .PushFunction(add);
            frame.Stack.Count.Should().Be(3);
            sut.Dispatch();
            frame.Stack.Count.Should().Be(1);
            var x = frame.Stack.Pop();
            x.Should().BeOfType<ValueStackElement>();
            var y = x as ValueStackElement;
            y.Should().NotBeNull();
            y.Value.Should().Be("hello world");
        }
    }
}
#pragma warning restore IDE0058 // Expression value is never used
