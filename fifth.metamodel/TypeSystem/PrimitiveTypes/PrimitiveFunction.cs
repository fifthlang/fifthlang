namespace Fifth.PrimitiveTypes
{
    using TypeSystem.PrimitiveTypes;

    public class PrimitiveFunction : PrimitiveAny
    {
        private PrimitiveFunction() : base("fun")
        {
        }

        public static PrimitiveFunction Default { get; } = new();
        public override string IlAbbreviation => String.Empty;
    }
}
