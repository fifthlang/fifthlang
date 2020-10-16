namespace fifth.parser.Parser
{
    public interface ISymbolTableEntry
    {
        string DefiningModule { get; set; }
        int Line { get; set; }
        string Name { get; set; }
        SymbolKind SymbolKind { get; set; }
    }
}