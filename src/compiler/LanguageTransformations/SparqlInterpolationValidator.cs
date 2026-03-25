using ast;

namespace Fifth.LangProcessingPhases;

/// <summary>
/// Validates interpolation expressions in SPARQL literals.
/// Ensures interpolations follow the rules defined in User Story 3:
/// - No nested interpolations (SPARQL004) - no ?&lt;...> or @&lt;...> inside {{...}}
/// - All other expressions are allowed (function calls, arithmetic, complex expressions, etc.)
///
/// All diagnostics are emitted directly as <see cref="compiler.Diagnostic"/>.
/// </summary>
public class SparqlInterpolationValidator : DefaultRecursiveDescentVisitor
{
    private readonly List<compiler.Diagnostic> diagnostics;

    /// <summary>
    /// Constructs a new validator that routes diagnostics into the supplied list.
    /// </summary>
    public SparqlInterpolationValidator(List<compiler.Diagnostic>? diagnostics = null)
    {
        this.diagnostics = diagnostics ?? new List<compiler.Diagnostic>();
    }

    /// <summary>
    /// Gets the diagnostics generated during validation.
    /// </summary>
    public IReadOnlyList<compiler.Diagnostic> Diagnostics => diagnostics.AsReadOnly();

    public override SparqlLiteralExpression VisitSparqlLiteralExpression(SparqlLiteralExpression ctx)
    {
        var result = base.VisitSparqlLiteralExpression(ctx);

        foreach (var interpolation in result.Interpolations)
        {
            ValidateInterpolation(interpolation, result);
        }

        return result;
    }

    private void ValidateInterpolation(Interpolation interpolation, SparqlLiteralExpression context)
    {
        if (interpolation.Expression == null)
        {
            return;
        }

        if (ContainsNestedInterpolation(interpolation.Expression))
        {
            diagnostics.Add(new compiler.Diagnostic(
                compiler.DiagnosticLevel.Error,
                SparqlDiagnostics.FormatNestedInterpolation(),
                context.Location?.Filename,
                SparqlDiagnostics.NestedInterpolation,
                Line: context.Location?.Line,
                Column: context.Location?.Column));
        }
    }

    private static bool ContainsNestedInterpolation(Expression expr)
    {
        return expr switch
        {
            SparqlLiteralExpression => true,
            TriGLiteralExpression => true,
            BinaryExp binary => ContainsNestedInterpolation(binary.LHS) || ContainsNestedInterpolation(binary.RHS),
            UnaryExp unary => ContainsNestedInterpolation(unary.Operand),
            FuncCallExp funcCall => funcCall.InvocationArguments.Any(arg => ContainsNestedInterpolation(arg)),
            MemberAccessExp memberAccess =>
                ContainsNestedInterpolation(memberAccess.LHS) ||
                (memberAccess.RHS != null && ContainsNestedInterpolation(memberAccess.RHS)),
            _ => false
        };
    }
}
