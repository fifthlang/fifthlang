using compiler.LanguageTransformations;
using Fifth.LangProcessingPhases;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 28: Compound phase that runs type annotation, symbol table rebuild,
/// augmented assignment lowering, type error collection, graph triple operator
/// lowering with re-link/re-annotation, and second-pass type error collection.
/// </summary>
public class TypeAnnotationPhase : ICompilerPhase
{
    public string Name => "TypeAnnotation";
    public IReadOnlyList<string> DependsOn => new[] { "Symbols", "VarRefs" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "Types" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        var diagnostics = new List<Diagnostic>();

        // 1. Run type annotation
        var typeAnnotationVisitor = new TypeAnnotationVisitor();
        ast = typeAnnotationVisitor.Visit(ast);

        // 2. Rebuild symbol table after type annotation
        ast = new SymbolTableBuilderVisitor().Visit(ast);

        // 3. Lower augmented assignments (+= and -=) using type information
        ast = new AugmentedAssignmentLoweringRewriter().Visit(ast);

        // 4. Collect type errors (only Error severity, not Info)
        foreach (var error in typeAnnotationVisitor.Errors
            .Where(e => e.Severity == TypeCheckingSeverity.Error))
        {
            diagnostics.Add(new Diagnostic(
                DiagnosticLevel.Error,
                $"{error.Message} at {error.Filename}:{error.Line}:{error.Column}",
                error.Filename,
                "TYPE_ERROR"));
        }

        // 5. If errors, return failure
        if (diagnostics.Any(d => d.Level == DiagnosticLevel.Error))
            return PhaseResult.Fail(ast, diagnostics);

        // 6. Lower graph triple operators with full type info available
        ast = (AstThing)new TripleGraphAdditionLoweringRewriter().Rewrite(ast).Node;

        // 7. Re-link after rewriting
        ast = new TreeLinkageVisitor().Visit(ast);

        // 8. Rebuild symbol table
        ast = new SymbolTableBuilderVisitor().Visit(ast);

        // 9. Re-resolve variable references
        ast = new VarRefResolverVisitor().Visit(ast);

        // 10. Second type annotation pass
        var typeAnnotationVisitor2 = new TypeAnnotationVisitor();
        ast = typeAnnotationVisitor2.Visit(ast);

        // 11. Final symbol table rebuild
        ast = new SymbolTableBuilderVisitor().Visit(ast);

        // 12. Collect second-pass type errors
        foreach (var error in typeAnnotationVisitor2.Errors
            .Where(e => e.Severity == TypeCheckingSeverity.Error))
        {
            diagnostics.Add(new Diagnostic(
                DiagnosticLevel.Error,
                $"{error.Message} at {error.Filename}:{error.Line}:{error.Column}",
                error.Filename,
                "TYPE_ERROR"));
        }

        // 13. If errors, return failure
        if (diagnostics.Any(d => d.Level == DiagnosticLevel.Error))
            return PhaseResult.Fail(ast, diagnostics);

        // 14. Return success
        return PhaseResult.Ok(ast, diagnostics);
    }
}
