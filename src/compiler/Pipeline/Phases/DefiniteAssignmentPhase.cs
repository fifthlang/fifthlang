using ast;
using compiler.SemanticAnalysis;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 8: Analyses definite assignment of required fields in constructors.
/// Merges analyzer diagnostics into the phase context.
/// </summary>
public class DefiniteAssignmentPhase : ICompilerPhase
{
    public string Name => "DefiniteAssignment";
    public IReadOnlyList<string> DependsOn => new[] { "ResolvedConstructors" };
    public IReadOnlyList<string> ProvidedCapabilities => Array.Empty<string>();

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var analyzer = new DefiniteAssignmentAnalyzer();
        ast = analyzer.Visit(ast);
        foreach (var diag in analyzer.Diagnostics)
        {
            context.Diagnostics.Add(diag);
        }
        return PhaseResult.Ok(ast);
    }
}
