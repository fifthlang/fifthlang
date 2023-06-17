namespace Fifth.TypeSystem.PrimitiveTypes
{
    public abstract class PrimitiveAny : IType
    {
        protected PrimitiveAny(string name, string? @namespace = default)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Namespace = @namespace ?? string.Empty;
        }

        public string Name { get; }
        public string Namespace { get; }
        public TypeId TypeId { get; set; }
        public virtual string IlAbbreviation => string.Empty;
    }
}
