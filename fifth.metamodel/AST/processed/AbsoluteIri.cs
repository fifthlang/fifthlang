namespace Fifth.AST.Deprecated
{
    using TypeSystem.PrimitiveTypes;
    using Visitors;

    public class AbsoluteIri : TypedAstNode
    {
        public AbsoluteIri() : base(PrimitiveUri.Default.TypeId)
        {
        }

        public string Uri { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterAbsoluteIri(this);
            visitor.LeaveAbsoluteIri(this);
        }
    }
}
