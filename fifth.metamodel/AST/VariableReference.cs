namespace Fifth.AST
{
    using Visitors;

    public class VariableReference : TypedAstNode
    {
        public VariableReference(string name) : base(null) => Name = name;

        public string Name { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterVariableReference(this);
            visitor.LeaveVariableReference(this);
        }
    }
}
