using ast;
using Fifth.LangProcessingPhases;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 19: Resolves Fifth variable references within SPARQL literals.
/// Must run before SparqlLiteralLowering (which destroys SparqlLiteralExpression nodes)
/// but after SymbolTableInitial (which populates variable declarations).
/// </summary>
public class SparqlVariableBindingPhase : ICompilerPhase
{
    public string Name => "SparqlVariableBinding";
    public IReadOnlyList<string> DependsOn => new[] { "Symbols" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "SparqlBindings" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var result = new SparqlVariableBindingVisitor(context.Diagnostics).Visit(ast);
        return PhaseResult.Ok(result);
    }
}
