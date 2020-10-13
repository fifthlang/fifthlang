using System;
using System.Collections.Generic;

namespace fifth.Parser
{
    public class SymbolTable : Dictionary<string, ISymbolTableEntry>, ISymbolTable
    {
        public ISymbolTableEntry Resolve(string v)
        {
            ISymbolTableEntry result;

            if (TryGetValue(v, out result))
            {
                return result;
            }
            return null;
        }
        public IEnumerable<ISymbolTableEntry> All() => Values;
    }

    public interface ISymbolTable : IDictionary<string, ISymbolTableEntry>
    {
        IEnumerable<ISymbolTableEntry> All();
        ISymbolTableEntry Resolve(string v);
    }
}