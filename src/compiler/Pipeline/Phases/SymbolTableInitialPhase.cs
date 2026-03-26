using ast;
using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 5: Builds the initial symbol table from the AST.
/// </summary>
public class SymbolTableInitialPhase : ICompilerPhase
{
    public string Name => "SymbolTableInitial";
    public IReadOnlyList<string> DependsOn => new[] { "TreeStructure", "Builtins" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "Symbols" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var result = new SymbolTableBuilderVisitor().Visit(ast);
        return PhaseResult.Ok(result);
    }
}
