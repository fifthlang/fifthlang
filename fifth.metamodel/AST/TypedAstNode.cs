namespace Fifth.AST
{
    using fifth.metamodel.metadata;

    public abstract class TypedAstNode : AstNode, ITypedAstNode
    {
        protected TypedAstNode()
        {
        }

        protected TypedAstNode(TypeId fifthType)
        {
            TypeId = fifthType;
        }

        public TypeId TypeId { get; set; }
    }
}
