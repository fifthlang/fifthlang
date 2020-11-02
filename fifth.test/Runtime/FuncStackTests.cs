namespace Fifth.Tests
{
    using System.Collections.Generic;
    using Fifth.Runtime;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture()]
    public class FuncStackTests
    {
        public static IEnumerable<(string, object)> DeserialisationCases()
        {
            yield return ("'a'", (object)'a');
        }

        public static IEnumerable<(object, string)> SerialisationCases()
        {
            yield return ((object)'a', "'a'");
        }

        [Test()]
        public void FuncStackTest()
        {
            var sut = new FuncStack();
            sut.Should().NotBeNull();
            sut.Stack.Should().NotBeNull();
            sut.Stack.Should().BeEmpty();
        }

        [Test()]
        public void PopTest()
        {
            var sut = new FuncStack();
            sut.Should().NotBeNull();
            sut.Push(5.AsFun()); // assume push works
            sut.Pop();
            sut.Stack.Should().BeEmpty();
        }

        [Test()]
        public void PushTest()
        {
            var sut = new FuncStack();
            sut.Should().NotBeNull();
            sut.Stack.Should().NotBeNull();
            sut.Stack.Should().BeEmpty();
            sut.Push(5.AsFun());
            sut.Stack.Should().NotBeEmpty();
        }

        [Test]
        public void TestCanAddFunc()
        {
            var sut = new FuncStack();
            Assert.That(sut.Stack, Is.Empty);
            sut.Push(5.AsFun());
            Assert.That(sut.Stack, Is.Not.Empty);
        }

        [Test]
        public void TestCanAddMultipleFuncs()
        {
            var sut = new FuncStack();
            Assert.That(sut.Stack, Is.Empty);
            sut.Push(5.AsFun());
            sut.Push((-6).AsFun());
            Assert.That(sut.Stack, Has.Count.EqualTo(2));
        }

        [Test]
        public void TestCanAddSameFuncMultipleTimes()
        {
            var f5 = 5.AsFun();
            var add = Fun.Wrap((int x, int y) => x + y);
            var sut = new FuncStack();
            Assert.That(sut.Stack, Is.Empty);
            sut.Push(f5);
            sut.Push(f5);
            sut.Push(add);
            Assert.That(sut.Stack, Has.Count.EqualTo(3));
        }

        [TestCaseSource("DeserialisationCases"), Ignore("not sure this is worth pursuing")]
        public void TestCanDeserialiseValue((string serialisedStack, object resolvedValue) blah)
        {
            var sut = new FuncStackDeserialiser();
            var stack = sut.Deserialise(blah.serialisedStack);
            var dispatcher = new Dispatcher(stack);
            dispatcher.Dispatch();
            var actual = stack.Pop().Invoke();
            actual.Should().Be(blah.resolvedValue);
        }

        [TestCaseSource("SerialisationCases"), Ignore("not sure this is worth pursuing")]
        public void TestCanSerialiseValue((object value, string expectedSerialisation) blah)
        {
            var sut = new FuncStackSerialiser();
            var stack = new FuncStack();
            stack.Push(blah.value.AsFun());
            var actual = sut.Serialise(stack);
            actual.Should().Be(blah.expectedSerialisation);
        }
    }
}
