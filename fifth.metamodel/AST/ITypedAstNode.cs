namespace Fifth.AST
{
    using TypeSystem;

    public interface ITypedAstNode
    {
        TypeId TypeId { get; set; }
    }
}
