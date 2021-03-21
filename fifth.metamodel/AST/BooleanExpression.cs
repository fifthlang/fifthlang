namespace Fifth.AST
{
    using Parser.LangProcessingPhases;

    public class BooleanExpression : LiteralExpression<bool>
    {
        public BooleanExpression(bool value, AstNode parentNode, IFifthType type)
            : base(value, parentNode, type)
        {
        }
        public BooleanExpression(bool value, IFifthType type)
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
