using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 32: Lowers query application expressions to
/// QueryApplicationExecutor.Execute() calls.
/// </summary>
public class QueryApplicationLoweringPhase : ICompilerPhase
{
    public string Name => "QueryApplicationLowering";
    public IReadOnlyList<string> DependsOn => new[] { "QueryTypes" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "QueryLowered" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var rewriter = new QueryApplicationLoweringRewriter();
        var result = rewriter.Rewrite(ast);
        ast = result.Node;
        return PhaseResult.Ok(ast);
    }
}
