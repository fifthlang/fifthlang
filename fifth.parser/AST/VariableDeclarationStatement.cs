namespace Fifth.AST
{
    using Parser.LangProcessingPhases;

    public class VariableDeclarationStatement : Statement
    {
        public Expression Expression { get; set; }
        public Identifier Name { get; set; }
        public IFifthType Type { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterVariableDeclarationStatement(this);
            Name.Accept(visitor);

            Expression?.Accept(visitor);
            visitor.LeaveVariableDeclarationStatement(this);
        }
    }
}
