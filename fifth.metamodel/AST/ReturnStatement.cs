namespace Fifth.AST
{
    using Visitors;

    public class ReturnStatement : Statement
    {
        public ReturnStatement(Expression expression)
        {
            Expression = expression;
        }

        public Expression Expression { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterReturnStatement(this);
            Expression.Accept(visitor);
            visitor.LeaveReturnStatement(this);
        }
    }
}
