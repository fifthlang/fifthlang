using ast;

namespace Fifth.LangProcessingPhases;

public class KnowledgeGraphTypeAnnotationVisitor(TypeAnnotationContext context) : GenericTypeAnnotationVisitor(context)
{
    public override SparqlLiteralExpression VisitSparqlLiteralExpression(SparqlLiteralExpression ctx)
    {
        var result = base.VisitSparqlLiteralExpression(ctx);
        var queryType = new FifthType.TType { Name = TypeName.From("Query") };
        Context.OnTypeInferred(result, queryType);
        return result with { Type = queryType };
    }

    public override Interpolation VisitInterpolation(Interpolation ctx)
    {
        var result = base.VisitInterpolation(ctx);
        if (result.Expression?.Type != null)
        {
            return result with { ResultType = result.Expression.Type };
        }

        return result;
    }
}
