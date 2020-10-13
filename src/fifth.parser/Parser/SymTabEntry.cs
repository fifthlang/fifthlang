using Antlr4.Runtime;

namespace fifth.Parser
{
    public class SymTabEntry : ISymbolTableEntry
    {
        public string Name { get; set; }
        public string DefiningModule { get; set; }
        public int Line { get; set; }
        public SymbolKind SymbolKind { get; set; }
        public ParserRuleContext Context { get; set; }
    }
}