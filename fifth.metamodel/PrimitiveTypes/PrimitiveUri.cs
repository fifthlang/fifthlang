namespace Fifth.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "uri")]
    public class PrimitiveUri : IFifthType
    {
        private PrimitiveUri()
        {
        }

        public static PrimitiveUri Default { get; } = new PrimitiveUri();
    }
}
