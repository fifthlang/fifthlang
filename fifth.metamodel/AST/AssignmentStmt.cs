namespace Fifth.AST
{
    using TypeSystem;
    using Visitors;

    public class AssignmentStmt : Statement
    {
        public AssignmentStmt(AstNode parentNode, TypeId fifthType)
            : base(parentNode, fifthType)
        {
        }

        public AssignmentStmt(TypeId fifthType)
            : base(fifthType)
        {
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
