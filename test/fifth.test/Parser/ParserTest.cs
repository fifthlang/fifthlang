using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using NUnit.Framework;
using static FifthParser;

namespace Fifth.Tests
{
    [TestFixture()]
    public class ParserTests : ParserTestBase
    {
        [Test]
        public void TestCanRecogniseAssignmentDecl(){
            var ctx = ParseDeclAssignment("int result  = 5;");
            Assert.That( ctx, Is.TypeOf<VarDeclStmtContext>());
        }
    }
}