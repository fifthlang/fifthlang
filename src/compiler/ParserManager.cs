using Antlr4.Runtime;
using compiler.LangProcessingPhases;
using Fifth;

namespace compiler;

public static class FifthParserManager
{
    private static FifthParser GetParserForStream(ICharStream source)
    {
        var lexer = new FifthLexer(source);
        lexer.RemoveErrorListeners();
        lexer.AddErrorListener(new ThrowingErrorListener<int>());

        var parser = new FifthParser(new CommonTokenStream(lexer));
        parser.RemoveErrorListeners();
        parser.AddErrorListener(new ThrowingErrorListener<IToken>());
        return parser;
    }

    #region File Handling

    public static AstThing ParseFile(string sourceFile)
    {
        return ParseFile(sourceFile, null);
    }

    public static AstThing ParseFile(string sourceFile, List<Diagnostic>? diagnostics)
    {
        var parser = GetParserForFile(sourceFile);
        var tree = parser.fifth();
        var v = new AstBuilderVisitor();
        var ast = v.Visit(tree);
        if (diagnostics != null)
        {
            foreach (var d in v.Diagnostics)
            {
                diagnostics.Add(new Diagnostic(
                    d.Level switch
                    {
                        AstDiagnosticLevel.Warning => DiagnosticLevel.Warning,
                        AstDiagnosticLevel.Error => DiagnosticLevel.Error,
                        _ => DiagnosticLevel.Info
                    },
                    d.Message,
                    sourceFile,
                    d.Code));
            }
        }
        return ast as AssemblyDef ?? throw new System.Exception("ParseFile did not produce an AssemblyDef AST");
    }

    public static (FifthParser parser, FifthParser.FifthContext tree) ParseFileToTree(string sourceFile)
    {
        var parser = GetParserForFile(sourceFile);
        var tree = parser.fifth();
        return (parser, tree);
    }

    // Parse only: lex + parse without building the AST. Used by syntax-only tests.
    public static void ParseFileSyntaxOnly(string sourceFile)
    {
        var parser = GetParserForFile(sourceFile);
        parser.fifth();
        var next = parser.TokenStream.LA(1);
        if (next != Antlr4.Runtime.TokenConstants.EOF)
        {
            throw new System.Exception($"Unexpected trailing tokens after parse. Next token type: {next}");
        }
    }

    private static FifthParser GetParserForFile(string sourceFile)
    {
        var s = CharStreams.fromPath(sourceFile);
        return GetParserForStream(s);
    }

    #endregion File Handling

    #region Embedded Resource Handling

    public static AstThing ParseEmbeddedResource(Stream sourceStream)
    {
        var parser = GetParserForEmbeddedResource(sourceStream);
        var tree = parser.fifth();
        var v = new AstBuilderVisitor();
        var ast = v.Visit(tree);
        return ast as AssemblyDef ?? throw new System.Exception("ParseEmbeddedResource did not produce an AssemblyDef AST");
    }

    public static (FifthParser parser, FifthParser.FifthContext tree) ParseEmbeddedResourceToTree(Stream sourceStream)
    {
        var parser = GetParserForEmbeddedResource(sourceStream);
        var tree = parser.fifth();
        return (parser, tree);
    }

    private static FifthParser GetParserForEmbeddedResource(Stream sourceStream)
    {
        var s = CharStreams.fromStream(sourceStream);
        return GetParserForStream(s);
    }

    #endregion Embedded Resource Handling

    #region String handling

    public static AstThing ParseString(string source)
    {
        var parser = GetParserForString(source);
        var tree = parser.fifth();
        var v = new AstBuilderVisitor();
        var ast = v.Visit(tree);
        return ast as AssemblyDef ?? throw new System.Exception("ParseString did not produce an AssemblyDef AST");
    }

    public static (FifthParser parser, FifthParser.FifthContext tree) ParseStringToTree(string source)
    {
        var parser = GetParserForString(source);
        var tree = parser.fifth();
        return (parser, tree);
    }

    private static FifthParser GetParserForString(string source)
    {
        var s = CharStreams.fromString(source);
        return GetParserForStream(s);
    }

    #endregion String handling
}
