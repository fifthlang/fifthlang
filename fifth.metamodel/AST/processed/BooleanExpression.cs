namespace Fifth.AST.Deprecated
{
    using Visitors;

    public class BooleanExpression : LiteralExpression<bool>
    {
        public BooleanExpression(bool value, TypeId type)
            : base(value, type)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterBooleanExpression(this);
            visitor.LeaveBooleanExpression(this);
        }
    }
}
