using ast;
using ast_generated;

namespace Fifth.LangProcessingPhases;

public class KnowledgeGraphTypeAnnotationRewriter(TypeAnnotationContext context) : TypeAnnotationRewriterStageBase(context)
{
    public override RewriteResult VisitSparqlLiteralExpression(SparqlLiteralExpression ctx)
    {
        var baseResult = base.VisitSparqlLiteralExpression(ctx);
        var result = (SparqlLiteralExpression)baseResult.Node;
        var queryType = new FifthType.TType { Name = TypeName.From("Query") };
        Context.OnTypeInferred(result, queryType);
        return new RewriteResult(result with { Type = queryType }, baseResult.Prologue);
    }

    public override RewriteResult VisitInterpolation(Interpolation ctx)
    {
        var baseResult = base.VisitInterpolation(ctx);
        var result = (Interpolation)baseResult.Node;
        if (result.Expression?.Type != null)
        {
            return new RewriteResult(result with { ResultType = result.Expression.Type }, baseResult.Prologue);
        }

        return baseResult;
    }
}
