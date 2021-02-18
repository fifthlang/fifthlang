namespace Fifth.AST
{
    using Parser.LangProcessingPhases;

    public class BooleanExpression : LiteralExpression<bool>
    {
        public BooleanExpression(bool value):base(value)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterBooleanExpression(this);
            visitor.LeaveBooleanExpression(this);
        }
    }
}
