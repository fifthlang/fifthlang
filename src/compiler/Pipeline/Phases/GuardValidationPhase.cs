using ast;
using compiler.Validation.GuardValidation;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 15: Validates completeness of guarded function overload sets.
/// Merges guard validator diagnostics into context and returns Fail on errors.
/// </summary>
public class GuardValidationPhase : ICompilerPhase
{
    public string Name => "GuardValidation";
    public IReadOnlyList<string> DependsOn => new[] { "OverloadGroups" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "GuardChecks" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var guardValidator = new GuardCompletenessValidator();
        ast = guardValidator.Visit(ast);
        var diagnostics = guardValidator.Diagnostics.ToList();
        foreach (var diag in diagnostics)
            context.Diagnostics.Add(diag);
        if (diagnostics.Any(d => d.Level == DiagnosticLevel.Error))
            return PhaseResult.Fail(ast, diagnostics);
        return PhaseResult.Ok(ast, diagnostics);
    }
}
