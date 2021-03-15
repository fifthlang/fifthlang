namespace Fifth.Test.Runtime
{
    using System.Diagnostics.CodeAnalysis;
    using Fifth.Runtime;
    using FluentAssertions;
    using NUnit.Framework;
    using Tests;

    [TestFixture(Category = "WIP")]
    [SuppressMessage("Style", "IDE0022:Use expression body for methods", Justification = "<Pending>")]
    internal class EndToEndTests : ParserTestBase
    {
        [TestCase("void main() => write('hello world');", 0)]
        [TestCase("int main() => greet('world'), 0; void greet(string s) => write('hello, ' + s);", 0)]
        [TestCase("int main() => greet('world'), 1+2; void greet(string s) => write('hello, ' + s);", 3)]
        [TestCase("int main() => doSomeCalculation(13, 17); int doSomeCalculation(int x, int y) => x + y;", 30)]
        [TestCase("int main() => int a = doSomeCalculation(13, 17), a + 10; int doSomeCalculation(int x, int y) => x + y;", 40)]
        public void TestProgram(string fragment, int expectedResult)
        {
            var runtime = new FifthRuntime();
            var result = runtime.Execute(fragment);
            result.Should().Be(expectedResult);
        }
    }
}
