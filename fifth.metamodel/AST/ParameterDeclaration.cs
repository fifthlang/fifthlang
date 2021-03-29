namespace Fifth.AST
{
    using Parser.LangProcessingPhases;
    using TypeSystem;

    public class ParameterDeclaration : TypedAstNode
    {
        public ParameterDeclaration(string parameterName, string typeName, IFifthType fifthType)
            : base(fifthType)
        {
            ParameterName = parameterName;
            TypeName = typeName;
        }

        public string ParameterName { get; set; }
        public string TypeName { get; }
        public IFifthType ParameterType => FifthType;

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterParameterDeclaration(this);
            visitor.LeaveParameterDeclaration(this);
        }
    }
}
