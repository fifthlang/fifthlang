namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public class TypeInitialiser : AstNode
    {
        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterTypeInitialiser(this);
            visitor.LeaveTypeInitialiser(this);
        }
    }
}
