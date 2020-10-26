using System;
using NUnit.Framework;

namespace Fifth.Tests
{
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
            Func<int> f1 = () => 1;
            var sut = Fun.Wrap(f1);
            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.ResultType, Is.EqualTo(typeof(int)));
        }

        [Test]
        public void TestFn1()
        {
            Func<int, int> f2 = (x) => x + 1;
            var sut = Fun.Wrap(f2);
            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.ResultType, Is.EqualTo(typeof(int)));
            Assert.That(sut.ArgTypes.Count, Is.EqualTo(1));
            Assert.That(sut.ArgTypes[0], Is.EqualTo(typeof(int)));
        }

        [Test]
        public void TestFn2()
        {
            Func<int, float, float> f3 = (x, y) => x + y;
            var sut = Fun.Wrap(f3);
            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.ResultType, Is.EqualTo(typeof(float)));
            Assert.That(sut.ArgTypes.Count, Is.EqualTo(2));
            Assert.That(sut.ArgTypes[0], Is.EqualTo(typeof(int)));
            Assert.That(sut.ArgTypes[1], Is.EqualTo(typeof(float)));
        }
    }
}
