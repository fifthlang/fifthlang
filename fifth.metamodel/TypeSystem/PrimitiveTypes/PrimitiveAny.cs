namespace Fifth.TypeSystem.PrimitiveTypes
{
    using TypeSystem;

    public abstract class PrimitiveAny : IType
    {
        protected PrimitiveAny(string name) => Name = name;

        public string Name { get; }
        public TypeId TypeId { get; set; }
    }
}
