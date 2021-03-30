namespace Fifth.TypeSystem.PrimitiveTypes
{
    using Fifth.PrimitiveTypes;
    using TypeSystem;

    public class PrimitiveList : PrimitiveAnyGeneric
    {
        public PrimitiveList(TypeId typeParameter)
            : base(false, false, "list", typeParameter)
        {
        }
    }
}
