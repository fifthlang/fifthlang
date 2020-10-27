namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public class IdentifierExpression : Expression
    {
        public Identifier Identifier { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterIdentifierExpression(this);
            visitor.LeaveIdentifierExpression(this);
        }
    }
}
