namespace Fifth.Tests
{
    using static FifthParser;
    using NUnit.Framework;

    [TestFixture]
    [Category("Parser")]
    public class ParserTests : ParserTestBase
    {
        [Test]
        public void TestCanRecogniseAssignmentDecl()
        {
            var ctx = ParseDeclAssignment("int result  = 5;");
            Assert.That(ctx, Is.TypeOf<VarDeclStmtContext>());
        }
    }
}
