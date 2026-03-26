using ast;
using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 21: Lowers TriG literal expressions to Store.LoadFromTriG() calls.
/// </summary>
public class TriGLiteralLoweringPhase : ICompilerPhase
{
    public string Name => "TriGLiteralLowering";
    public IReadOnlyList<string> DependsOn => new[] { "SparqlLowered" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "TriGLowered" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var rewriter = new TriGLiteralLoweringRewriter();
        var result = rewriter.Rewrite(ast);
        ast = result.Node;
        return PhaseResult.Ok(ast);
    }
}
