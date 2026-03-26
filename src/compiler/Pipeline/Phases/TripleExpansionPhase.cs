using ast;
using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 24: Expands triple literal expressions.
/// </summary>
public class TripleExpansionPhase : ICompilerPhase
{
    public string Name => "TripleExpansion";
    public IReadOnlyList<string> DependsOn => new[] { "TripleDiags" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "TripleExpanded" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var result = new TripleExpansionVisitor(context.Diagnostics).Visit(ast);
        return PhaseResult.Ok(result);
    }
}
