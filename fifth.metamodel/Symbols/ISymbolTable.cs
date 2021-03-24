namespace Fifth.Symbols
{
    using System.Collections.Generic;

    public interface ISymbolTable : IDictionary<string, ISymbolTableEntry>
    {
        IEnumerable<ISymbolTableEntry> All();

        ISymbolTableEntry Resolve(string v);
    }
}
