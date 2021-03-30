namespace Fifth.AST
{
    using TypeSystem;

    public abstract class TypedAstNode : AstNode, ITypedAstNode
    {
        protected TypedAstNode(AstNode parentNode, TypeId fifthType)
            : base(parentNode) => FifthType = fifthType;

        protected TypedAstNode(TypeId fifthType)
            : base() => FifthType = fifthType;

        public TypeId FifthType { get; set; }
    }
}
