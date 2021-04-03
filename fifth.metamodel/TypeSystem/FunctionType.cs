namespace Fifth.TypeSystem
{
    public record FunctionSignature : IFunctionSignature
    {
        public FunctionSignature(TypeId returnType, TypeId[] formalParameterTypes)
        {
            ReturnType = returnType;
            FormalParameterTypes = formalParameterTypes;
        }

        public TypeId ReturnType { get; }
        public TypeId[] FormalParameterTypes { get; }
        public string Name { get; }
        public TypeId TypeId { get; set; }
        public TypeId[] TypeParameters { get; }
    }
}
