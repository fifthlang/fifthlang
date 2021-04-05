namespace Fifth.AST
{
    using Visitors;

    public class IdentifierExpression : Expression
    {
        public IdentifierExpression(Identifier identifier)
            => Identifier = identifier;

        public Identifier Identifier { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterIdentifierExpression(this);
            visitor.LeaveIdentifierExpression(this);
        }
    }
}
