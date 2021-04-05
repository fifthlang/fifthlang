namespace Fifth.AST
{
    using System;
    using Visitors;

    public class IfElseStatement : Statement
    {
        public IfElseStatement(Expression condition, Block ifBlock, Block elseBlock)
        {
            _ = condition ?? throw new ArgumentNullException(nameof(condition));
            _ = ifBlock ?? throw new ArgumentNullException(nameof(ifBlock));
            _ = elseBlock ?? throw new ArgumentNullException(nameof(elseBlock));

            Condition = condition;
            IfBlock = ifBlock;
            ElseBlock = elseBlock;
        }

        public Expression Condition { get; set; }
        public Block ElseBlock { get; set; }
        public Block IfBlock { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterIfElseExp(this);

            Condition.Accept(visitor);
            IfBlock.Accept(visitor);
            ElseBlock.Accept(visitor);

            visitor.LeaveIfElseExp(this);
        }
    }
}
