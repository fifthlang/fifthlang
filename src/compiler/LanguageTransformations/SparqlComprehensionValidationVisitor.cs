using ast;
using ast_model.TypeSystem;

namespace Fifth.LangProcessingPhases;

/// <summary>
/// Validation visitor for SPARQL comprehensions.
///
/// Validates:
/// 1. Generator type is list or tabular SELECT result
/// 2. For SPARQL generators: query is SELECT (not ASK/CONSTRUCT/DESCRIBE)
/// 3. For SPARQL object projections: property values use property access syntax (x.propertyName)
/// 4. For SPARQL object projections: referenced properties exist in SELECT projection
/// 5. Constraints are boolean expressions
/// 6. Rejects ?variable syntax (must use x.property instead)
///
/// Emits diagnostic codes LCOMP001-006 for validation failures.
/// All diagnostics are emitted directly as <see cref="compiler.Diagnostic"/> into the
/// shared compiler diagnostic list.
/// </summary>
public class SparqlComprehensionValidationVisitor : DefaultRecursiveDescentVisitor
{
    private readonly List<compiler.Diagnostic> diagnostics;

    public SparqlComprehensionValidationVisitor(List<compiler.Diagnostic>? diagnostics = null)
    {
        this.diagnostics = diagnostics ?? new List<compiler.Diagnostic>();
    }

    /// <summary>
    /// Gets the diagnostics emitted during validation.
    /// </summary>
    public IReadOnlyList<compiler.Diagnostic> Diagnostics => diagnostics.AsReadOnly();

    public override ListComprehension VisitListComprehension(ListComprehension ctx)
    {
        var result = base.VisitListComprehension(ctx);

        ValidateGeneratorType(result);

        if (result.Source is SparqlLiteralExpression sparqlSource)
        {
            ValidateSparqlComprehension(result, sparqlSource);
        }

        ValidateConstraints(result);

        return result;
    }

    /// <summary>
    /// Validates that the generator (source) expression has a compatible type.
    /// </summary>
    private void ValidateGeneratorType(ListComprehension ctx)
    {
        if (ctx.Source.Type == null)
        {
            return;
        }

        var sourceType = ctx.Source.Type;

        var isListType = sourceType switch
        {
            FifthType.TType t => t.Name.ToString().StartsWith("List<") ||
                                  t.Name.ToString() == "List" ||
                                  t.Name.ToString() == "Result",
            FifthType.TListOf => true,
            FifthType.TArrayOf => true,
            FifthType.UnknownType => true,
            _ => false
        };

        if (!isListType)
        {
            EmitDiagnostic(
                compiler.ComprehensionDiagnostics.InvalidGeneratorType,
                compiler.ComprehensionDiagnostics.FormatInvalidGeneratorType(sourceType.ToString()),
                compiler.DiagnosticLevel.Error,
                ctx.Source);
        }
    }

    /// <summary>
    /// Validates SPARQL-specific comprehension rules.
    /// </summary>
    private void ValidateSparqlComprehension(ListComprehension ctx, SparqlLiteralExpression sparqlSource)
    {
        var introspection = compiler.LanguageTransformations.SparqlSelectIntrospection.IntrospectQuery(sparqlSource.SparqlText);

        if (!introspection.Success)
        {
            return;
        }

        if (introspection.QueryForm != "SELECT")
        {
            EmitDiagnostic(
                compiler.ComprehensionDiagnostics.NonSelectQuery,
                compiler.ComprehensionDiagnostics.FormatNonSelectQuery(introspection.QueryForm ?? "unknown"),
                compiler.DiagnosticLevel.Error,
                sparqlSource);
            return;
        }

        if (ctx.Projection is ObjectInitializerExp objProj)
        {
            ValidateSparqlObjectProjection(objProj, introspection);
        }
    }

    /// <summary>
    /// Validates SPARQL object projection bindings.
    /// </summary>
    private void ValidateSparqlObjectProjection(
        ObjectInitializerExp objProj,
        compiler.LanguageTransformations.SparqlSelectIntrospection.IntrospectionResult introspection)
    {
        if (objProj.PropertyInitialisers == null)
        {
            return;
        }

        foreach (var propInit in objProj.PropertyInitialisers)
        {
            if (propInit.RHS == null)
            {
                continue;
            }

            if (propInit.RHS is VarRefExp varRef && varRef.VarName.StartsWith('?'))
            {
                EmitDiagnostic(
                    compiler.ComprehensionDiagnostics.InvalidObjectProjectionBinding,
                    "SPARQL variable references using '?variable' syntax are not allowed. Use property access syntax instead (e.g., 'x.age' where x is the iteration variable).",
                    compiler.DiagnosticLevel.Error,
                    varRef);
                continue;
            }

            if (propInit.RHS is MemberAccessExp memberAccess)
            {
                if (memberAccess.RHS is VarRefExp memberName)
                {
                    var propertyName = memberName.VarName;

                    if (!compiler.LanguageTransformations.SparqlSelectIntrospection.HasProjectedVariable(introspection, propertyName))
                    {
                        var availableVars = introspection.IsSelectStar
                            ? "*"
                            : string.Join(", ", introspection.ProjectedVariables);

                        EmitDiagnostic(
                            compiler.ComprehensionDiagnostics.UnknownProperty,
                            compiler.ComprehensionDiagnostics.FormatUnknownProperty(propertyName, availableVars),
                            compiler.DiagnosticLevel.Error,
                            memberAccess);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Validates that all where constraints are boolean expressions.
    /// </summary>
    private void ValidateConstraints(ListComprehension ctx)
    {
        if (ctx.Constraints == null || ctx.Constraints.Count == 0)
        {
            return;
        }

        foreach (var constraint in ctx.Constraints)
        {
            if (constraint.Type == null)
            {
                continue;
            }

            var isBooleanType = constraint.Type switch
            {
                FifthType.TType t => t.Name.ToString() == "bool" || t.Name.ToString() == "Boolean",
                FifthType.TDotnetType dt => dt.Name.ToString() == "bool" || dt.Name.ToString() == "Boolean" || dt.TheType == typeof(bool),
                _ => false
            };

            if (!isBooleanType)
            {
                EmitDiagnostic(
                    compiler.ComprehensionDiagnostics.NonBooleanConstraint,
                    compiler.ComprehensionDiagnostics.FormatNonBooleanConstraint(constraint.Type.ToString()),
                    compiler.DiagnosticLevel.Error,
                    constraint);
            }
        }
    }

    private void EmitDiagnostic(string code, string message, compiler.DiagnosticLevel level, AstThing context)
    {
        diagnostics.Add(new compiler.Diagnostic(
            level,
            message,
            context.Location?.Filename,
            code,
            Line: context.Location?.Line,
            Column: context.Location?.Column));
    }
}
