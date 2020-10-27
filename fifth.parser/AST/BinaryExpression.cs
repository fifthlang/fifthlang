namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

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
    }
}
