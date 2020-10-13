using Antlr4.Runtime;

namespace fifth.Parser
{
    public enum SymbolKind
    {
        Function, Variable, FormalParameter
    }
    public class SymTabEntry : ISymbolTableEntry
    {
        public string Name { get; set; }
        public string DefiningModule { get; set; }
        public int Line { get; set; }
        public SymbolKind SymbolKind { get; set; }
        public ParserRuleContext Context { get; set; }
    }
}