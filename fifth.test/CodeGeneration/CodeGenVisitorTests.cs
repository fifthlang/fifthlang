namespace Fifth.Test.CodeGeneration;

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AST;
using Fifth.CodeGeneration.LangProcessingPhases;

[TestFixture]
[Category("CIL")]
[Category("Slow")]
public class CodeGenVisitorTests
{
    [Test]
    public void CopesWithClassDefinition()
    {
        using var f = TestUtilities.LoadTestResource("Fifth.Test.TestSampleCode.class-definition.5th");
        if (FifthParserManager.TryParseFile<FifthProgram>(f.Path, out var ast, out var errors))
        {
            var sb = new StringBuilder();
            var sut = new CodeGenVisitor(new StringWriter(sb));
            sut.VisitFifthProgram(ast);
            var generatedCode = sb.ToString();
            generatedCode.Should().NotBeNullOrWhiteSpace();
            Console.WriteLine(generatedCode);
        }
    }

    [Test]
    public void CopesWithOverloading()
    {
        using var f = TestUtilities.LoadTestResource("Fifth.Test.TestSampleCode.overloading.5th");
        if (FifthParserManager.TryParseFile<FifthProgram>(f.Path, out var ast, out var errors))
        {
            var sb = new StringBuilder();
            var sut = new CodeGenVisitor(new StringWriter(sb));
            sut.VisitFifthProgram(ast);
            var generatedCode = sb.ToString();
            generatedCode.Should().NotBeNullOrWhiteSpace();
            Console.WriteLine(generatedCode);
        }
    }

    [Test]
    public void CopesWithPropertyAccess()
    {
        using var f = TestUtilities.LoadTestResource("Fifth.Test.TestSampleCode.property-access.5th");
        if (FifthParserManager.TryParseFile<FifthProgram>(f.Path, out var ast, out var errors))
        {
            var sb = new StringBuilder();
            var sut = new CodeGenVisitor(new StringWriter(sb));
            sut.VisitFifthProgram(ast);
            var generatedCode = sb.ToString();
            generatedCode.Should().NotBeNullOrWhiteSpace();
            Console.WriteLine(generatedCode);
        }
    }

    [TestCaseSource(typeof(CodeGenVisitorTests), nameof(TypeArithmeticTestCases))]
    public async Task<string> TestArithmeticOperationsWorkForEachType(string numberType, string operatorSymbol, string leftNumber,
        string rightNumber)
    {
        var prog = $@"
main():int{{
    print(sum());
    return 0;
}}

sum(): {numberType}{{
    a: {numberType} = {leftNumber};
    b: {numberType} = {rightNumber};
    return a {operatorSymbol} b;
}}";
        var outputs = await TestUtilities.BuildRunAndTestProgramInString(prog);
        return outputs.FirstOrDefault();
    }

    [TestCaseSource(typeof(CodeGenVisitorTests), nameof(DestructuringTestCases))]
    [Category("WIP")]
    public async Task<string> TestDestructuringCases(string resourceName)
    {
        var outputs = await TestUtilities.BuildRunAndTestProgramInResource(resourceName);
        return outputs.FirstOrDefault();
    }

    [TestCaseSource(typeof(CodeGenVisitorTests), nameof(StringFormattingTestCases))]
    public async Task<string> TestStringFormattingWorksThroughPrint(string format, string input)
    {
        var prog = $@"main():int{{ print(""{format}"", {input}); return 0; }}";
        var outputs = await TestUtilities.BuildRunAndTestProgramInString(prog);
        return outputs.FirstOrDefault();
    }

    #region Test Cases

    public static IEnumerable DestructuringTestCases
    {
        get
        {
            yield return new TestCaseData("Fifth.Test.TestSampleCode.destructuring.5th").Returns("26.84635829149776");
        }
    }

    public static IEnumerable StringFormattingTestCases
    {
        get
        {
            yield return new TestCaseData("{0:c}", "12345.67").Returns("£12,345.67");
        }
    }

    public static IEnumerable TypeArithmeticTestCases
    {
        get
        {
            #region short

            yield return new TestCaseData("short", "+", "0", "0").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("short", "+", "0", "1").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("short", "+", "1", "1").Returns("2" + Environment.NewLine);
            yield return new TestCaseData("short", "+", "1", "0").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("short", "+", "0", "-1").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("short", "+", "1", "-1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("short", "+", "-1", "0").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("short", "-", "0", "0").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("short", "-", "0", "1").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("short", "-", "1", "1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("short", "-", "1", "0").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("short", "-", "0", "-1").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("short", "-", "1", "-1").Returns("2" + Environment.NewLine);
            yield return new TestCaseData("short", "-", "-1", "0").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("short", "*", "0", "0").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("short", "*", "0", "1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("short", "*", "1", "1").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("short", "*", "1", "0").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("short", "*", "0", "-1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("short", "*", "1", "-1").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("short", "*", "-1", "0").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("short", "/", "0", "1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("short", "/", "1", "1").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("short", "/", "0", "-1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("short", "/", "1", "-1").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("short", "+", "1024", "1024").Returns("2048" + Environment.NewLine);
            yield return new TestCaseData("short", "+", "5", "6").Returns("11" + Environment.NewLine);

            #endregion short

            #region int

            yield return new TestCaseData("int", "+", "0", "0").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("int", "+", "0", "1").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("int", "+", "1", "1").Returns("2" + Environment.NewLine);
            yield return new TestCaseData("int", "+", "1", "0").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("int", "+", "0", "-1").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("int", "+", "1", "-1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("int", "+", "-1", "0").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("int", "-", "0", "0").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("int", "-", "0", "1").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("int", "-", "1", "1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("int", "-", "1", "0").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("int", "-", "0", "-1").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("int", "-", "1", "-1").Returns("2" + Environment.NewLine);
            yield return new TestCaseData("int", "-", "-1", "0").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("int", "*", "0", "0").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("int", "*", "0", "1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("int", "*", "1", "1").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("int", "*", "1", "0").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("int", "*", "0", "-1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("int", "*", "1", "-1").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("int", "*", "-1", "0").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("int", "/", "0", "1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("int", "/", "1", "1").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("int", "/", "0", "-1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("int", "/", "1", "-1").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("int", "+", "1024", "1024").Returns("2048" + Environment.NewLine);
            yield return new TestCaseData("int", "+", "5", "6").Returns("11" + Environment.NewLine);

            #endregion int

            #region long

            yield return new TestCaseData("long", "+", "0", "0").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("long", "+", "0", "1").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("long", "+", "1", "1").Returns("2" + Environment.NewLine);
            yield return new TestCaseData("long", "+", "1", "0").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("long", "+", "0", "-1").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("long", "+", "1", "-1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("long", "+", "-1", "0").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("long", "-", "0", "0").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("long", "-", "0", "1").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("long", "-", "1", "1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("long", "-", "1", "0").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("long", "-", "0", "-1").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("long", "-", "1", "-1").Returns("2" + Environment.NewLine);
            yield return new TestCaseData("long", "-", "-1", "0").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("long", "*", "0", "0").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("long", "*", "0", "1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("long", "*", "1", "1").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("long", "*", "1", "0").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("long", "*", "0", "-1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("long", "*", "1", "-1").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("long", "*", "-1", "0").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("long", "/", "0", "1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("long", "/", "1", "1").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("long", "/", "0", "-1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("long", "/", "1", "-1").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("long", "+", "1024", "1024").Returns("2048" + Environment.NewLine);
            yield return new TestCaseData("long", "+", "5", "6").Returns("11" + Environment.NewLine);

            #endregion long

            #region float

            yield return new TestCaseData("float", "+", "0.1", "0.1").Returns("0.2" + Environment.NewLine);
            yield return new TestCaseData("float", "+", "0.1", "1.1").Returns("1.2" + Environment.NewLine);
            yield return new TestCaseData("float", "+", "1.1", "1.1").Returns("2.2" + Environment.NewLine);
            yield return new TestCaseData("float", "+", "1.1", "0.1").Returns("1.2" + Environment.NewLine);
            yield return new TestCaseData("float", "+", "0.1", "-1.1").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("float", "+", "1.1", "-1.1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("float", "+", "-1.1", "0.1").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("float", "-", "0.1", "0.1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("float", "-", "0.1", "1.1").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("float", "-", "1.1", "1.1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("float", "-", "1.1", "0.1").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("float", "-", "0.1", "-1.1").Returns("1.2" + Environment.NewLine);
            yield return new TestCaseData("float", "-", "1.1", "-1.1").Returns("2.2" + Environment.NewLine);
            yield return new TestCaseData("float", "-", "-1.1", "0.1").Returns("-1.2" + Environment.NewLine);
            yield return new TestCaseData("float", "*", "0.1", "0.1").Returns("0.01" + Environment.NewLine);
            yield return new TestCaseData("float", "*", "0.1", "1.1").Returns("0.11" + Environment.NewLine);
            yield return new TestCaseData("float", "*", "1.1", "1.1").Returns("1.21" + Environment.NewLine);
            yield return new TestCaseData("float", "*", "1.1", "0.1").Returns("0.11" + Environment.NewLine);
            yield return new TestCaseData("float", "*", "0.1", "-1.1").Returns("-0.11" + Environment.NewLine);
            yield return new TestCaseData("float", "*", "1.1", "-1.1").Returns("-1.21" + Environment.NewLine);
            yield return new TestCaseData("float", "*", "-1.1", "0.1").Returns("-0.11" + Environment.NewLine);
            yield return new TestCaseData("float", "/", "0.1", "1.1").Returns("0.09090909" + Environment.NewLine);
            yield return new TestCaseData("float", "/", "1.1", "1.1").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("float", "/", "0.1", "-1.1").Returns("-0.09090909" + Environment.NewLine);
            yield return new TestCaseData("float", "/", "1.1", "-1.1").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("float", "+", "1024.1", "1024.1").Returns("2048.2" + Environment.NewLine);
            yield return new TestCaseData("float", "+", "5.1", "6.1").Returns("11.2" + Environment.NewLine);

            #endregion float

            #region double

            yield return new TestCaseData("double", "+", "0.1", "0.1").Returns("0.200000002980232" + Environment.NewLine);
            yield return new TestCaseData("double", "+", "0.1", "1.1").Returns("1.20000002533197" + Environment.NewLine);
            yield return new TestCaseData("double", "+", "1.1", "1.1").Returns("2.20000004768372" + Environment.NewLine);
            yield return new TestCaseData("double", "+", "1.1", "0.1").Returns("1.20000002533197" + Environment.NewLine);
            yield return new TestCaseData("double", "+", "0.1", "-1.1").Returns("-1.00000002235174" + Environment.NewLine);
            yield return new TestCaseData("double", "+", "1.1", "-1.1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("double", "+", "-1.1", "0.1").Returns("-1.00000002235174" + Environment.NewLine);
            yield return new TestCaseData("double", "-", "0.1", "0.1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("double", "-", "0.1", "1.1").Returns("-1.00000002235174" + Environment.NewLine);
            yield return new TestCaseData("double", "-", "1.1", "1.1").Returns("0" + Environment.NewLine);
            yield return new TestCaseData("double", "-", "1.1", "0.1").Returns("1.00000002235174" + Environment.NewLine);
            yield return new TestCaseData("double", "-", "0.1", "-1.1").Returns("1.20000002533197" + Environment.NewLine);
            yield return new TestCaseData("double", "-", "1.1", "-1.1").Returns("2.20000004768372" + Environment.NewLine);
            yield return new TestCaseData("double", "-", "-1.1", "0.1").Returns("-1.20000002533197" + Environment.NewLine);
            yield return new TestCaseData("double", "*", "0.1", "0.1").Returns("0.0100000002980232" + Environment.NewLine);
            yield return new TestCaseData("double", "*", "0.1", "1.1").Returns("0.110000004023314" + Environment.NewLine);
            yield return new TestCaseData("double", "*", "1.1", "1.1").Returns("1.21000005245209" + Environment.NewLine);
            yield return new TestCaseData("double", "*", "1.1", "0.1").Returns("0.110000004023314" + Environment.NewLine);
            yield return new TestCaseData("double", "*", "0.1", "-1.1").Returns("-0.110000004023314" + Environment.NewLine);
            yield return new TestCaseData("double", "*", "1.1", "-1.1").Returns("-1.21000005245209" + Environment.NewLine);
            yield return new TestCaseData("double", "*", "-1.1", "0.1").Returns("-0.110000004023314" + Environment.NewLine);
            yield return new TestCaseData("double", "/", "0.1", "1.1").Returns("0.0909090902933405" + Environment.NewLine);
            yield return new TestCaseData("double", "/", "1.1", "1.1").Returns("1" + Environment.NewLine);
            yield return new TestCaseData("double", "/", "0.1", "-1.1").Returns("-0.0909090902933405" + Environment.NewLine);
            yield return new TestCaseData("double", "/", "1.1", "-1.1").Returns("-1" + Environment.NewLine);
            yield return new TestCaseData("double", "+", "1024.1", "1024.1").Returns("2048.19995117188" + Environment.NewLine);
            yield return new TestCaseData("double", "+", "5.1", "6.1").Returns("11.1999998092651" + Environment.NewLine);

            #endregion double
        }
    }

    #endregion Test Cases
}
