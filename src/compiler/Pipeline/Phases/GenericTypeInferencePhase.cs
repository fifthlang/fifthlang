using ast;
using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 11: Infers generic type arguments after type parameter resolution (T045).
/// </summary>
public class GenericTypeInferencePhase : ICompilerPhase
{
    public string Name => "GenericTypeInference";
    public IReadOnlyList<string> DependsOn => new[] { "TypeParameters" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "GenericTypes" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var result = new GenericTypeInferenceVisitor().Visit(ast);
        return PhaseResult.Ok(result);
    }
}
