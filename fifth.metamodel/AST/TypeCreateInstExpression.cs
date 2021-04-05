namespace Fifth.AST
{
    using Visitors;

    public class TypeCreateInstExpression : Expression
    {
        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterTypeCreateInstExpression(this);
            visitor.LeaveTypeCreateInstExpression(this);
        }
    }
}
