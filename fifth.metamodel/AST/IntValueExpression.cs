namespace Fifth.AST
{
    using TypeSystem;
    using Visitors;

    public class IntValueExpression : LiteralExpression<int>
    {
        public IntValueExpression(int value, AstNode parentNode, TypeId type)
            : base(value, parentNode, type)
        {
        }

        public IntValueExpression(int value, TypeId type)
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
