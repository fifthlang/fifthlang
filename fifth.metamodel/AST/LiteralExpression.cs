namespace Fifth.AST
{
    using fifth.metamodel.metadata;

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
