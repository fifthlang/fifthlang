namespace Fifth.AST
{
    using TypeSystem;

    public abstract class Expression : TypedAstNode
    {
        protected Expression(AstNode parentNode, TypeId fifthType) : base(parentNode, fifthType)
        {
        }

        protected Expression(TypeId fifthType) : base(fifthType)
        {
        }
    }
}
