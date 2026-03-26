using compiler.LanguageTransformations;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 36: Compound phase that validates lambda capture constraints,
/// then lowers lambdas to closure classes + Apply calls, then re-links the tree.
/// </summary>
public class LambdaClosureConversionPhase : ICompilerPhase
{
    public string Name => "LambdaClosureConversion";
    public IReadOnlyList<string> DependsOn => new[] { "Types" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "ClosureConverted" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        // 1. Validate lambda capture constraints
        ast = new LambdaCaptureValidationVisitor(context.Diagnostics).Visit(ast);

        if (context.Diagnostics.Any(d => d.Level == DiagnosticLevel.Error))
            return PhaseResult.Fail(ast, context.Diagnostics.ToList());

        // 2. Lower lambdas to closure classes + Apply calls
        var rewriter = new LambdaClosureConversionRewriter();
        var result = rewriter.Rewrite(ast);
        ast = result.Node;

        // 3. Re-link tree after rewriting so downstream components see consistent parents
        ast = new TreeLinkageVisitor().Visit(ast);

        return PhaseResult.Ok(ast);
    }
}
