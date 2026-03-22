using FluentAssertions;
using syntax_parser_tests.Utils;
using test_infra;

namespace syntax_parser_tests;

public class ValidDeclaration_SyntaxTests
{
    [Fact]
    public void VariableDeclaration_ShouldSucceed()
    {
        var input = "x : int = 1;";
        ParserTestUtils.AssertNoErrors(input + "\n", p => p.statement(),
            "Colon-form variable declaration should parse");
    }

    [Fact]
    public void StoreDeclaration_ShouldSucceed()
    {
        var input = "home : store = sparql_store(<http://example.com/>);";
        ParserTestUtils.AssertNoErrors(input + "\n", p => p.statement(),
            "Colon-form store declaration should parse");
    }

    [Fact]
    public void GraphDeclaration_ShouldSucceed()
    {
        var input = "g : graph = KG.CreateGraph();";
        ParserTestUtils.AssertNoErrors(input + "\n", p => p.statement(),
            "Colon-form graph declaration should parse");
    }

    [Fact]
    public void SparqlStoreDeclaration_ShouldEmitDeprecationWarning()
    {
        var code = "home : store = sparql_store(<http://example.com/>);\nmain(): int { return 0; }";
        var result = ParseHarness.ParseString(code, new ParseOptions(Phase: compiler.FifthParserManager.AnalysisPhase.None));

        result.Root.Should().NotBeNull("sparql_store declaration should still parse successfully");
        result.Diagnostics.Should().Contain(d => d.Code == "STORE_DEPRECATED_001" && d.Severity == DiagnosticSeverity.Warning,
            "sparql_store usage should emit STORE_DEPRECATED_001 warning");
    }

    [Fact]
    public void NewStoreFunc_ShouldNotEmitDeprecationWarning()
    {
        var code = "home : store = remote_store(<http://example.com/>);\nmain(): int { return 0; }";
        var result = ParseHarness.ParseString(code, new ParseOptions(Phase: compiler.FifthParserManager.AnalysisPhase.None));

        result.Root.Should().NotBeNull("remote_store declaration should parse successfully");
        result.Diagnostics.Should().NotContain(d => d.Code == "STORE_DEPRECATED_001",
            "remote_store usage should not emit deprecation warning");
    }

    // --- New store declaration form tests (Requirements 5.2, 5.4, 10.1–10.3, 10.5) ---

    [Fact]
    public void RemoteStoreDeclaration_ShouldSucceed()
    {
        var input = "name : store = remote_store(<http://example.com/>);";
        ParserTestUtils.AssertNoErrors(input + "\n", p => p.statement(),
            "Colon-form remote_store declaration should parse");
    }

    [Fact]
    public void LocalStoreDeclaration_ShouldSucceed()
    {
        var input = "name : store = local_store(\"/data/store\");";
        ParserTestUtils.AssertNoErrors(input + "\n", p => p.statement(),
            "Colon-form local_store declaration should parse");
    }

    [Fact]
    public void MemStoreDeclaration_ShouldSucceed()
    {
        var input = "name : store = mem_store();";
        ParserTestUtils.AssertNoErrors(input + "\n", p => p.statement(),
            "Colon-form mem_store declaration should parse");
    }

    [Fact]
    public void SparqlStoreDeclaration_Regression_ShouldSucceed()
    {
        var input = "name : store = sparql_store(<http://example.com/>);";
        ParserTestUtils.AssertNoErrors(input + "\n", p => p.statement(),
            "Colon-form sparql_store declaration should still parse (regression)");
    }

    [Fact]
    public void DefaultStoreDecl_LocalStore_ShouldSucceed()
    {
        var code = "store default = local_store(\"/data/store\");\nmain(): int { return 0; }";
        var result = ParseHarness.ParseString(code, new ParseOptions(Phase: compiler.FifthParserManager.AnalysisPhase.None));

        result.Root.Should().NotBeNull("default store declaration with local_store should parse");
        result.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error).Should().BeEmpty(
            "default store declaration with local_store should not produce parse errors");
    }

    [Fact]
    public void DefaultStoreDecl_RemoteStore_ShouldSucceed()
    {
        var code = "store default = remote_store(<http://example.com/>);\nmain(): int { return 0; }";
        var result = ParseHarness.ParseString(code, new ParseOptions(Phase: compiler.FifthParserManager.AnalysisPhase.None));

        result.Root.Should().NotBeNull("default store declaration with remote_store should parse");
        result.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error).Should().BeEmpty(
            "default store declaration with remote_store should not produce parse errors");
    }

    [Fact]
    public void DefaultStoreDecl_MemStore_ShouldSucceed()
    {
        var code = "store default = mem_store();\nmain(): int { return 0; }";
        var result = ParseHarness.ParseString(code, new ParseOptions(Phase: compiler.FifthParserManager.AnalysisPhase.None));

        result.Root.Should().NotBeNull("default store declaration with mem_store should parse");
        result.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error).Should().BeEmpty(
            "default store declaration with mem_store should not produce parse errors");
    }
}
