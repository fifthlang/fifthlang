namespace Fifth.AST
{
    using Parser.LangProcessingPhases;

    public class WhileExp : Statement
    {
        public Expression Condition { get; set; }
        public Block LoopBlock { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterWhileExp(this);

            Condition.Accept(visitor);
            LoopBlock.Accept(visitor);

            visitor.LeaveWhileExp(this);
        }
    }
}
