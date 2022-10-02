namespace Fifth.PrimitiveTypes
{
    using TypeSystem;
    using TypeSystem.PrimitiveTypes;

    public class PrimitiveFloat : PrimitiveNumeric
    {
        private PrimitiveFloat() : base("float", 3)
        {
        }

        public static PrimitiveFloat Default { get; set; } = new();

        [Operation(Operator.Add)]
        public static float add_float_float(float left, float right)
        {
            return left + right;
        }

        [Operation(Operator.Divide)]
        public static float divide_float_float(float left, float right)
        {
            return left / right;
        }

        [Operation(Operator.Multiply)]
        public static float multiply_float_float(float left, float right)
        {
            return left * right;
        }

        [Operation(Operator.Subtract)]
        public static float subtract_float_float(float left, float right)
        {
            return left - right;
        }
    }
}
