namespace Fifth.AST
{
    using Symbols;
    using TypeSystem;

    public abstract class ScopeAstNode : AstNode, IScope
    {
        protected ScopeAstNode()
        {
            SymbolTable = new SymbolTable();
            EnclosingScope = default;
        }

        public IScope EnclosingScope { get; set; }
        public ISymbolTable SymbolTable { get; set; }

        public void Declare(string name, SymbolKind kind, IAstNode ctx, params (string, object)[] properties)
        {
            var symTabEntry = new SymTabEntry {Name = name, SymbolKind = kind, Line = Line, Context = ctx};
            foreach (var (x, y) in properties)
            {
                symTabEntry[x] = y;
            }

            SymbolTable[name] = symTabEntry;
        }

        public bool TryResolve(string name, out ISymbolTableEntry result)
        {
            result = null;
            var tmp = SymbolTable.Resolve(name);
            if (tmp != null)
            {
                result = tmp;
                return true;
            }

            return this?.ParentNode.NearestScope()?.TryResolve(name, out result) ?? false;
        }

        public ISymbolTableEntry Resolve(string name)
        {
            if (TryResolve(name, out var ste))
            {
                return ste;
            }

            throw new CompilationException($"Unable to resolve symbol {name}");
        }
    }
}
