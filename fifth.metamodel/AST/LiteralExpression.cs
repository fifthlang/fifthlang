namespace Fifth.AST
{
    public abstract class LiteralExpression<T> : Expression
    {
        protected LiteralExpression(T value, AstNode parentNode, IFifthType type)
            : base(parentNode, type) => Value = value;

        protected LiteralExpression(T value, IFifthType type)
            : base(type) => Value = value;

        public T Value { get; set; }
    }
}
