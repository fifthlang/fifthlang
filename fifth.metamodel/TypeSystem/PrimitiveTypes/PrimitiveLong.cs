namespace Fifth.PrimitiveTypes
{
    using TypeSystem;
    using TypeSystem.PrimitiveTypes;

    public class PrimitiveLong : PrimitiveNumeric
    {
        private PrimitiveLong() : base("long", 2)
        {
        }

        public static PrimitiveLong Default { get; set; } = new();

        [Operation(Operator.Add)]
        public static long Add(long left, long right) => left + right;

        [Operation(Operator.Divide)]
        public static long Divide(long left, long right) => left / right;

        [Operation(Operator.Multiply)]
        public static long Multiply(long left, long right) => left * right;

        [Operation(Operator.Subtract)]
        public static long Subtract(long left, long right) => left - right;
    }
}
