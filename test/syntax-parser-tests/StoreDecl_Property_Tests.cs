using System.Linq;
using ast;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using test_infra;

namespace syntax_parser_tests;

/// <summary>
/// Property-based tests for store declaration parsing.
/// Validates that the parser produces correct AST for all store creation function variants.
/// </summary>
[Trait("Category", "PBT")]
public class StoreDecl_Property_Tests
{
    // ========================================================================
    // Custom FsCheck Generators
    // ========================================================================

    /// <summary>
    /// Generates a valid Fifth identifier: starts with a letter, followed by letters/digits.
    /// </summary>
    private static Gen<string> GenIdentifier()
    {
        return from firstChar in Gen.Elements(
                   "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray())
               from restLength in Gen.Choose(0, 8)
               from restChars in Gen.ArrayOf(restLength,
                   Gen.Elements("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray()))
               let name = new string(new[] { firstChar }.Concat(restChars).ToArray())
               // Avoid Fifth keywords that would conflict with parsing
               where name != "store" && name != "graph" && name != "class" && name != "return"
                  && name != "if" && name != "else" && name != "while" && name != "with"
                  && name != "import" && name != "namespace" && name != "alias" && name != "export"
                  && name != "new" && name != "true" && name != "false" && name != "nil"
                  && name != "default" && name != "from" && name != "in" && name != "where"
                  && name != "fun" && name != "try" && name != "catch" && name != "finally"
                  && name != "throw" && name != "as" && name != "extends"
               select name;
    }

    /// <summary>
    /// Represents a store function variant with its name and how to generate a valid argument list.
    /// </summary>
    public record StoreFuncVariant(string FuncName, string ArgString);

    /// <summary>
    /// Generates a store declaration string for one of the 4 function variants.
    /// </summary>
    private static Gen<(string StoreName, StoreFuncVariant Variant, string Code)> GenStoreDeclaration()
    {
        var variants = new[]
        {
            new StoreFuncVariant("remote_store", "<http://example.org/sparql>"),
            new StoreFuncVariant("local_store", "\"/data/my-store\""),
            new StoreFuncVariant("mem_store", ""),
            new StoreFuncVariant("sparql_store", "<http://example.org/sparql>"),
        };

        return from name in GenIdentifier()
               from variant in Gen.Elements(variants)
               let argPart = string.IsNullOrEmpty(variant.ArgString)
                   ? "()"
                   : $"({variant.ArgString})"
               let code = $"{name} : store = {variant.FuncName}{argPart};\nmain(): int {{ return 0; }}"
               select (name, variant, code);
    }

    /// <summary>
    /// Custom Arbitrary registrations for FsCheck [Property] attribute.
    /// </summary>
    public class StoreDeclArbitrary
    {
        public static Arbitrary<(string StoreName, StoreFuncVariant Variant, string Code)> StoreDecl() =>
            Arb.From(GenStoreDeclaration());
    }

    // ========================================================================
    // Property 3: Parser produces correct AST for all store creation functions
    // Validates: Requirements 5.3
    // ========================================================================

    /// <summary>
    /// For any store creation function in {remote_store, local_store, mem_store, sparql_store},
    /// parsing a valid store declaration using that function should produce a VarDeclStatement
    /// with a Kind = "StoreDecl" annotation. The function name in the AST should match the
    /// generated function name.
    ///
    /// **Validates: Requirements 5.3**
    /// </summary>
    [Property(MaxTest = 100, Arbitrary = new[] { typeof(StoreDeclArbitrary) })]
    public void Parser_Produces_Correct_AST_For_All_Store_Creation_Functions(
        (string StoreName, StoreFuncVariant Variant, string Code) input)
    {
        // Arrange & Act: parse the generated store declaration
        var result = ParseHarness.ParseString(input.Code,
            new ParseOptions(Phase: compiler.FifthParserManager.AnalysisPhase.None));

        // Assert: root should be non-null (parsed successfully)
        result.Root.Should().NotBeNull(
            $"parsing should succeed for store declaration: {input.Code}");

        // Assert: no parse errors (deprecation warnings are OK)
        result.Diagnostics
            .Where(d => d.Severity == DiagnosticSeverity.Error)
            .Should().BeEmpty(
                $"no parse errors expected for: {input.Code}");

        // Find the VarDeclStatement with StoreDecl annotation in the AST
        var module = result.Root!.Modules.FirstOrDefault();
        module.Should().NotBeNull("AST should contain at least one module");

        // Store declarations are top-level statements collected in the module
        // They appear as statements in function bodies or as module-level annotations
        // The AstBuilderVisitor puts store decls as annotations on the module
        // But the VarDeclStatement is also produced - let's check the module annotations
        module!.Annotations.Should().ContainKey("GraphStores",
            "module should have GraphStores annotation from store declaration");

        var stores = module.Annotations["GraphStores"] as Dictionary<string, string>;
        stores.Should().NotBeNull("GraphStores annotation should be a dictionary");
        stores.Should().ContainKey(input.StoreName,
            $"store name '{input.StoreName}' should be in GraphStores");

        // Verify the function name appears in the store value
        if (input.Variant.FuncName == "sparql_store")
        {
            // sparql_store uses the SPARQL keyword path - the URI is stored directly
            stores![input.StoreName].Should().NotBeNullOrEmpty(
                "sparql_store should have a non-empty URI value");
        }
        else
        {
            // For func_call variants, the full call text is stored
            stores![input.StoreName].Should().Contain(input.Variant.FuncName,
                $"store value should contain function name '{input.Variant.FuncName}'");
        }

        // Also verify the VarDeclStatement AST node is produced correctly
        // by checking that the visitor produced the StoreDecl annotation.
        // We do this by re-parsing at the statement level and inspecting the AST node directly.
        var stmtCode = $"{input.StoreName} : store = {input.Variant.FuncName}" +
            (string.IsNullOrEmpty(input.Variant.ArgString)
                ? "()"
                : $"({input.Variant.ArgString})") + ";\n";

        var (parser, errors) = Utils.ParserTestUtils.CreateParser(stmtCode);
        var storeCtx = parser.colon_store_decl();
        errors.Should().BeEmpty($"statement-level parse should succeed for: {stmtCode}");

        var visitor = new compiler.LangProcessingPhases.AstBuilderVisitor();
        var astNode = visitor.Visit(storeCtx);

        astNode.Should().BeOfType<VarDeclStatement>(
            "store declaration should produce a VarDeclStatement");

        var varDeclStmt = (VarDeclStatement)astNode;

        // Assert: Kind = "StoreDecl" annotation
        varDeclStmt.Annotations.Should().ContainKey("Kind");
        varDeclStmt.Annotations["Kind"].Should().Be("StoreDecl",
            "store declaration should have Kind = StoreDecl annotation");

        // Assert: variable name matches
        varDeclStmt.VariableDecl.Name.Should().Be(input.StoreName,
            $"variable name should be '{input.StoreName}'");

        // Assert: variable type is Store
        varDeclStmt.VariableDecl.TypeName.ToString().Should().Be("Store",
            "variable type should be Store");

        // Assert: InitialValue is a MemberAccessExp (KG.<func>(...))
        varDeclStmt.InitialValue.Should().BeOfType<MemberAccessExp>(
            "initial value should be a MemberAccessExp (KG.<func>)");

        var memberAccess = (MemberAccessExp)varDeclStmt.InitialValue!;

        // Assert: LHS is VarRefExp with VarName = "KG"
        memberAccess.LHS.Should().BeOfType<VarRefExp>();
        ((VarRefExp)memberAccess.LHS).VarName.Should().Be("KG",
            "LHS of member access should be KG");

        // Assert: RHS is FuncCallExp with correct function name
        memberAccess.RHS.Should().BeOfType<FuncCallExp>(
            "RHS should be a FuncCallExp");

        var funcCall = (FuncCallExp)memberAccess.RHS!;
        funcCall.Annotations.Should().ContainKey("FunctionName");

        var expectedFuncName = input.Variant.FuncName;
        funcCall.Annotations["FunctionName"].Should().Be(expectedFuncName,
            $"function name annotation should be '{expectedFuncName}'");
    }
}
