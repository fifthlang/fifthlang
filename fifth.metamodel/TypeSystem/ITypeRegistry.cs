namespace Fifth.TypeSystem
{
    using fifth.metamodel.metadata;

    public interface ITypeRegistry
    {
        public bool TryGetType(TypeId typeId, out IType type);

        public bool TrySetType(IType type, out TypeId typeId);

        public bool RegisterType(IType type);
    }
}
