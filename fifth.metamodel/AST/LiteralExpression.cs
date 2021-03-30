namespace Fifth.AST
{
    using TypeSystem;

    public abstract class LiteralExpression<T> : Expression
    {
        protected LiteralExpression(T value, AstNode parentNode, TypeId type)
            : base(parentNode, type) => Value = value;

        protected LiteralExpression(T value, TypeId type)
            : base(type) => Value = value;

        public T Value { get; set; }
    }
}
