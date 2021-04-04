namespace Fifth.AST
{
    using TypeSystem;
    using Visitors;

    public class UnaryExpression : Expression
    {
        public UnaryExpression(TypeId fifthType) : base(fifthType)
        {
        }

        public UnaryExpression(AstNode parentNode, TypeId fifthType) : base(parentNode, fifthType)
        {
        }

        public UnaryExpression(Expression operand, Operator op, AstNode parentNode, TypeId fifthType)
            : this(parentNode, fifthType)
        {
            Operand = operand;
            Op = op;
        }

        public UnaryExpression(Expression operand, Operator op, TypeId fifthType)
            : this(fifthType)
        {
            Operand = operand;
            Op = op;
        }

        public UnaryExpression(Expression operand, Operator op)
            : this(operand, op, operand.TypeId)
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
