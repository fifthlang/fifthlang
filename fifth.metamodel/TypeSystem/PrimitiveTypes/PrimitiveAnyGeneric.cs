namespace Fifth.PrimitiveTypes
{
    using TypeSystem;

    public abstract class PrimitiveAnyGeneric : PrimitiveAny, IGenericFifthType
    {
        protected PrimitiveAnyGeneric(bool isPrimitive, bool isNumeric, string shortName, params TypeId[] typeParameters)
            : base(isPrimitive, isNumeric, shortName)
        {
        }

        public TypeId[] TypeParameters { get; }
    }
}
