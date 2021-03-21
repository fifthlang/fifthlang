namespace Fifth.AST
{
    using Parser.LangProcessingPhases;

    public class ParameterDeclaration : TypedAstNode
    {
        public ParameterDeclaration(string parameterName, IFifthType fifthType)
            : base(fifthType) =>
            ParameterName = parameterName;

        public string ParameterName { get; set; }
        public IFifthType ParameterType => FifthType;

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterParameterDeclaration(this);
            visitor.LeaveParameterDeclaration(this);
        }
    }
}
