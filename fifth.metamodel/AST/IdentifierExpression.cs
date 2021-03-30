namespace Fifth.AST
{
    using Parser.LangProcessingPhases;
    using TypeSystem;

    public class IdentifierExpression : Expression
    {
        public IdentifierExpression(Identifier identifier, TypeId fifthType) : base(fifthType) => Identifier = identifier;

        public IdentifierExpression(Identifier identifier) : base(identifier.FifthType) => Identifier = identifier;

        public Identifier Identifier { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterIdentifierExpression(this);
            visitor.LeaveIdentifierExpression(this);
        }
    }
}
