using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 35: Validates lambda functions (arity limits, etc.).
/// </summary>
public class LambdaValidationPhase : ICompilerPhase
{
    public string Name => "LambdaValidation";
    public IReadOnlyList<string> DependsOn => new[] { "Types" };
    public IReadOnlyList<string> ProvidedCapabilities => Array.Empty<string>();

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        ast = new LambdaValidationVisitor(context.Diagnostics).Visit(ast);

        if (context.Diagnostics.Any(d => d.Level == DiagnosticLevel.Error))
            return PhaseResult.Fail(ast, context.Diagnostics.ToList());

        return PhaseResult.Ok(ast);
    }
}
