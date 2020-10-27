namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public class TypeCreateInstExpression : Expression
    {
        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterTypeCreateInstExpression(this);
            visitor.LeaveTypeCreateInstExpression(this);
        }
    }
}
