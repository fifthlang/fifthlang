namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public class VariableDeclarationStatement : Statement
    {
        public string Name { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterVariableDeclarationStatement(this);
            visitor.LeaveVariableDeclarationStatement(this);
        }
    }
}
