namespace Fifth.AST
{
    using Visitors;

    public class AliasDeclaration : AstNode
    {
        public AliasDeclaration(AstNode parentNode) : base(parentNode)
        {
        }

        public AbsoluteIri IRI { get; set; }
        public string Name { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterAlias(this);
            visitor.LeaveAlias(this);
        }
    }
}
