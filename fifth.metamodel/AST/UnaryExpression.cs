namespace Fifth.AST
{
    using Visitors;

    public class UnaryExpression : Expression
    {
        public UnaryExpression(Expression operand, Operator op)
        {
            Operand = operand;
            Op = op;
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
