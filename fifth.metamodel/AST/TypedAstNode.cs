namespace Fifth.AST
{
    using TypeSystem;

    public abstract class TypedAstNode : AstNode, ITypedAstNode
    {
        protected TypedAstNode(AstNode parentNode, TypeId fifthType)
            : base(parentNode) => TypeId = fifthType;

        protected TypedAstNode(TypeId fifthType)
            : base() => TypeId = fifthType;

        public TypeId TypeId { get; set; }
    }
}
