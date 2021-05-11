namespace Fifth.AST
{
    public interface IFunctionDefinition : IVisitable, IAstNode
    {
        public ParameterDeclarationList ParameterDeclarations{get;set;}
        public string Typename{get;set;}
        public string Name{get;set;}
        public bool IsEntryPoint{get;set;}
        public TypeId ReturnType{get;set;}
        Block Body { get; set; }
    }
}
