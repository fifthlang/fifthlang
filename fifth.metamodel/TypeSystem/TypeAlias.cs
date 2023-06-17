namespace Fifth.TypeSystem
{
    public class TypeAlias : ITypeAlias
    {
        public TypeId AliasedTypeId { get; set; }
        public string Name { get; }
        public string Namespace { get; }
        public TypeId TypeId { get; set; }
    }
}
