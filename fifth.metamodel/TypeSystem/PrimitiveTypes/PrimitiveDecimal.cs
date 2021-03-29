namespace Fifth.PrimitiveTypes
{
    using TypeSystem;

    public class PrimitiveDecimal : PrimitiveAny
    {
        private PrimitiveDecimal():base(true, true, "decimal")
        {
        }

        public static PrimitiveDecimal Default { get; set; } = new PrimitiveDecimal();

        [Operation(Operator.Add)]
        public static decimal Add(decimal left, decimal right) => left + right;

        [Operation(Operator.Divide)]
        public static decimal Divide(decimal left, decimal right) => left / right;

        [Operation(Operator.Multiply)]
        public static decimal Multiply(decimal left, decimal right) => left * right;

        [Operation(Operator.Subtract)]
        public static decimal Subtract(decimal left, decimal right) => left - right;
    }
}
