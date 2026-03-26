using ast;
using compiler.SemanticAnalysis;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 7: Resolves constructor calls against class definitions.
/// Runs after symbol table is built so class definitions can be looked up.
/// </summary>
public class ConstructorResolutionPhase : ICompilerPhase
{
    public string Name => "ConstructorResolution";
    public IReadOnlyList<string> DependsOn => new[] { "Symbols" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "ResolvedConstructors" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var result = new ConstructorResolver(context.Diagnostics).Visit(ast);
        return PhaseResult.Ok(result);
    }
}
