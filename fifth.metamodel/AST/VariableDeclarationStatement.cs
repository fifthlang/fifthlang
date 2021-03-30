namespace Fifth.AST
{
    using Parser.LangProcessingPhases;
    using TypeSystem;

    public class VariableDeclarationStatement : Statement
    {
        public VariableDeclarationStatement(AstNode parentNode, TypeId fifthType)
            : base(parentNode, fifthType)
        {
        }

        public VariableDeclarationStatement(Identifier name, Expression value, TypeId fifthType)
            : base(fifthType)
        {
            Name = name;
            Expression = value;
        }

        public Expression Expression { get; set; }
        public Identifier Name { get; set; }
        public string TypeName => FifthType.Lookup()?.ShortName;

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterVariableDeclarationStatement(this);
            Name.Accept(visitor);

            Expression?.Accept(visitor);
            visitor.LeaveVariableDeclarationStatement(this);
        }
    }
}
