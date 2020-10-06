using NUnit.Framework;
using fifth;
using System;
using System.Collections.Generic;
using FluentAssertions;

namespace fifth.Tests
{
    [TestFixture()]
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
            FuncWrapper sut = x.AsFun();
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
            FuncWrapper sut = Fun.Wrap(() => x);
            sut.Should().NotBeNull();
            sut.Should().BeOfType<FuncWrapper>();
        }

        [Test()]
        public void WrapTest1()
        {
            FuncWrapper sut = Fun.Wrap((int a) => a);
            sut.Should().NotBeNull();
            sut.Should().BeOfType<FuncWrapper>();
        }

        [Test()]
        public void WrapTest2()
        {
            FuncWrapper sut = Fun.Wrap((string s, int x) => String.Format(s, x));
            sut.Should().NotBeNull();
            sut.Should().BeOfType<FuncWrapper>();
        }

        [Test()]
        public void WrapTest3()
        {
            FuncWrapper sut = Fun.Wrap((string s, int x, bool y) => String.Format(s, y ? x : 0));
            sut.Should().NotBeNull();
            sut.Should().BeOfType<FuncWrapper>();
        }

        [Test()]
        public void WrapTest4()
        {
            FuncWrapper sut = Fun.Wrap((string s, int x, bool y, decimal z) => String.Format(s, y ? x : z));
            sut.Should().NotBeNull();
            sut.Should().BeOfType<FuncWrapper>();
        }

        [Test()]
        public void WrapTest5()
        {
            FuncWrapper sut = Fun.Wrap((string s, int x, bool y, decimal z, char c) => String.Format(s + c, y ? x : z));
            sut.Should().NotBeNull();
            sut.Should().BeOfType<FuncWrapper>();
        }
    }
}