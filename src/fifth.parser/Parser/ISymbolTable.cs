using System.Collections.Generic;

namespace Fifth.Parser
{
    public interface ISymbolTable : IDictionary<string, ISymbolTableEntry>
    {
        IEnumerable<ISymbolTableEntry> All();
        ISymbolTableEntry Resolve(string v);
    }
}