namespace Fifth.VirtualMachine.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "int")]
    public class PrimitiveInteger : IFifthType
    {
        public static PrimitiveInteger Default = new PrimitiveInteger();

        private PrimitiveInteger()
        {
        }

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