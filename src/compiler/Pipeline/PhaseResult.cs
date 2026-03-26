using ast;

namespace compiler.Pipeline;

/// <summary>
/// Structured result from a phase execution, replacing the null-return convention.
/// </summary>
public record PhaseResult(
    AstThing TransformedAst,
    IReadOnlyList<Diagnostic> Diagnostics,
    bool Success
)
{
    /// <summary>Create a successful result with no diagnostics.</summary>
    public static PhaseResult Ok(AstThing ast) =>
        new(ast, Array.Empty<Diagnostic>(), true);

    /// <summary>Create a successful result with diagnostics.</summary>
    public static PhaseResult Ok(AstThing ast, IReadOnlyList<Diagnostic> diagnostics) =>
        new(ast, diagnostics, true);

    /// <summary>Create a failure result.</summary>
    public static PhaseResult Fail(AstThing ast, IReadOnlyList<Diagnostic> diagnostics) =>
        new(ast, diagnostics, false);
}
