namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public class AbsoluteIri : AstNode
    {
        public string Uri { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterAbsoluteUri(this);
            visitor.LeaveAbsoluteUri(this);
        }
    }
}
