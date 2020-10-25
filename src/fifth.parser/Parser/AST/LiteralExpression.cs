namespace Fifth.AST
{
    public class LiteralExpression<T> : Expression
    {
        public LiteralExpression(T value) => this.Value = value;

        public T Value { get; set; }
    }
}
