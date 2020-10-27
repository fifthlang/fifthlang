namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public class IntValueExpression : LiteralExpression<int>
    {
        public IntValueExpression(int value) : base(value)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterIntValueExpression(this);
            visitor.LeaveIntValueExpression(this);
        }
    }
}
