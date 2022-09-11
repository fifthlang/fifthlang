namespace Fifth.Test.CodeGeneration;
using System.Linq;

[TestFixture]
[Category("CIL")]
[Category("Slow")]
public class EndToEndDestructuringTests
{
    [Test]
    [Category("WIP")]
    public void TestDestructuringCases()
    {
        var outputs = TestUtilities.BuildRunAndTestProgramInResource("Fifth.Test.TestSampleCode.destructuring.5th").Result;
        outputs.First().Should().Be("26.84635829149776");
    }

    [Test]
    [Category("WIP")]
    public void TestRecursiveDestructuringCases()
    {
        var outputs = TestUtilities.BuildRunAndTestProgramInResource("Fifth.Test.TestSampleCode.recursive-destructuring.5th").Result;
        outputs.First().Should().Be("26.84635829149776");
    }

}
