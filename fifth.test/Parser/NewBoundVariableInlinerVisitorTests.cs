namespace Fifth.Test.Parser;

using System.Linq;
using Antlr4.Runtime;
using Fifth.AST;
using Fifth.AST.Builders;
using Fifth.LangProcessingPhases;
using Fifth.Parser.LangProcessingPhases;
using NUnit.Framework;
using Tests;

[TestFixture, Category("Destructuring"), Category("Visitors")]
public class NewBoundVariableInlinerVisitorTests : ParserTestBase
{
    private readonly string sampleDefinition = @"
class Person {
    Name: string;
    Height: float;
    Age: float;
    Weight: float;
}

calculate_bmi(p: Person {
    age: Age | age > 60,
    height: Height ,
    weight: Weight
    }) : float {
    return weight / (height * height);
}";
    [Test, Category("WIP")]
    public void if_passed_a_destructuring_function_definition_will_transform_to_only_use_direct_variable_references()
    {
        var (sut, ctx) = DestructuringPatternFlattenerVisitor.CreateVisitor();
        var decl = CreateDestructuringParamDecl();
        var newdecl = sut.ProcessDestructuringParamDecl(decl, ctx);
    }

    private DestructuringParamDecl CreateDestructuringParamDecl()
    {
        var ast = ParseProgram() as FifthProgram;
        var calculate_bmi = ast.Functions.First(f => f.Name == "calculate_bmi");
        return calculate_bmi.ParameterDeclarations.ParameterDeclarations.First() as DestructuringParamDecl;
    }

    private IAstNode ParseProgram()
    {
        var ast = FifthParserManager.ParseProgramToAst(CharStreams.fromString(sampleDefinition));
        ast.Accept(new BuiltinInjectorVisitor());
        ast.Accept(new VerticalLinkageVisitor());
        return ast;
    }
}
