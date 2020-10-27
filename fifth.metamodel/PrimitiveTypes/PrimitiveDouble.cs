namespace Fifth.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "double")]
    public class PrimitiveDouble : IFifthType
    {
        private PrimitiveDouble()
        {
        }

        public static PrimitiveDouble Default { get; set; } = new PrimitiveDouble();

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "+")]
        public static double Add(double left, double right) => left + right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "/")]
        public static double Divide(double left, double right) => left / right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "*")]
        public static double Multiply(double left, double right) => left * right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "-")]
        public static double Subtract(double left, double right) => left - right;
    }
}