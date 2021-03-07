namespace Fifth.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "float")]
    public class PrimitiveFloat : IFifthType
    {
        private PrimitiveFloat()
        {
        }

        public static PrimitiveFloat Default { get; set; } = new PrimitiveFloat();

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "+")]
        public static float add_float_float(float left, float right) => left + right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "/")]
        public static float divide_float_float(float left, float right) => left / right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "*")]
        public static float multiply_float_float(float left, float right) => left * right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "-")]
        public static float subtract_float_float(float left, float right) => left - right;
    }
}
