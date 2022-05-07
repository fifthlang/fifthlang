namespace Fifth.AST;

using Symbols;

public abstract class BaseVarReference : Expression{}
public partial class VariableReference
{
    public SymTabEntry SymTabEntry { get; set; }
}

public partial class CompoundVariableReference
{
    public override string? ToString()
        => string.Join('.', this.ComponentReferences.Select(cr => cr.Name).ToArray());
}
