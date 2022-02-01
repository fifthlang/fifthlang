namespace Fifth.AST
{
    public abstract class TypedAstNode : AstNode, ITypedAstNode
    {
        protected TypedAstNode()
        {
        }

        protected TypedAstNode(TypeId fifthType) => TypeId = fifthType;

        public TypeId TypeId { get; set; }
    }
}