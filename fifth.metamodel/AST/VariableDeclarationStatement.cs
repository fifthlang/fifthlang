namespace Fifth.AST
{
    using Parser.LangProcessingPhases;

    public class VariableDeclarationStatement : Statement
    {
        public VariableDeclarationStatement(AstNode parentNode, IFifthType fifthType)
            : base(parentNode, fifthType)
        {
        }

        public VariableDeclarationStatement(Identifier name, Expression value, IFifthType fifthType)
            : base(fifthType)
        {
            Name = name;
            Expression = value;
        }

        public Expression Expression { get; set; }
        public Identifier Name { get; set; }
        public string TypeName => FifthType.GetTypeName();

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterVariableDeclarationStatement(this);
            Name.Accept(visitor);

            Expression?.Accept(visitor);
            visitor.LeaveVariableDeclarationStatement(this);
        }
    }
}
