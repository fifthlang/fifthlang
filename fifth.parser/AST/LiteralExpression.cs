namespace Fifth.AST
{
    public abstract class LiteralExpression<T> : Expression
    {
        public LiteralExpression(T value) => Value = value;

        public T Value { get; set; }
    }
}
