namespace Fifth.AST
{
    using System;
    using Parser.LangProcessingPhases;
    using TypeSystem;

    public class WhileExp : Statement
    {
        public WhileExp(AstNode parentNode, TypeId fifthType)
            : base(parentNode, fifthType)
        {
        }

        public WhileExp(Expression condition, Block loopBlock, AstNode parentNode, TypeId fifthType)
            : this(parentNode, fifthType)
        {
            _ = condition ?? throw new ArgumentNullException(nameof(condition));
            _ = loopBlock ?? throw new ArgumentNullException(nameof(loopBlock));

            Condition = condition;
            LoopBlock = loopBlock;
        }

        public WhileExp(Expression condition, Block loopBlock, TypeId fifthType)
            : this(condition, loopBlock, null, fifthType)
        {
        }

        public WhileExp(Expression condition, Block loopBlock, AstNode parentNode)
            : this(parentNode, loopBlock?.TypeId)
        {
            _ = condition ?? throw new ArgumentNullException(nameof(condition));
            _ = loopBlock ?? throw new ArgumentNullException(nameof(loopBlock));

            Condition = condition;
            LoopBlock = loopBlock;
        }

        public WhileExp(Expression condition, Block loopBlock)
            : this(condition, loopBlock, null, loopBlock?.TypeId)
        {
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
