namespace Fifth.TypeSystem.PrimitiveTypes
{
    using Fifth.PrimitiveTypes;
    using TypeSystem;

    public class PrimitiveList : PrimitiveAnyGeneric
    {
        public PrimitiveList(IFifthType typeParameter)
            : base(false, false, "list", typeParameter)
        {
        }
    }
}
