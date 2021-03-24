namespace Fifth.Symbols
{
    using AST;

    public interface IScope
    {
        IScope EnclosingScope { get; set; }
        ISymbolTable SymbolTable { get; set; }

        void Declare(string name, SymbolKind kind, IAstNode ctx, params (string, object)[] properties);

        ISymbolTableEntry Resolve(string name);

        bool TryResolve(string name, out ISymbolTableEntry result);
    }
}
