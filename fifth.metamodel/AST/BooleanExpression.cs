namespace Fifth.AST
{
    using Parser.LangProcessingPhases;
    using TypeSystem;

    public class BooleanExpression : LiteralExpression<bool>
    {
        public BooleanExpression(bool value, AstNode parentNode, TypeId type)
            : base(value, parentNode, type)
        {
        }

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
