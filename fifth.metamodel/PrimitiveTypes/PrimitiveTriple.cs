namespace Fifth.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "triple")]
    public class PrimitiveTriple : IFifthType
    {
        private PrimitiveTriple()
        {
        }

        public static PrimitiveTriple Default { get; } = new PrimitiveTriple();
    }
}
