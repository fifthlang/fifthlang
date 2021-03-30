namespace Fifth.AST
{
    using Parser.LangProcessingPhases;
    using TypeSystem;

    public class VariableReference : TypedAstNode
    {
        public VariableReference(AstNode parentNode, TypeId fifthType)
            : base(parentNode, fifthType)
        {
        }

        public string Name { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterVariableReference(this);
            visitor.LeaveVariableReference(this);
        }
    }
}
