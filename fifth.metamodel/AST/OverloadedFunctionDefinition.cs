namespace Fifth.AST
{
    public partial class OverloadedFunctionDefinition
    {
        public ParameterDeclarationList ParameterDeclarations { get; set; }
        public string Typename { get; set; }
        public string Name { get; set; }
        public bool IsEntryPoint { get; set; }
        public TypeId ReturnType { get; set; }
        public TypeId TypeId { get; set; }
        public Block Body { get; set; }
    }

    public partial class BuiltinFunctionDefinition
    {
        public Block Body { get; set; }
    }
}
