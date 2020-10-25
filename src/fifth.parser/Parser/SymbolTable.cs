using System;
using System.Collections.Generic;

namespace Fifth.Parser
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
}