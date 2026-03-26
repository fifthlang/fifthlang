using ast;
using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 10: Registers type parameters in scope after symbol table building (T030).
/// </summary>
public class TypeParameterResolutionPhase : ICompilerPhase
{
    public string Name => "TypeParameterResolution";
    public IReadOnlyList<string> DependsOn => new[] { "Symbols" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "TypeParameters" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var result = new TypeParameterResolutionVisitor().Visit(ast);
        return PhaseResult.Ok(result);
    }
}
