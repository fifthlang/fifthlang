using System.Collections.Generic;
using Antlr4.Runtime;
using fifth.parser.Parser;
using static FifthParser;

public class AnnotatedSyntaxTree
{
    public AnnotatedSyntaxTree(ParserRuleContext astRoot)
    {
        AstRoot = astRoot;
    }

    /// A lookup table to get from the AST Nodes to the scopes defined for them
    public Dictionary<ParserRuleContext, IScope> ScopeLookupTable
        => new Dictionary<ParserRuleContext, IScope>();

        // keep a track of the root of the AST
    public ParserRuleContext AstRoot { get; set; }

    public IScope CreateNewScope(ParserRuleContext ctx)
    {
        // check if this is we are creating a root/global scope for the outermost context
        if (AstRoot == null && ctx is FifthContext)
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
}
