using ast;
using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 3: Compound phase that inserts class constructors then re-links the tree.
/// Runs ClassCtorInserter followed by TreeLinkageVisitor to fix parent pointers
/// on newly inserted constructor nodes.
/// </summary>
public class ClassCtorsPhase : ICompilerPhase
{
    public string Name => "ClassCtors";
    public IReadOnlyList<string> DependsOn => new[] { "TreeStructure" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "ClassConstructors" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        ast = new ClassCtorInserter(context.Diagnostics).Visit(ast);
        ast = new TreeLinkageVisitor().Visit(ast);
        return PhaseResult.Ok(ast);
    }
}
