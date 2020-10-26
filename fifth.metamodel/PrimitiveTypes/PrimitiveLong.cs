namespace Fifth.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "long")]
    public class PrimitiveLong : IFifthType
    {
        private PrimitiveLong()
        {
        }

        public static PrimitiveLong Default { get; set; } = new PrimitiveLong();

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "+")]
        public static long Add(long left, long right) => left + right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "/")]
        public static long Divide(long left, long right) => left / right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "*")]
        public static long Multiply(long left, long right) => left * right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "-")]
        public static long Subtract(long left, long right) => left - right;
    }
}
