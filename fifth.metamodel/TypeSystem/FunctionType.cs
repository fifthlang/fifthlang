namespace Fifth.TypeSystem
{
    public record FunctionSignature : IFunctionSignature
    {
        public FunctionSignature(string name, TypeId returnType, TypeId[] formalParameterTypes)
        {
            Name = name;
            ReturnType = returnType;
            FormalParameterTypes = formalParameterTypes;
        }

        public TypeId ReturnType { get; }
        public TypeId[] FormalParameterTypes { get; }
        public string Name { get; }
        public TypeId TypeId { get; set; }
        public TypeId[] GenericTypeParameters { get; }
    }
}
