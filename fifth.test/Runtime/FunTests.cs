#pragma warning disable IDE0058 // Expression value is never used
namespace Fifth.Tests
{
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    [Category("Functions")]
    [Category("Metadata")]
    [Category("Helper Code")]
    public class FunTests
    {
        [TestCase(5)]
        [TestCase(-5)]
        [TestCase(5.0)]
        [TestCase(-5.0)]
        [TestCase(5.0D)]
        [TestCase(-5.0D)]
        [TestCase("hello")]
        [TestCase('h')]
        [TestCase(true)]
        public void AsFunTest(object x)
        {
            var sut = x.AsFun();
            sut.Should().NotBeNull();
            sut.Should().BeOfType<FuncWrapper>();
        }

        [TestCase(5)]
        [TestCase(-5)]
        [TestCase(5.0)]
        [TestCase(-5.0)]
        [TestCase(5.0D)]
        [TestCase(-5.0D)]
        [TestCase("hello")]
        [TestCase('h')]
        [TestCase(true)]
        public void WrapTest(object x)
        {
            var sut = Fun.Wrap(() => x);
            sut.Should().NotBeNull();
            sut.Should().BeOfType<FuncWrapper>();
        }

        [Test]
        public void WrapTest1()
        {
            var sut = Fun.Wrap((int a) => a);
            sut.Should().NotBeNull();
            sut.Should().BeOfType<FuncWrapper>();
        }

        [Test]
        public void WrapTest2()
        {
            var sut = Fun.Wrap((string s, int x) => string.Format(s, x));
            sut.Should().NotBeNull();
            sut.Should().BeOfType<FuncWrapper>();
        }

        [Test]
        public void WrapTest3()
        {
            var sut = Fun.Wrap((string s, int x, bool y) => string.Format(s, y ? x : 0));
            sut.Should().NotBeNull();
            sut.Should().BeOfType<FuncWrapper>();
        }

        [Test]
        public void WrapTest4()
        {
            var sut = Fun.Wrap((string s, int x, bool y, decimal z) => string.Format(s, y ? x : z));
            sut.Should().NotBeNull();
            sut.Should().BeOfType<FuncWrapper>();
        }

        [Test]
        public void WrapTest5()
        {
            var sut = Fun.Wrap((string s, int x, bool y, decimal z, char c) => string.Format(s + c, y ? x : z));
            sut.Should().NotBeNull();
            sut.Should().BeOfType<FuncWrapper>();
        }
    }
}
#pragma warning restore IDE0058 // Expression value is never used
