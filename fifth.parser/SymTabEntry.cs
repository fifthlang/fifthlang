namespace Fifth.Parser
{
    using System.Collections.Generic;
    using Antlr4.Runtime;
    using Fifth.AST;

    public class SymTabEntry : ISymbolTableEntry
    {
        public SymTabEntry() => Annotations = new Dictionary<string, object>();

        public IDictionary<string, object> Annotations { get; private set; }
        public IAstNode Context { get; set; }
        public string DefiningModule { get; set; }
        public int Line { get; set; }
        public string Name { get; set; }
        public SymbolKind SymbolKind { get; set; }
    }
}
