namespace Fifth.AST
{
    using System;
    using Visitors;

    public class WhileExp : Statement
    {
        public WhileExp(Expression condition, Block loopBlock)
        {
            _ = condition ?? throw new ArgumentNullException(nameof(condition));
            _ = loopBlock ?? throw new ArgumentNullException(nameof(loopBlock));

            Condition = condition;
            LoopBlock = loopBlock;
        }

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
