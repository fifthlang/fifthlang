using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 25: Rebuilds the symbol table after triple expansion,
/// ensuring all references point to updated AST nodes before type annotation.
/// </summary>
public class SymbolTableFinalPhase : ICompilerPhase
{
    public string Name => "SymbolTableFinal";
    public IReadOnlyList<string> DependsOn => new[] { "TripleExpanded" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "SymbolsFinal" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var result = new SymbolTableBuilderVisitor().Visit(ast);
        return PhaseResult.Ok(result);
    }
}
