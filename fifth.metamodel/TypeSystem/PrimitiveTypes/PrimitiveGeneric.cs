namespace Fifth.PrimitiveTypes
{
    using fifth.metamodel.metadata;
    using TypeSystem;
    using TypeSystem.PrimitiveTypes;

    public abstract class PrimitiveGeneric : PrimitiveAny, IGenericType
    {
        protected PrimitiveGeneric(bool isPrimitive, bool isNumeric, string name, params TypeId[] typeParameters)
            : base(name)
        {
        }

        public TypeId[] GenericTypeParameters { get; }
    }
}
