namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public class FloatValueExpression : LiteralExpression<float>
    {
        public FloatValueExpression(float value) : base(value)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterFloatValueExpression(this);
            visitor.LeaveFloatValueExpression(this);
        }
    }
}
