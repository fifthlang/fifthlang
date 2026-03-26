using ast;
using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 20: Lowers SPARQL literal expressions to Query.Parse() calls.
/// </summary>
public class SparqlLiteralLoweringPhase : ICompilerPhase
{
    public string Name => "SparqlLiteralLowering";
    public IReadOnlyList<string> DependsOn => new[] { "SparqlBindings" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "SparqlLowered" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var rewriter = new SparqlLiteralLoweringRewriter();
        var result = rewriter.Rewrite(ast);
        ast = result.Node;
        return PhaseResult.Ok(ast);
    }
}
