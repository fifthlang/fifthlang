namespace Fifth.TypeSystem
{
    public interface ITypeRegistry
    {
        public bool TryGetType(TypeId typeId, out IFifthType type);

        public bool TrySetType(IFifthType type, out TypeId typeId);

        public bool RegisterType(IFifthType type);
    }
}
