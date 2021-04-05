namespace Fifth.TypeSystem
{
    public interface IType
    {
        public string Name { get; }
        public TypeId TypeId { get; set; } // no more than 64K types
    }

    public interface ITypeAlias : IType
    {
        public TypeId AliasedTypeId { get; }
    }

    public interface IGenericType : IType
    {
        public TypeId[] TypeParameters { get; }
    }

    public interface IFunctionSignature : IGenericType
    {
        public TypeId[] FormalParameterTypes { get; }
        TypeId ReturnType { get; }
    }
}
