namespace Fifth.TypeSystem
{
    public record FunctionType : IFunctionType
    {
        public FunctionType(TypeId returnType, TypeId[] formalParameterTypes)
        {
            ReturnType = returnType;
            FormalParameterTypes = formalParameterTypes;
        }

        public TypeId ReturnType { get; }
        public TypeId[] FormalParameterTypes { get; }
        public bool IsNumeric { get; }
        public bool IsGeneric { get; }
        public string ShortName { get; }
        public TypeId TypeId { get; set; }
        public bool IsPrimitive { get; }
    }
}
