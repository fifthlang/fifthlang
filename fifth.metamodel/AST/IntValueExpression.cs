namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;
    using TypeSystem;

    public class IntValueExpression : LiteralExpression<int>
    {
        public IntValueExpression(int value, AstNode parentNode, IFifthType type)
            : base(value, parentNode, type)
        {
        }

        public IntValueExpression(int value, IFifthType type)
            : base(value, type)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterIntValueExpression(this);
            visitor.LeaveIntValueExpression(this);
        }
    }
}
