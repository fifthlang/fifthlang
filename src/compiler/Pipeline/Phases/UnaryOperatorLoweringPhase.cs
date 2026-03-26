using ast;
using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 18: Lowers unary operators (++, --, -, +) to binary expressions.
/// </summary>
public class UnaryOperatorLoweringPhase : ICompilerPhase
{
    public string Name => "UnaryOperatorLowering";
    public IReadOnlyList<string> DependsOn => new[] { "DestructuringLowered" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "UnaryLowered" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var rewriter = new UnaryOperatorLoweringRewriter();
        var result = rewriter.Rewrite(ast);
        ast = result.Node;
        return PhaseResult.Ok(ast);
    }
}
