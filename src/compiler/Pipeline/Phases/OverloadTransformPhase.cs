using ast;
using Fifth.LangProcessingPhases;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 16: Transforms multi-clause overloaded functions into simpler form.
/// </summary>
public class OverloadTransformPhase : ICompilerPhase
{
    public string Name => "OverloadTransform";
    public IReadOnlyList<string> DependsOn => new[] { "GuardChecks" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "OverloadTransforms" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var result = new OverloadTransformingVisitor().Visit(ast);
        return PhaseResult.Ok(result);
    }
}
