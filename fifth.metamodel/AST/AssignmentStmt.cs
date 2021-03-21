namespace Fifth.AST
{
    using Parser.LangProcessingPhases;

    public class AssignmentStmt : Statement
    {
        public AssignmentStmt(AstNode parentNode, IFifthType fifthType)
            : base(parentNode, fifthType)
        {
        }
        public AssignmentStmt(IFifthType fifthType)
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
