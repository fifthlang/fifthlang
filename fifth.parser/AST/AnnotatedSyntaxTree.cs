namespace Fifth
{
    using System.Collections.Generic;
    using Fifth.AST;
    using Fifth.Parser;
    using static FifthParser;

    public class AnnotatedSyntaxTree
    {
        public AnnotatedSyntaxTree(IAstNode astRoot) : this() => AstRoot = astRoot;

        private AnnotatedSyntaxTree() => ScopeLookupTable = new Dictionary<IAstNode, IScope>();

        // keep a track of the root of the AST
        public IAstNode AstRoot { get; set; }

        /// A lookup table to get from the AST Nodes to the scopes defined for them
        public Dictionary<IAstNode, IScope> ScopeLookupTable { get; private set; }

        public IScope CreateNewScope(IAstNode ctx)
        {
            // check if this is we are creating a root/global scope for the outermost context
            if (AstRoot == null && ctx is FifthProgram)
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

        public IScope CreateNewScope(IAstNode ctx, IScope enclosingScope)
        {
            var result = CreateNewScope(ctx);
            result.EnclosingScope = enclosingScope;
            return result;
        }
    }
}
