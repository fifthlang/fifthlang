namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;
    using TypeSystem;

    public class FunctionDefinition : ScopeAstNode
    {
        public ExpressionList Body { get; set; }
        public string Typename { get; }
        public string Name { get; set; }
        public ParameterDeclarationList ParameterDeclarations { get; set; }
        public IFifthType ReturnType => FifthType;

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterFunctionDefinition(this);
            ParameterDeclarations.Accept(visitor);
            Body.Accept(visitor);
            visitor.LeaveFunctionDefinition(this);
        }

        public FunctionDefinition(string name, ParameterDeclarationList parameterDeclarations, ExpressionList body,
            string typename, IFifthType fifthType) : base(fifthType)
        {
            Name = name;
            ParameterDeclarations = parameterDeclarations;
            Body = body;
            Typename = typename;
        }
    }
}
