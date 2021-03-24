namespace Fifth.AST
{
    using TypeSystem;

    public interface ITypedAstNode
    {
        IFifthType FifthType { get; set; }
    }
}
