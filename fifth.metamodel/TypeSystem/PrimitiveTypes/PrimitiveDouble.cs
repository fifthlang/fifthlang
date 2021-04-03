namespace Fifth.PrimitiveTypes
{
    using TypeSystem;
    using TypeSystem.PrimitiveTypes;

    public class PrimitiveDouble : PrimitiveAny
    {
        private PrimitiveDouble():base("double")
        {
        }
        public static PrimitiveDouble Default { get; set; } = new PrimitiveDouble();

        [Operation(Operator.Add)]
        public static double Add(double left, double right) => left + right;

        [Operation(Operator.Divide)]
        public static double Divide(double left, double right) => left / right;

        [Operation(Operator.Multiply)]
        public static double Multiply(double left, double right) => left * right;

        [Operation(Operator.Subtract)]
        public static double Subtract(double left, double right) => left - right;
    }
}
