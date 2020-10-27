namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public class Identifier : AstNode
    {
        public string Value { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterIdentifier(this);
            visitor.LeaveIdentifier(this);
        }
    }
}
