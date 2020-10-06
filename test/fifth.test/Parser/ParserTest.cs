using FluentAssertions;
using NUnit.Framework;
using System.IO;
using System.Text;

namespace fifth.Parser.Tests
{
    [TestFixture()]
    public class ParserTests
    {
        private static string TestProgram => @"use std;
            main(int x, int y) => myprint(x + y);
            myprint(int x) => std.print(""the answer is "" + x);";

        [Test]
        public void TestCanParseFullProgram()
        {
            string program = TestProgram;
            var parser = ParseProgram(program);
            var ast = parser.astBuilder.Build();
            ast.Should().NotBeNull();
            ast.FunctionDefinitions.Count.Should().Be(2);
        }

        private static Deleteme.Parser ParseProgram(string program)
        {
            Deleteme.Parser parser;
            // convert string to stream
            byte[] byteArray = Encoding.UTF8.GetBytes(program);
            //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
            MemoryStream stream = new MemoryStream(byteArray);
            Deleteme.Scanner scanner = new Deleteme.Scanner(stream);
            parser = new Deleteme.Parser(scanner);
            parser.Parse();
            return parser;
        }
    }
}