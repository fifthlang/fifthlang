namespace Fifth.AST
{
    using PrimitiveTypes;
    using Visitors;

    public class IntValueExpression : LiteralExpression<int>
    {
        public IntValueExpression(int value)
            : base(value, PrimitiveInteger.Default.TypeId)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterIntValueExpression(this);
            visitor.LeaveIntValueExpression(this);
        }
    }
}
