using ast;
using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 23: Emits diagnostics for triple literal constructs.
/// </summary>
public class TripleDiagnosticsPhase : ICompilerPhase
{
    public string Name => "TripleDiagnostics";
    public IReadOnlyList<string> DependsOn => new[] { "TreeRelinked" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "TripleDiags" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var result = new TripleDiagnosticsVisitor(context.Diagnostics).Visit(ast);
        return PhaseResult.Ok(result);
    }
}
