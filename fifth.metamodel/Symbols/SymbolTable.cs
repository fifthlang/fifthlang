namespace Fifth.Symbols
{
    using System.Collections.Generic;

    public class SymbolTable : Dictionary<string, ISymbolTableEntry>, ISymbolTable
    {
        public IEnumerable<ISymbolTableEntry> All() => Values;

        public ISymbolTableEntry Resolve(string v)
        {
            if (TryGetValue(v, out var result))
            {
                return result;
            }
            return null;
        }
    }
}
