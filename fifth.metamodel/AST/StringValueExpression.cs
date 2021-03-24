namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;
    using TypeSystem;

    public class StringValueExpression : LiteralExpression<string>
    {
        public StringValueExpression(string value, AstNode parentNode, IFifthType type)
            : base(value, parentNode, type)
        {
        }

        public StringValueExpression(string value, IFifthType type)
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
