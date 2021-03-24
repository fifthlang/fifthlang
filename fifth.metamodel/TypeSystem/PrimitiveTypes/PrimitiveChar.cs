namespace Fifth.PrimitiveTypes
{
    using TypeSystem;

    public class PrimitiveChar : PrimitiveAny
    {
        private PrimitiveChar():base(true, false, "char")
        {
        }

        public static PrimitiveChar Default { get; set; } = new PrimitiveChar();

        [Operation(Operator.Add)]
        public static string Add(char left, char right) => $"{left}{right}";
    }
}
