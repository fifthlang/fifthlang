namespace Fifth.AST
{
    using Fifth;
    using Fifth.Parser.LangProcessingPhases;
    using TypeSystem;

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

        public BinaryExpression(AstNode parentNode, IFifthType fifthType) : base(parentNode, fifthType)
        {
        }

        public BinaryExpression(Expression left, Expression right, Operator op, IFifthType resultType)
            : base(resultType)
        {
            Left = left;
            Right = right;
            Op = op;
        }
    }
}
