using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 29: Validates external qualified calls now that types have been annotated.
/// Skipped when diagnostics are not being collected (matching the original
/// <c>if (diagnostics != null)</c> guard in ParserManager).
/// </summary>
public class ExternalCallValidationPhase : ICompilerPhase
{
    public string Name => "ExternalCallValidation";
    public IReadOnlyList<string> DependsOn => new[] { "Types" };
    public IReadOnlyList<string> ProvidedCapabilities => Array.Empty<string>();

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        ast = new ExternalCallValidationVisitor(context.Diagnostics, context.TargetFramework).Visit(ast);

        if (context.Diagnostics.Any(d => d.Level == DiagnosticLevel.Error))
            return PhaseResult.Fail(ast, context.Diagnostics.ToList());

        return PhaseResult.Ok(ast);
    }
}
