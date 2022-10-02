namespace Fifth.AST;

using Symbols;

public abstract class BaseVarReference : Expression
{
}
public partial class VariableReference
{
    public ISymbolTableEntry? SymTabEntry { get; set; }
}

public partial class CompoundVariableReference
{
    public override string? ToString()
    {
        return string.Join('.', this.ComponentReferences.Select(cr => cr.Name).ToArray());
    }
}
