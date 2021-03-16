namespace Fifth.PrimitiveTypes
{
#pragma warning disable IDE1006 // Naming Styles

    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "int")]
    public class PrimitiveInteger : IFifthType
    {
        private PrimitiveInteger()
        {
        }

        public static PrimitiveInteger Default { get; set; } = new PrimitiveInteger();

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "+")]
        public static int add_int_int(int left, int right) => left + right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "/")]
        public static int divide_int_int(int left, int right) => left / right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "*")]
        public static int multiply_int_int(int left, int right) => left * right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "-")]
        public static int subtract_int_int(int left, int right) => left - right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = ">=")]
        public static bool greater_than_or_equal_int_int(int left, int right) => left >= right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "<=")]
        public static bool less_than_or_equal_int_int(int left, int right) => left <= right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = ">")]
        public static bool greater_int_int(int left, int right) => left > right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "<")]
        public static bool less_int_int(int left, int right) => left < right;
    }

#pragma warning restore IDE1006 // Naming Styles
}
