namespace Fifth.Tests
{
    using NUnit.Framework;
    using static FifthParser;

    [TestFixture, Ignore("meh")]
    [Category("Parser")]
    public class ParserTests : ParserTestBase
    {
        [Test]
        public void TestCanRecogniseAssignmentDecl()
        {
            var ctx = ParseDeclAssignment("int result  = 5;");
            Assert.That(ctx, Is.TypeOf<Stmt_vardeclContext>());
        }
    }
}
