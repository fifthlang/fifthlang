namespace Fifth.AST
{
    using Visitors;

    public class BinaryExpression : Expression
    {
        public BinaryExpression(Expression left, Expression right, Operator op)
        {
            Left = left;
            Right = right;
            Op = op;
        }

        public Expression Left { get; set; }
        public Operator Op { get; set; }
        public Expression Right { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterBinaryExpression(this);
            Left.Accept(visitor);
            Right.Accept(visitor);
            visitor.LeaveBinaryExpression(this);
        }
    }
}
