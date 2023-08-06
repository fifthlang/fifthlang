namespace Fifth.TypeSystem
{
    using fifth.metamodel.metadata;

    public class TypeAlias : ITypeAlias
    {
        public TypeId AliasedTypeId { get; set; }
        public string Name { get; }
        public string Namespace { get; }
        public TypeId TypeId { get; set; }
    }
}
