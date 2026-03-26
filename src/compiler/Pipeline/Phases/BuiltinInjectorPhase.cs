using ast;
using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 2: Injects built-in type definitions into the AST.
/// </summary>
public class BuiltinInjectorPhase : ICompilerPhase
{
    public string Name => "BuiltinInjector";
    public IReadOnlyList<string> DependsOn => new[] { "TreeStructure" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "Builtins" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var result = new BuiltinInjectorVisitor().Visit(ast);
        return PhaseResult.Ok(result);
    }
}
