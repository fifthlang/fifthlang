namespace Fifth.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "int")]
    public class PrimitiveInteger : IFifthType
    {
        private PrimitiveInteger()
        {
        }

        public static PrimitiveInteger Default { get; set; } = new PrimitiveInteger();

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "+")]
        public static int Add(int left, int right) => left + right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "/")]
        public static int Divide(int left, int right) => left / right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "*")]
        public static int Multiply(int left, int right) => left * right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "-")]
        public static int Subtract(int left, int right) => left - right;
    }
}
