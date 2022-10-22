namespace Fifth.TypeSystem.PrimitiveTypes
{
    public abstract class PrimitiveAny : IType
    {
        protected PrimitiveAny(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public TypeId TypeId { get; set; }
        public virtual string IlAbbreviation => String.Empty;
    }
}
