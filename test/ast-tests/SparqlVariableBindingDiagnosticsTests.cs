using ast;
using ast_model.Symbols;
using FluentAssertions;
using test_infra;

namespace ast_tests;

/// <summary>
/// Tests that SPARQL variable binding diagnostics are routed into the main compiler
/// diagnostic pipeline (REM-005).
/// </summary>
public class SparqlVariableBindingDiagnosticsTests
{
    private ParseResult ParseHarnessed(string code)
        => ParseHarness.ParseString(code, new ParseOptions());

    /// <summary>
    /// A SPARQL literal that only references in-scope Fifth variables should produce
    /// no SPARQL002 diagnostics.
    /// </summary>
    [Fact]
    public void SparqlVariableBinding_ValidVariable_NoDiagnostic()
    {
        const string code = """
            main(): int {
                age: int = 42;
                q: Query = ?<SELECT ?s WHERE { ?s <http://ex/age> age: }>;
                return 0;
            }
            """;

        var result = ParseHarnessed(code);

        result.Diagnostics
            .Where(d => d.Code == "SPARQL002")
            .Should().BeEmpty("in-scope variable 'age' should resolve without error");
    }

    /// <summary>
    /// When a SPARQL literal references an identifier that is NOT in scope as a Fifth variable,
    /// the compiler must emit a SPARQL002 diagnostic at Error severity into the main pipeline.
    /// </summary>
    [Fact]
    public void SparqlVariableBindingTest_InvalidVariable_ProducesError()
    {
        // 'unknownVar' is not declared anywhere — the binding visitor should flag it.
        const string code = """
            main(): int {
                q: Query = ?<SELECT ?s WHERE { ?s <http://ex/val> unknownVar: }>;
                return 0;
            }
            """;

        // Parse up to just before SPARQL lowering to get the AST with SparqlLiteralExpression intact
        var result = ParseHarness.ParseString(code, new ParseOptions(
            "UnaryOperatorLowering"));

        // Verify the AST contains a SparqlLiteralExpression
        var sparqlNodes = FindSparqlLiterals(result.Root!).ToList();
        sparqlNodes.Should().NotBeEmpty("the SPARQL literal should be parsed into a SparqlLiteralExpression");

        var sparqlExpr = sparqlNodes[0];
        sparqlExpr.SparqlText.Should().Contain("unknownVar", "the SPARQL text should contain the variable reference");

        // Check that NearestScope is available
        var scope = sparqlExpr.NearestScope();
        scope.Should().NotBeNull("the SPARQL literal should have a nearest scope after tree linkage + symbol table");

        // Now manually run the binding visitor on the parsed AST
        var sparqlDiags = new List<compiler.Diagnostic>();
        var visitor = new Fifth.LangProcessingPhases.SparqlVariableBindingVisitor(sparqlDiags);
        visitor.Visit(result.Root!);

        sparqlDiags.Should().Contain(d =>
            d.Code == "SPARQL002" && d.Level == compiler.DiagnosticLevel.Error,
            "an undeclared variable in a SPARQL literal must produce a SPARQL002 error diagnostic");
    }

    private static IEnumerable<SparqlLiteralExpression> FindSparqlLiterals(AssemblyDef root)
    {
        foreach (var module in root.Modules)
        {
            foreach (var func in module.Functions.OfType<FunctionDef>())
            {
                foreach (var stmt in func.Body.Statements)
                {
                    if (stmt is VarDeclStatement vds && vds.InitialValue is SparqlLiteralExpression sle)
                        yield return sle;
                    if (stmt is ExpStatement es && es.RHS is SparqlLiteralExpression sle2)
                        yield return sle2;
                }
            }
        }
    }
}
