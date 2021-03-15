namespace Fifth.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture, Category("Functions"), Category("Helper Code")]
    public class FuncWrapperTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCanInvokeWrappedFunction()
        {
            var f5 = 5.AsFun();
            Assert.That(f5.Invoke(), Is.EqualTo(5));
        }

        [Test]
        public void TestCanInvokeWrappedFunctionTaking2Params()
        {
            var add = Fun.Wrap((int x, int y) => x + y);
            Assert.That(add.Invoke(1, 2), Is.EqualTo(3));
        }

        [Test]
        public void TestFn0()
        {
            static int f1() => 1;
            var sut = Fun.Wrap(f1);
            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.ResultType, Is.EqualTo(typeof(int)));
        }

        [Test]
        public void TestFn1()
        {
            static int f2(int x) => x + 1;
            var sut = Fun.Wrap((Func<int, int>)f2);
            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.ResultType, Is.EqualTo(typeof(int)));
            Assert.That(sut.ArgTypes.Count, Is.EqualTo(1));
            Assert.That(sut.ArgTypes[0], Is.EqualTo(typeof(int)));
        }

        [Test]
        public void TestFn2()
        {
            static float f3(int x, float y) => x + y;
            var sut = Fun.Wrap((Func<int, float, float>)f3);
            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.ResultType, Is.EqualTo(typeof(float)));
            Assert.That(sut.ArgTypes.Count, Is.EqualTo(2));
            Assert.That(sut.ArgTypes[0], Is.EqualTo(typeof(int)));
            Assert.That(sut.ArgTypes[1], Is.EqualTo(typeof(float)));
        }
    }
}
