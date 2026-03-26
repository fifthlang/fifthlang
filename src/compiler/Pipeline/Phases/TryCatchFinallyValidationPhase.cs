using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 30: Validates try/catch/finally constructs for semantic correctness.
/// Skipped when diagnostics are not being collected (matching the original
/// <c>if (diagnostics != null)</c> guard in ParserManager).
/// </summary>
public class TryCatchFinallyValidationPhase : ICompilerPhase
{
    public string Name => "TryCatchFinallyValidation";
    public IReadOnlyList<string> DependsOn => new[] { "Types" };
    public IReadOnlyList<string> ProvidedCapabilities => Array.Empty<string>();

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        ast = new TryCatchFinallyValidationVisitor(context.Diagnostics).Visit(ast);

        if (context.Diagnostics.Any(d => d.Level == DiagnosticLevel.Error))
            return PhaseResult.Fail(ast, context.Diagnostics.ToList());

        return PhaseResult.Ok(ast);
    }
}
