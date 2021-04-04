namespace Fifth.AST
{
    using TypeSystem;
    using Visitors;

    public class FloatValueExpression : LiteralExpression<float>
    {
        public FloatValueExpression(float value, AstNode parentNode, TypeId type)
            : base(value, parentNode, type)
        {
        }

        public FloatValueExpression(float value, TypeId type)
            : base(value, type)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterFloatValueExpression(this);
            visitor.LeaveFloatValueExpression(this);
        }
    }
}
