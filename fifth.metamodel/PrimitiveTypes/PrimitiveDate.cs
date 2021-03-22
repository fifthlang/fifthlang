namespace Fifth.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "date")]
    public class PrimitiveDate : IFifthType
    {
        private PrimitiveDate()
        {
        }

        public static PrimitiveDate Default { get; } = new PrimitiveDate();
    }
}
