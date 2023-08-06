namespace Fifth.AST
{
    using fifth.metamodel.metadata;

    public interface ITypedAstNode
    {
        TypeId TypeId { get; set; }
    }
}
