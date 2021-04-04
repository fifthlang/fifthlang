namespace Fifth.AST
{
    using Fifth;
    using TypeSystem;
    using Visitors;

    public class BinaryExpression : Expression
    {
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

        public BinaryExpression(AstNode parentNode, TypeId fifthType) : base(parentNode, fifthType)
        {
        }

        public BinaryExpression(Expression left, Expression right, Operator op, TypeId resultType)
            : base(resultType)
        {
            Left = left;
            Right = right;
            Op = op;
        }
    }
}
