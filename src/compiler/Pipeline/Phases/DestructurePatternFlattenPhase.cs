using ast;
using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 13: Compound phase that runs destructuring pattern resolution
/// followed by constraint propagation.
/// Runs DestructuringVisitor then DestructuringConstraintPropagator.
/// </summary>
public class DestructurePatternFlattenPhase : ICompilerPhase
{
    public string Name => "DestructurePatternFlatten";
    public IReadOnlyList<string> DependsOn => new[] { "FieldExpansion" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "Destructuring" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        ast = new DestructuringVisitor().Visit(ast);
        ast = new DestructuringConstraintPropagator().Visit(ast);
        return PhaseResult.Ok(ast);
    }
}
