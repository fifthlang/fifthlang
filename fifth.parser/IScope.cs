namespace Fifth.Parser
{
    using Fifth.AST;

    public interface IScope
    {
        IAstNode AstNode { get; set; }
        IScope EnclosingScope { get; set; }
        ISymbolTable SymbolTable { get; set; }

        void Declare(string name, SymbolKind kind, IAstNode ctx, params (string, object)[] properties);
    }
}
