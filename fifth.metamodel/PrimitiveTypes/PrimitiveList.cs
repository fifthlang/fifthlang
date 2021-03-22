namespace Fifth.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "list")]
    public class PrimitiveList : IFifthType
    {
        private PrimitiveList()
        {
        }

        public static PrimitiveList Default { get; } = new PrimitiveList();
    }
}
