using System.Collections.Generic;

namespace fifth.parser.Parser
{
    public interface ISymbolTable : IDictionary<string, ISymbolTableEntry>
    {
        IEnumerable<ISymbolTableEntry> All();
        ISymbolTableEntry Resolve(string v);
    }
}