namespace Fifth.AST
{
    public abstract class Expression : TypedAstNode
    {
        protected Expression(AstNode parentNode, IFifthType fifthType) : base(parentNode, fifthType)
        {
        }
        protected Expression(IFifthType fifthType) : base(fifthType)
        {
        }
    }
}
