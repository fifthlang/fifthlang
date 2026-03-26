using ast;
using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 14: Gathers overloaded function definitions into overload groups.
/// </summary>
public class OverloadGatheringPhase : ICompilerPhase
{
    public string Name => "OverloadGathering";
    public IReadOnlyList<string> DependsOn => new[] { "Destructuring" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "OverloadGroups" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var result = new OverloadGatheringVisitor().Visit(ast);
        return PhaseResult.Ok(result);
    }
}
