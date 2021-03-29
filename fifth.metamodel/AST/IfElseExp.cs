namespace Fifth.AST
{
    using System;
    using Parser.LangProcessingPhases;
    using TypeSystem;

    public class IfElseExp : Statement
    {
        public IfElseExp(IFifthType fifthType)
            : base(fifthType)
        {
        }

        public IfElseExp(Expression condition, Block ifBlock, Block elseBlock, IFifthType fifthType)
            : base(fifthType) =>
            Construct(condition, ifBlock, elseBlock);

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

        public void Construct(Expression condition, Block ifBlock, Block elseBlock)
        {
            _ = condition ?? throw new ArgumentNullException(nameof(condition));
            _ = ifBlock ?? throw new ArgumentNullException(nameof(ifBlock));
            _ = elseBlock ?? throw new ArgumentNullException(nameof(elseBlock));

            if (ifBlock.FifthType != elseBlock.FifthType)
            {
                throw new TypeCheckingException("Unable to create if statement with different types in its blocks");
            }

            Condition = condition;
            IfBlock = ifBlock;
            ElseBlock = elseBlock;
            FifthType = IfBlock.FifthType; // just to be sure
        }
    }
}
