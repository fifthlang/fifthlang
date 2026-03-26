using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 38: Applies tail-call optimization to eligible recursive functions.
/// Currently disabled/commented out in the monolithic pipeline due to AST construction issues.
/// Registered in the default pipeline but skipped via PipelineOptions.
/// </summary>
public class TailCallOptimizationPhase : ICompilerPhase
{
    public string Name => "TailCallOptimization";
    public IReadOnlyList<string> DependsOn => new[] { "Defunctionalised" };
    public IReadOnlyList<string> ProvidedCapabilities => Array.Empty<string>();

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        ast = new TailCallOptimizationRewriter().Visit(ast);
        // Re-link after transformation to maintain consistent parent pointers
        ast = new TreeLinkageVisitor().Visit(ast);
        return PhaseResult.Ok(ast);
    }
}
