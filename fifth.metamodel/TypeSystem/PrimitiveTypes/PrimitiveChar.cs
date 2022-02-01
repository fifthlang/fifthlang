namespace Fifth.PrimitiveTypes
{
    using TypeSystem;
    using TypeSystem.PrimitiveTypes;

    public class PrimitiveChar : PrimitiveAny
    {
        private PrimitiveChar() : base("char")
        {
        }

        public static PrimitiveChar Default { get; set; } = new();

        [Operation(Operator.Add)]
        public static string Add(char left, char right) => $"{left}{right}";
    }
}
