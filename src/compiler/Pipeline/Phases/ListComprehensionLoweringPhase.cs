using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 34: Lowers list comprehensions to imperative loops
/// with list allocation and append.
/// </summary>
public class ListComprehensionLoweringPhase : ICompilerPhase
{
    public string Name => "ListComprehensionLowering";
    public IReadOnlyList<string> DependsOn => new[] { "Types" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "ComprehensionLowered" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var rewriter = new ListComprehensionLoweringRewriter();
        var result = rewriter.Rewrite(ast);
        ast = result.Node;
        return PhaseResult.Ok(ast);
    }
}
