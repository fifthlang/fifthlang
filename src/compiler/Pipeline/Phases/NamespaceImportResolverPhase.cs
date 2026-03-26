using ast;
using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 6: Resolves namespace import declarations against the symbol table.
/// </summary>
public class NamespaceImportResolverPhase : ICompilerPhase
{
    public string Name => "NamespaceImportResolver";
    public IReadOnlyList<string> DependsOn => new[] { "Symbols" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "NamespaceImports" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var result = new NamespaceImportResolverVisitor(context.Diagnostics).Visit(ast);
        return PhaseResult.Ok(result);
    }
}
