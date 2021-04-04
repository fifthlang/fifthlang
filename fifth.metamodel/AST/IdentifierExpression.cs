namespace Fifth.AST
{
    using TypeSystem;
    using Visitors;

    public class IdentifierExpression : Expression
    {
        public IdentifierExpression(Identifier identifier, TypeId fifthType) : base(fifthType) => Identifier = identifier;

        public IdentifierExpression(Identifier identifier) : base(identifier.TypeId) => Identifier = identifier;

        public Identifier Identifier { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterIdentifierExpression(this);
            visitor.LeaveIdentifierExpression(this);
        }
    }
}
