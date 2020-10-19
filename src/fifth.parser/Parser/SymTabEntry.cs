using System.Collections.Generic;
using Antlr4.Runtime;

namespace fifth.parser.Parser
{
    public class SymTabEntry : ISymbolTableEntry
    {
        public SymTabEntry()
        {
            Annotations = new Dictionary<string, object>();
        }
        public string Name { get; set; }
        public string DefiningModule { get; set; }
        public int Line { get; set; }
        public SymbolKind SymbolKind { get; set; }
        public ParserRuleContext Context { get; set; }

        public IDictionary<string, object> Annotations { get; private set; }
    }
}
