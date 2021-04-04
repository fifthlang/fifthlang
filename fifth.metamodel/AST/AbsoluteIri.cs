namespace Fifth.AST
{
    using PrimitiveTypes;
    using TypeSystem.PrimitiveTypes;
    using Visitors;

    public class AbsoluteIri : TypedAstNode
    {
        public string Uri { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterAbsoluteUri(this);
            visitor.LeaveAbsoluteUri(this);
        }

        public AbsoluteIri(AstNode parentNode) : base(parentNode, PrimitiveUri.Default.TypeId)
        {
        }
    }
}
