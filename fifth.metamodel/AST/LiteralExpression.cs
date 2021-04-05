namespace Fifth.AST
{
    public abstract class LiteralExpression<T> : Expression
    {
        protected LiteralExpression(T value, TypeId type)
        {
            TypeId = type;
            Value = value;
        }

        public T Value { get; set; }
    }
}
