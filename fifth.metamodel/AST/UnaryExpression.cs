namespace Fifth.AST
{
    using Parser.LangProcessingPhases;
    using TypeSystem;

    public class UnaryExpression : Expression
    {
        public UnaryExpression(IFifthType fifthType) : base(fifthType)
        {
        }

        public UnaryExpression(AstNode parentNode, IFifthType fifthType) : base(parentNode, fifthType)
        {
        }

        public UnaryExpression(Expression operand, Operator op, AstNode parentNode, IFifthType fifthType)
            : this(parentNode, fifthType)
        {
            Operand = operand;
            Op = op;
        }

        public UnaryExpression(Expression operand, Operator op, IFifthType fifthType)
            : this(fifthType)
        {
            Operand = operand;
            Op = op;
        }

        public UnaryExpression(Expression operand, Operator op)
            : this(operand, op, operand.FifthType)
        {
        }

        public Operator Op { get; set; }
        public Expression Operand { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterUnaryExpression(this);
            Operand.Accept(visitor);
            visitor.LeaveUnaryExpression(this);
        }
    }
}
