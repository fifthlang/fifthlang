namespace Fifth.PrimitiveTypes
{
    using TypeSystem;
    using TypeSystem.PrimitiveTypes;

    public class PrimitiveLong : PrimitiveAny
    {
        private PrimitiveLong():base("long")
        {
        }

        public static PrimitiveLong Default { get; set; } = new PrimitiveLong();

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
