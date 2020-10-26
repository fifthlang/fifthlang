using NUnit.Framework;

namespace Fifth.Tests
{
    public class FuncStackTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCanAddFunc()
        {
            FuncStack sut = new FuncStack();
            Assert.That(sut.Stack, Is.Empty);
            sut.Push(5.AsFun());
            Assert.That(sut.Stack, Is.Not.Empty);
        }

        [Test]
        public void TestCanAddMultipleFuncs()
        {
            FuncStack sut = new FuncStack();
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
            FuncStack sut = new FuncStack();
            Assert.That(sut.Stack, Is.Empty);
            sut.Push(f5);
            sut.Push(f5);
            sut.Push(add);
            Assert.That(sut.Stack, Has.Count.EqualTo(3));
        }
    }
}
