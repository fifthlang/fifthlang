namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public class ParameterDeclaration : AstNode
    {
        public string ParameterName { get; set; }
        public IFifthType ParameterType { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterParameterDeclaration(this);
            visitor.LeaveParameterDeclaration(this);
        }
    }
}
