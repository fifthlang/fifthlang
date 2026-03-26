using ast;
using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 22: Mid-pipeline re-link of parent-child relationships in the AST.
/// Runs TreeLinkageVisitor to fix parent pointers after lowering passes.
/// </summary>
public class TreeRelinkPhase : ICompilerPhase
{
    public string Name => "TreeRelink";
    public IReadOnlyList<string> DependsOn => new[] { "TriGLowered" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "TreeRelinked" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var result = new TreeLinkageVisitor().Visit(ast);
        return PhaseResult.Ok(result);
    }
}
