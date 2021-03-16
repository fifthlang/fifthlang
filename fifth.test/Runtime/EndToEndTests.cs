#pragma warning disable IDE0058 // Expression value is never used
namespace Fifth.Test.Runtime
{
    using Fifth.Runtime;
    using FluentAssertions;
    using NUnit.Framework;
    using Tests;

    [TestFixture(Category = "End to End")]
    internal class EndToEndTests : ParserTestBase
    {
        [TestCase("void main() => write('hello world');", 0)]
        [TestCase("int main() => greet('world'), 0; void greet(string s) => write('hello, ' + s);", 0)]
        [TestCase("int main() => greet('world'), 1+2; void greet(string s) => write('hello, ' + s);", 3)]
        [TestCase("int main() => doSomeCalculation(13, 17); int doSomeCalculation(int x, int y) => x + y;", 30)]
        [TestCase(
            "int main() => int a = doSomeCalculation(13, 17), a + 10; int doSomeCalculation(int x, int y) => x + y;",
            40)]
        [TestCase("int main() => int a = doSomeCalculation(13, 17), int b = a + 10, b + 11; " +
                  "int doSomeCalculation(int x, int y) => x + y;", 51)]
        [TestCase("int main(int a, int b) => a+b;", 11, 5, 6)]
        public void TestProgram(string fragment, int expectedResult, params object[] args)
        {
            var runtime = new FifthRuntime();
            var result = runtime.Execute(fragment, args);
            result.Should().Be(expectedResult);
        }
    }
}
