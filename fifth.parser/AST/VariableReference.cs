namespace Fifth.AST
{
    using Parser.LangProcessingPhases;

    public class VariableReference : AstNode
    {
        public string Name { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterVariableReference(this);
            visitor.LeaveVariableReference(this);
        }
    }
}
