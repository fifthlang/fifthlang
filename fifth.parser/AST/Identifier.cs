namespace Fifth.AST
{
    public class Identifier : AstNode
    {
        public string Value { get; set; }

    }
    public class IdentifierExpression : Expression
    {
        public Identifier Identifier { get; set; }

    }
}
