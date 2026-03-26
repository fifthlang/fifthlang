using Fifth.LangProcessingPhases;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 33: Validates SPARQL comprehensions before lowering.
/// Ensures the original comprehension AST structure is valid.
/// </summary>
public class ListComprehensionValidationPhase : ICompilerPhase
{
    public string Name => "ListComprehensionValidation";
    public IReadOnlyList<string> DependsOn => new[] { "Types" };
    public IReadOnlyList<string> ProvidedCapabilities => Array.Empty<string>();

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var validator = new SparqlComprehensionValidationVisitor(context.Diagnostics);
        ast = validator.Visit(ast);

        if (context.Diagnostics.Any(d => d.Level == DiagnosticLevel.Error))
            return PhaseResult.Fail(ast, context.Diagnostics.ToList());

        return PhaseResult.Ok(ast);
    }
}
