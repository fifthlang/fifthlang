using ast;
using ast_generated;
using ast_model.TypeSystem;
using Fifth.LangProcessingPhases;
using FluentAssertions;

namespace ast_tests;

public class GenericTypeAnnotationVisitorTests
{
    private readonly TypeAnnotationContext _context = new();
    private GenericTypeAnnotationVisitor CreateVisitor() => new(_context);

    [Fact]
    public void VisitFuncCallExp_WithExplicitTypeArgument_ShouldSetInferredType()
    {
        var visitor = CreateVisitor();
        var tParam = new TypeParameterDefBuilder().WithName(TypeParameterName.From("T")).WithConstraints([]).Build();
        var genericReturnType = new FifthType.TType { Name = TypeName.From("T") };
        var funcDef = new FunctionDefBuilder()
            .WithName(MemberName.From("identity"))
            .WithTypeParameters([tParam])
            .WithParams([])
            .WithBody(new BlockStatement { Statements = [] })
            .WithReturnType(genericReturnType)
            .WithIsStatic(false)
            .WithIsConstructor(false)
            .Build();

        var explicitTypeArg = new FifthType.TDotnetType(typeof(int)) { Name = TypeName.From("int") };
        var result = visitor.VisitFuncCallExp(new FuncCallExp
        {
            FunctionDef = funcDef,
            InvocationArguments = [],
            TypeArguments = [explicitTypeArg]
        });

        result.Type.Should().Be(explicitTypeArg);
    }
}
