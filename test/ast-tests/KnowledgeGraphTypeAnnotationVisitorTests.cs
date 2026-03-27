using ast;
using ast_model.TypeSystem;
using Fifth.LangProcessingPhases;
using FluentAssertions;

namespace ast_tests;

public class KnowledgeGraphTypeAnnotationVisitorTests
{
    private readonly TypeAnnotationContext _context = new();
    private KnowledgeGraphTypeAnnotationRewriter CreateVisitor() => new(_context);

    [Fact]
    public void VisitSparqlLiteralExpression_ShouldSetQueryType()
    {
        var visitor = CreateVisitor();
        var result = (SparqlLiteralExpression)visitor.VisitSparqlLiteralExpression(new SparqlLiteralExpression
        {
            SparqlText = "SELECT * WHERE { ?s ?p ?o }",
            Bindings = [],
            Interpolations = []
        }).Node;

        result.Type.Should().BeOfType<FifthType.TType>();
        result.Type.Name.Value.Should().Be("Query");
    }

    [Fact]
    public void VisitInterpolation_ShouldSetResultTypeFromExpression()
    {
        var visitor = CreateVisitor();
        var interpolation = new Interpolation
        {
            Position = 0,
            Length = 4,
            Expression = new Int32LiteralExp
            {
                Value = 1,
                Type = new FifthType.TDotnetType(typeof(int)) { Name = TypeName.From("int") }
            }
        };

        var result = (Interpolation)visitor.VisitInterpolation(interpolation).Node;

        result.ResultType.Should().NotBeNull();
        result.ResultType.Should().BeOfType<FifthType.TDotnetType>();
    }
}
