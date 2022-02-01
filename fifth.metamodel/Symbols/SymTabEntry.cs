namespace Fifth.Symbols
{
    using AST;

    public class SymTabEntry : AnnotatedThing, ISymbolTableEntry
    {
        public SymTabEntry() : base()
        {
        }

        public int Column { get; set; }
        public IAstNode Context { get; set; }
        public string Filename { get; set; }
        public int Line { get; set; }
        public string OriginalText { get; set; }
        public string Name { get; set; }
        public SymbolKind SymbolKind { get; set; }
    }
}
