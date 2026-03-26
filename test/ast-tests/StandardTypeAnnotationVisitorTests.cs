using ast;
using ast_model.TypeSystem;
using Fifth.LangProcessingPhases;
using FluentAssertions;

namespace ast_tests;

public class StandardTypeAnnotationVisitorTests
{
    private readonly TypeAnnotationContext _context = new();
    private StandardTypeAnnotationVisitor CreateVisitor() => new(_context);

    [Fact]
    public void VisitInt32LiteralExp_ShouldSetIntType()
    {
        var visitor = CreateVisitor();
        var result = visitor.VisitInt32LiteralExp(new Int32LiteralExp { Value = 42 });

        result.Type.Should().BeOfType<FifthType.TDotnetType>();
        ((FifthType.TDotnetType)result.Type).TheType.Should().Be(typeof(int));
    }

    [Fact]
    public void VisitBinaryExp_IntArithmetic_ShouldReturnIntType()
    {
        var visitor = CreateVisitor();
        var left = new Int32LiteralExp { Value = 5 };
        var right = new Int32LiteralExp { Value = 3 };

        var result = visitor.VisitBinaryExp(new BinaryExp { LHS = left, Operator = Operator.ArithmeticAdd, RHS = right });

        result.Type.Should().BeOfType<FifthType.TDotnetType>();
        ((FifthType.TDotnetType)result.Type).TheType.Should().Be(typeof(int));
    }
}
