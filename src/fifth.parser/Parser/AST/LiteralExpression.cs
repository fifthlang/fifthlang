namespace Fifth.AST
{
    public class LiteralExpression<T> : Expression
    {
        public LiteralExpression(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}
