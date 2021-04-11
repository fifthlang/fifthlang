namespace Fifth.AST.Deprecated
{
    using TypeSystem;

    public interface ITypedAstNode
    {
        TypeId TypeId { get; set; }
    }
}
