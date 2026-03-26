using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 31: Type checks query application expressions (query &lt;- store)
/// after type annotation. Emits diagnostics for type mismatches.
/// </summary>
public class QueryApplicationTypeCheckPhase : ICompilerPhase
{
    public string Name => "QueryApplicationTypeCheck";
    public IReadOnlyList<string> DependsOn => new[] { "Types" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "QueryTypes" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var rewriter = new QueryApplicationTypeCheckRewriter(context.Diagnostics);
        var result = rewriter.Rewrite(ast);
        ast = result.Node;

        if (context.Diagnostics.Any(d => d.Level == DiagnosticLevel.Error))
            return PhaseResult.Fail(ast, context.Diagnostics.ToList());

        return PhaseResult.Ok(ast);
    }
}
