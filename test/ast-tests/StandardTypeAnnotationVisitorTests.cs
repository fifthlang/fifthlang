using ast;
using ast_model.TypeSystem;
using Fifth.LangProcessingPhases;
using FluentAssertions;

namespace ast_tests;

public class StandardTypeAnnotationVisitorTests
{
    private readonly TypeAnnotationContext _context = new();
    private StandardTypeAnnotationRewriter CreateVisitor() => new(_context);

    [Fact]
    public void VisitInt32LiteralExp_ShouldSetIntType()
    {
        var visitor = CreateVisitor();
        var result = (Int32LiteralExp)visitor.VisitInt32LiteralExp(new Int32LiteralExp { Value = 42 }).Node;

        result.Type.Should().BeOfType<FifthType.TDotnetType>();
        ((FifthType.TDotnetType)result.Type).TheType.Should().Be(typeof(int));
    }

    [Fact]
    public void VisitBinaryExp_IntArithmetic_ShouldReturnIntType()
    {
        var visitor = CreateVisitor();
        var left = new Int32LiteralExp { Value = 5 };
        var right = new Int32LiteralExp { Value = 3 };

        var result = (BinaryExp)visitor.VisitBinaryExp(new BinaryExp { LHS = left, Operator = Operator.ArithmeticAdd, RHS = right }).Node;

        result.Type.Should().BeOfType<FifthType.TDotnetType>();
        ((FifthType.TDotnetType)result.Type).TheType.Should().Be(typeof(int));
    }
}
