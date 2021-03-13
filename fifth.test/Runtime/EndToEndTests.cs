namespace Fifth.Test.Runtime
{
    using System.Diagnostics.CodeAnalysis;
    using Fifth.Runtime;
    using FluentAssertions;
    using NUnit.Framework;
    using Tests;

    [TestFixture]
    [SuppressMessage("Style", "IDE0022:Use expression body for methods", Justification = "<Pending>")]
    internal class EndToEndTests : ParserTestBase
    {
        [Test]
        public void TestProgram()
        {
            var fragment = "main() => write('hello world');";
            var runtime = new FifthRuntime();
            var result = runtime.Execute(fragment);
            result.Should().Be(0);
        }
    }
}
