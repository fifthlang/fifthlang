namespace Fifth.TypeSystem
{
    public class TypeAlias : ITypeAlias
    {
        public TypeId AliasedTypeId { get; set; }
        public string Name { get; }
        public TypeId TypeId { get; set; }
    }
}
