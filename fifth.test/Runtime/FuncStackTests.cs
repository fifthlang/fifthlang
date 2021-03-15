namespace Fifth.Tests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture, Category("Stack"), Category("Functions")]
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

        [Test]
        public void FuncStackTest()
        {
            var sut = new FuncStack();
            sut.Should().NotBeNull();
            sut.Stack.Should().NotBeNull();
            sut.Stack.Should().BeEmpty();
        }

        [Test]
        public void PopTest()
        {
            var sut = new FuncStack();
            sut.Should().NotBeNull();
            sut.Push(5.AsFun()); // assume push works
            sut.Pop();
            sut.Stack.Should().BeEmpty();
        }

        [Test]
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
    }
}
