namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public class FloatValueExpression : LiteralExpression<float>
    {
        public FloatValueExpression(float value, AstNode parentNode, IFifthType type)
            : base(value, parentNode, type)
        {
        }
        public FloatValueExpression(float value, IFifthType type)
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
