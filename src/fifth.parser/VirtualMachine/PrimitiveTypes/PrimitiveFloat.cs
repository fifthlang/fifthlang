namespace fifth.VirtualMachine.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "float")]
    public class PrimitiveFloat : IFifthType
    {
        public static PrimitiveFloat Default = new PrimitiveFloat();

        private PrimitiveFloat()
        {
        }

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "+")]
        public static float Add(float left, float right) => left + right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "/")]
        public static float Divide(float left, float right) => left / right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "*")]
        public static float Multiply(float left, float right) => left * right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "-")]
        public static float Subtract(float left, float right) => left - right;
    }
}