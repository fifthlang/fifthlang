namespace Fifth.AST
{
    using PrimitiveTypes;
    using Visitors;

    public class StringValueExpression : LiteralExpression<string>
    {
        public StringValueExpression(string value)
            : base(value, PrimitiveString.Default.TypeId)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterStringValueExpression(this);
            visitor.LeaveStringValueExpression(this);
        }
    }
}
