namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public class AliasDeclaration : AstNode
    {
        public AbsoluteIri IRI { get; set; }
        public string Name { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterAlias(this);
            visitor.LeaveAlias(this);
        }

        public AliasDeclaration(AstNode parentNode) : base(parentNode)
        {
        }
    }
}
