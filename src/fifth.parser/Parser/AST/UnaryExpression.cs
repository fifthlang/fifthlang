namespace Fifth.AST
{
    public class UnaryExpression : Expression
    {
        public Operator Op { get; set; }
        public Expression Operand { get; set; }
    }
}
