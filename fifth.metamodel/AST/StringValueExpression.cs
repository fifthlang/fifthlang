namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;
    using TypeSystem;

    public class StringValueExpression : LiteralExpression<string>
    {
        public StringValueExpression(string value, AstNode parentNode, TypeId type)
            : base(value, parentNode, type)
        {
        }

        public StringValueExpression(string value, TypeId type)
            : base(value, type)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterStringValueExpression(this);
            visitor.LeaveStringValueExpression(this);
        }
    }
}
