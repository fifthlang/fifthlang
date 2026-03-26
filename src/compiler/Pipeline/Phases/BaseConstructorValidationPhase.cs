using ast;
using compiler.SemanticAnalysis;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 9: Validates base constructor calls (CTOR004, CTOR008 diagnostics).
/// Merges validator diagnostics into the phase context.
/// </summary>
public class BaseConstructorValidationPhase : ICompilerPhase
{
    public string Name => "BaseConstructorValidation";
    public IReadOnlyList<string> DependsOn => new[] { "ResolvedConstructors" };
    public IReadOnlyList<string> ProvidedCapabilities => Array.Empty<string>();

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var baseValidator = new BaseConstructorValidator();
        ast = baseValidator.Visit(ast);
        foreach (var diag in baseValidator.Diagnostics)
        {
            context.Diagnostics.Add(diag);
        }
        return PhaseResult.Ok(ast);
    }
}
