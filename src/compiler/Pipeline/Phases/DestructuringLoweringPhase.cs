using ast;
using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 17: Compound phase that resolves property references in destructuring
/// patterns then lowers them to variable declarations.
/// Runs DestructuringVisitor (property resolution) then DestructuringLoweringRewriter.
/// </summary>
public class DestructuringLoweringPhase : ICompilerPhase
{
    public string Name => "DestructuringLowering";
    public IReadOnlyList<string> DependsOn => new[] { "OverloadTransforms" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "DestructuringLowered" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        ast = new DestructuringVisitor().Visit(ast);
        var rewriter = new DestructuringLoweringRewriter();
        var result = rewriter.Rewrite(ast);
        ast = result.Node;
        return PhaseResult.Ok(ast);
    }
}
