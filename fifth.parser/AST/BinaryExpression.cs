namespace Fifth.AST
{
    public class BinaryExpression : Expression
    {
        public Expression Left { get; set; }
        public Operator Op { get; set; }
        public Expression Right { get; set; }
    }
}
