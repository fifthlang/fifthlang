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
        [TestCase("string main() => write('hello world');", "hello world")]
        [TestCase("int main() => greet('world'), 0; string greet(string s) => write('hello, ' + s);", "0")]
        [TestCase("int main() => greet('world'), 1+2; string greet(string s) => write('hello, ' + s);", "3")]
        [TestCase("int main() => doSomeCalculation(13, 17); int doSomeCalculation(int x, int y) => x + y;", "30")]
        [TestCase("int main() => int a = doSomeCalculation(13, 17), a + 10; int doSomeCalculation(int x, int y) => x + y;", "40")]
        [TestCase("int main() => int a = doSomeCalculation(13, 17), int b = a + 10, b + 11; " +
                  "int doSomeCalculation(int x, int y) => x + y;", "51")]
        [TestCase("int main(int a, int b) => a+b;", "11", 5, 6)]
        [TestCase("double main(double a) => sqrt(a);", "8", 64D)]
        [TestCase("int main(int a, int b) => max(a,b); " +
                  "int max(int a, int b) => if(a >= b){a} else {b};", "6", 5, 6)]
        [TestCase("int main(int a, int b) => max(a,b); " +
                  "int max(int a, int b) => if(a > b){a} else {b};", "6", 5, 6)]
        [TestCase("int main(int a, int b) => max(a,b); " +
                  "int max(int a, int b) => if(a <= b){a} else {b};", "5", 5, 6)]
        [TestCase("int main(int a, int b) => max(a,b); " +
                  "int max(int a, int b) => if(a < b){a} else {b};", "5", 5, 6)]
        [TestCase("int main() => int a = 0, " +
                    "while (a < 10){a = a + 1}, a;", "10")]
        [TestCase("int main() => int[] mylist = [1,2,3,4,5], head(mylist);", "1")]
        public void TestProgram(string fragment, string expectedResult, params object[] args)
        {
            var runtime = new FifthRuntime();
            var result = runtime.Execute(fragment, args);
            result.Should().Be(expectedResult);
        }
    }
}
