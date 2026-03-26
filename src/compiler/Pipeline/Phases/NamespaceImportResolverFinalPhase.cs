using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 26: Re-runs namespace import resolution with null diagnostics (final pass).
/// This ensures imports are resolved against the final symbol table state.
/// </summary>
public class NamespaceImportResolverFinalPhase : ICompilerPhase
{
    public string Name => "NamespaceImportResolverFinal";
    public IReadOnlyList<string> DependsOn => new[] { "SymbolsFinal" };
    public IReadOnlyList<string> ProvidedCapabilities => Array.Empty<string>();

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var result = new NamespaceImportResolverVisitor(null).Visit(ast);
        return PhaseResult.Ok(result);
    }
}
