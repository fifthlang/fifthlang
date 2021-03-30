namespace Fifth.AST
{
    using TypeSystem;

    public abstract class Statement : Expression
    {
        protected Statement(AstNode parentNode, TypeId fifthType)
            : base(parentNode, fifthType)
        {
        }

        protected Statement(TypeId fifthType)
            : base(fifthType)
        {
        }
    }
}
