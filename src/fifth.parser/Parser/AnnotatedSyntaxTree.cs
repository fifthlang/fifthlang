using System.Collections.Generic;
using Antlr4.Runtime;
using fifth.parser.Parser;
using static FifthParser;

public class AnnotatedSyntaxTree
{
    private AnnotatedSyntaxTree()
    {
        ScopeLookupTable = new Dictionary<ParserRuleContext, IScope>();
    }
    public AnnotatedSyntaxTree(ParserRuleContext astRoot) : this()
    {
        AstRoot = astRoot;
    }

    /// A lookup table to get from the AST Nodes to the scopes defined for them
    public Dictionary<ParserRuleContext, IScope> ScopeLookupTable { get; private set; }

    // keep a track of the root of the AST
    public ParserRuleContext AstRoot { get; set; }

    public IScope CreateNewScope(ParserRuleContext ctx)
    {
        // check if this is we are creating a root/global scope for the outermost context
        if (AstRoot == null && ctx.Payload is FifthContext)
        {
            AstRoot = ctx;
        }

        // if we've created a scope for this context before, reuse it
        if (ScopeLookupTable.ContainsKey(ctx))
        {
            return ScopeLookupTable[ctx];
        }
        // create the scope, attach it to context and return
        return ScopeLookupTable[ctx] = new Scope(ctx);
    }
    public IScope CreateNewScope(ParserRuleContext ctx, IScope enclosingScope)
    {
        var result = CreateNewScope(ctx);
        result.EnclosingScope = enclosingScope;
        return result;
    }
}
