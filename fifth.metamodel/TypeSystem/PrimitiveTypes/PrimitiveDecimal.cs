namespace Fifth.PrimitiveTypes
{
    using TypeSystem;
    using TypeSystem.PrimitiveTypes;

    public class PrimitiveDecimal : PrimitiveNumeric
    {
        private PrimitiveDecimal() : base("decimal", 5)
        {
        }

        public static PrimitiveDecimal Default { get; set; } = new();

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
