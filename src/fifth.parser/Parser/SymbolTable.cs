using System.Collections.Generic;

namespace fifth.Parser
{
    public class SymbolTable : Dictionary<string, ISymbolTableEntry>, ISymbolTable{
        
    }

    public interface ISymbolTable : IDictionary<string, ISymbolTableEntry>
    {
    }
}