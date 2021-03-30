namespace Fifth.AST
{
    using Symbols;
    using TypeSystem;

    public abstract class ScopeAstNode : TypedAstNode, IScope
    {
        protected ScopeAstNode(AstNode parentNode, TypeId fifthType, IScope enclosingScope) : base(parentNode, fifthType)
        {
            SymbolTable = new SymbolTable();
            EnclosingScope = enclosingScope;
        }

        protected ScopeAstNode(TypeId fifthType) : base(fifthType)
        {
            SymbolTable = new SymbolTable();
        }

        public IScope EnclosingScope { get; set; }
        public ISymbolTable SymbolTable { get; set; }

        public void Declare(string name, SymbolKind kind, IAstNode ctx, params (string, object)[] properties)
        {
            var symTabEntry = new SymTabEntry
            {
                Name = name,
                SymbolKind = kind,
                Line = Line,
                Context = ctx
            };
            foreach ((var x, var y) in properties)
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
            => SymbolTable.Resolve(name); // needs to recurse up
    }
}
