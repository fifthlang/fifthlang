using System.Linq;
using FluentAssertions;
using ast;
using compiler;

namespace ast_tests;

public class AliasScopeTests
{
    [Fact]
    public void Class_With_Identifier_AliasScope_Parses_And_Is_Recorded()
    {
        var code = """
            class Person in x {
                Age: int;
            }
            """;

        var ast = FifthParserManager.ParseString(code);
        ast.Should().NotBeNull();

        var pipeline = compiler.Pipeline.TransformationPipeline.CreateDefault();
        var result = pipeline.Execute(ast, compiler.Pipeline.PipelineOptions.Default);
        var processed = result.TransformedAst;
        processed.Should().NotBeNull();

        var assembly = processed as AssemblyDef;
        assembly.Should().NotBeNull();

        var person = assembly!.Modules.SelectMany(m => m.Classes).FirstOrDefault(c => c.Name.Value == "Person");
        person.Should().NotBeNull();
        person!.AliasScope.Should().Be("x");
    }
}
