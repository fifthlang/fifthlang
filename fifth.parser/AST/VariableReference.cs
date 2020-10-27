namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public class VariableReference : AstNode
    {
        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterVariableReference(this);
            visitor.LeaveVariableReference(this);
        }
    }
}
