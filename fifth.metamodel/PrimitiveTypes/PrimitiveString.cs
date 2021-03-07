namespace Fifth.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "string")]
    public class PrimitiveString : IFifthType
    {
        private PrimitiveString()
        {
        }

        public static PrimitiveString Default { get; } = new PrimitiveString();

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "+")]
        public static string add_string_string(string left, string right) => left + right;
    }
}
