using Fifth.LangProcessingPhases;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 27: Resolves variable references in the AST by performing symbol table lookups.
/// </summary>
public class VarRefResolverPhase : ICompilerPhase
{
    public string Name => "VarRefResolver";
    public IReadOnlyList<string> DependsOn => new[] { "SymbolsFinal" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "VarRefs" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var result = new VarRefResolverVisitor().Visit(ast);
        return PhaseResult.Ok(result);
    }
}
