namespace Fifth.AST
{
    using TypeSystem;

    public abstract class TypedAstNode : AstNode, ITypedAstNode
    {
        protected TypedAstNode(AstNode parentNode, IFifthType fifthType)
            : base(parentNode) => FifthType = fifthType;

        protected TypedAstNode(IFifthType fifthType)
            : base() => FifthType = fifthType;

        public IFifthType FifthType { get; set; }
    }
}
