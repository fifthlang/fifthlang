namespace Fifth.AST
{
    using Symbols;

    public abstract class BaseVarReference : Expression{}
    public partial class VariableReference
    {
        public SymTabEntry SymTabEntry { get; set; }
    }
}
