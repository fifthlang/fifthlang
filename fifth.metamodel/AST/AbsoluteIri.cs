namespace Fifth.AST
{
    using Parser.LangProcessingPhases;
    using PrimitiveTypes;
    using TypeSystem.PrimitiveTypes;

    public class AbsoluteIri : TypedAstNode
    {
        public string Uri { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterAbsoluteUri(this);
            visitor.LeaveAbsoluteUri(this);
        }

        public AbsoluteIri(AstNode parentNode) : base(parentNode, PrimitiveUri.Default)
        {
        }
    }
}
