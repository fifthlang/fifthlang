using FluentAssertions;
using NUnit.Framework;
using System;

namespace fifth.Tests
{
    [TestFixture()]
    public class FuncStackTests
    {
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
    }
}