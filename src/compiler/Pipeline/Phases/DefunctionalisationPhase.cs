using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 37: Compound phase that defunctionalises higher-order function types
/// into runtime closure interface types, then re-links the tree.
/// </summary>
public class DefunctionalisationPhase : ICompilerPhase
{
    public string Name => "Defunctionalisation";
    public IReadOnlyList<string> DependsOn => new[] { "ClosureConverted" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "Defunctionalised" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        // 1. Defunctionalise higher-order function types
        var rewriter = new DefunctionalisationRewriter();
        var result = rewriter.Rewrite(ast);
        ast = result.Node;

        // 2. Re-link after type rewrites to keep parent pointers consistent for codegen
        ast = new TreeLinkageVisitor().Visit(ast);

        return PhaseResult.Ok(ast);
    }
}
