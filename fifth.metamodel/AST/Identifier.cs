namespace Fifth.AST
{
    using Parser.LangProcessingPhases;
    using TypeSystem;

    public class Identifier : TypedAstNode
    {
        public Identifier(string value) : base(null)
        {
            Value = value;
        }

        public Identifier(string value, IFifthType type) : base(type)
        {
            Value = value;
        }

        public string Value { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterIdentifier(this);
            visitor.LeaveIdentifier(this);
        }
    }
}
