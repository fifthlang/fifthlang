namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public class AssignmentStmt : Statement
    {
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
