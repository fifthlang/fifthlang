namespace Fifth.Parser
{
    using Antlr4.Runtime;
    using Fifth.AST;

    public class Scope : IScope
    {
        public Scope(IAstNode astNode)
        {
            SymbolTable = new SymbolTable();
            AstNode = astNode;
        }

        public Scope(IAstNode astNode, IScope enclosingScope) : this(astNode) => EnclosingScope = enclosingScope;

        public IAstNode AstNode { get; set; }
        public IScope EnclosingScope { get; set; }
        public ISymbolTable SymbolTable { get; set; }

        public void Declare(string name, SymbolKind kind, IAstNode ctx, params (string, object)[] properties)
        {
            var symTabEntry = new SymTabEntry
            {
                Name = name,
                SymbolKind = kind,
                Line = ctx.Line,
                Context = ctx
            };
            foreach ((var x, var y) in properties)
            {
                symTabEntry.Annotations[x] = y;
            }
            SymbolTable[name] = symTabEntry;
        }
    }
}
