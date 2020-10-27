namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public class StringValueExpression : LiteralExpression<string>
    {
        public StringValueExpression(string value) : base(value)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterStringValueExpression(this);
            visitor.LeaveStringValueExpression(this);
        }
    }
}
