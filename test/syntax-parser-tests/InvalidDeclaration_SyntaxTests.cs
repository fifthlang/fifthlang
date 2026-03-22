using FluentAssertions;
using syntax_parser_tests.Utils;
using Fifth;

namespace syntax_parser_tests;

public class InvalidDeclaration_SyntaxTests
{
    [Fact]
    public void TypeFirstVariableDeclaration_ShouldFail()
    {
        // Incorrect (legacy-style) declaration: Type name = expr; should be name : Type = expr;
        var input = "Person eric = new Person();";
        ParserTestUtils.AssertHasErrors(input + "\n", p => p.statement(),
            "Type-first variable declaration should not be accepted by grammar");
    }

    [Fact]
    public void TypeFirstStoreDeclaration_KeywordFirst_ShouldSucceed()
    {
        // The keyword-first form 'store <name> = ...' is now valid grammar
        // (e.g. 'store default = sparql_store(<iri>);')
        var input = "store home = sparql_store(<http://example.com/>);";
        ParserTestUtils.AssertNoErrors(input + "\n", p => p.statement(),
            "Keyword-first store declaration should be accepted by grammar");
    }

    [Fact]
    public void TypeFirstGraphDeclaration_ShouldFail()
    {
        var input = "graph g = KG.CreateGraph();";
        ParserTestUtils.AssertHasErrors(input + "\n", p => p.statement(),
            "Type-first graph declaration should not be accepted; expect 'g : graph = KG.CreateGraph();' syntax");
    }
}
