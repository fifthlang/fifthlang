namespace Fifth.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "char")]
    public class PrimitiveChar : IFifthType
    {
        private PrimitiveChar()
        {
        }

        public static PrimitiveChar Default { get; set; } = new PrimitiveChar();

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "+")]
        public static string Add(char left, char right) => $"{left}{right}";
    }
}
