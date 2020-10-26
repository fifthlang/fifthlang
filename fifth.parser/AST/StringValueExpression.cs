namespace Fifth.AST
{
    public class StringValueExpression : LiteralExpression<string>
    {
        public StringValueExpression(string value) : base(value) { }
    }
}
