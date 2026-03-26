using ast;
using compiler.SemanticAnalysis;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 4: Validates constructor declarations (CTOR001–CTOR010 diagnostics).
/// </summary>
public class ConstructorValidationPhase : ICompilerPhase
{
    public string Name => "ConstructorValidation";
    public IReadOnlyList<string> DependsOn => new[] { "ClassConstructors" };
    public IReadOnlyList<string> ProvidedCapabilities => Array.Empty<string>();

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var result = new ConstructorValidator(context.Diagnostics).Visit(ast);
        return PhaseResult.Ok(result);
    }
}
