namespace Fifth.PrimitiveTypes
{
    using TypeSystem;

    public abstract class PrimitiveAnyGeneric : PrimitiveAny, IGenericFifthType
    {
        protected PrimitiveAnyGeneric(bool isPrimitive, bool isNumeric, string shortName, params IFifthType[] typeParameters)
            : base(isPrimitive, isNumeric, shortName)
        {
        }

        public IFifthType[] TypeParameters { get; }
    }
}
