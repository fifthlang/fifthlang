namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public class IfElseStmt : Statement
    {
        public Expression Condition { get; set; }
        public Block ElseBlock { get; set; }
        public Block IfBlock { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterIfElseStmt(this);

            Condition.Accept(visitor);
            IfBlock.Accept(visitor);
            ElseBlock.Accept(visitor);

            visitor.LeaveIfElseStmt(this);
        }
    }
}
