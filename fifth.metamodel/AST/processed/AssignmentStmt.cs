namespace Fifth.AST.Deprecated
{
    using Visitors;

    public class AssignmentStmt : Statement
    {
        public AssignmentStmt(VariableReference variableRef, Expression expression)
        {
            VariableRef = variableRef;
            Expression = expression;
        }

        public Expression Expression { get; set; }
        public VariableReference VariableRef { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterAssignmentStmt(this);
            Expression.Accept(visitor);
            VariableRef.Accept(visitor);
            visitor.LeaveAssignmentStmt(this);
        }
    }
}
