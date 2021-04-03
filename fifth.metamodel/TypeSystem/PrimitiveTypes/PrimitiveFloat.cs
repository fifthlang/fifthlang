namespace Fifth.PrimitiveTypes
{
    using TypeSystem;
    using TypeSystem.PrimitiveTypes;

    public class PrimitiveFloat : PrimitiveAny
    {
        private PrimitiveFloat():base("float")
        {
        }

        public static PrimitiveFloat Default { get; set; } = new PrimitiveFloat();

        [Operation(Operator.Add)]
        public static float add_float_float(float left, float right) => left + right;

        [Operation(Operator.Divide)]
        public static float divide_float_float(float left, float right) => left / right;

        [Operation(Operator.Multiply)]
        public static float multiply_float_float(float left, float right) => left * right;

        [Operation(Operator.Subtract)]
        public static float subtract_float_float(float left, float right) => left - right;
    }
}
