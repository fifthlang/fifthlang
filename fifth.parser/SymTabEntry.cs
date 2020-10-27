namespace Fifth.Parser
{
    using System.Collections.Generic;
    using Antlr4.Runtime;
    using Fifth.AST;

    public class SymTabEntry : BaseAnnotated, ISymbolTableEntry
    {
        public SymTabEntry() : base()
        {
        }

        public int Column { get; set; }
        public IAstNode Context { get; set; }
        public string Filename { get; set; }
        public int Line { get; set; }
        public string Name { get; set; }
        public SymbolKind SymbolKind { get; set; }
    }
}
