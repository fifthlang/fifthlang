namespace Fifth.AST
{
    using Fifth.VirtualMachine;

    public abstract class Expression : AstNode
    {
        public IFifthType FifthType { get; set; }
    }
}
