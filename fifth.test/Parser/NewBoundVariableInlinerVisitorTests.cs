namespace Fifth.Test.Parser;

using System.Linq;
using Antlr4.Runtime;
using Fifth.AST;
using Fifth.LangProcessingPhases;
using Fifth.Parser.LangProcessingPhases;
using NUnit.Framework;
using Tests;

[TestFixture, Category("Destructuring"), Category("Visitors")]
public class NewBoundVariableInlinerVisitorTests : ParserTestBase
{
    private readonly string simpleDefinition = @"
class Person {
    Name: string;
    Height: float;
    Age: float;
    Weight: float;
}

calculate_bmi(p: Person {
    age: Age,
    height: Height,
    weight: Weight
    }) : float {
    return weight / (height * height);
}";
    private readonly string recursiveDefinition = @"
class Person {
    Name: string;
    Vitals: VitalStatistics;
}
class VitalStatistics {
    Height: float;
    Age: float;
    Weight: float;
}

calculate_bmi(p: Person {
    name: Name,
    vitals: Vitals{
        age: Age,
        height: Height,
        weight: Weight
        }
    }) : float {
    return weight / (height * height);
}";
    [Test]
    public void if_passed_a_destructuring_function_definition_will_transform_to_only_use_direct_variable_references()
    {
        var ast = ParseProgram(simpleDefinition) as FifthProgram;
        var calculate_bmi = ast.Functions.First(f => f.Name == "calculate_bmi");
        var visitor = new DestructuringPatternFlattenerVisitor();
        var newdecl = (FunctionDefinition)visitor.Process((FunctionDefinition)calculate_bmi, new DummyContext());
        newdecl.Should().NotBeNull();
        newdecl.Body.Statements.Should().HaveCount(4); // i.e. three desugared bindings turned into vardecls, plus the original statement
    }


    [Test]
    public void can_desugar_recursive_destrdecls()
    {
        var ast = ParseProgram(recursiveDefinition) as FifthProgram;
        var calculate_bmi = ast.Functions.First(f => f.Name == "calculate_bmi");
        var visitor = new DestructuringPatternFlattenerVisitor();
        var newdecl = (FunctionDefinition)visitor.Process((FunctionDefinition)calculate_bmi, new DummyContext());
        newdecl.Should().NotBeNull();
        newdecl.Body.Statements.Should().HaveCount(6);
    }


    private IAstNode ParseProgram(string prog)
    {
        var ast = FifthParserManager.ParseProgramToAst(CharStreams.fromString(prog));
        ast.Accept(new BuiltinInjectorVisitor());
        ast.Accept(new VerticalLinkageVisitor());
        ast.Accept(new SymbolTableBuilderVisitor());
        return ast;
    }
}
