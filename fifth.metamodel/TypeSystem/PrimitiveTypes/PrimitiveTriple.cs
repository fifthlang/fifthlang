namespace Fifth.PrimitiveTypes
{
    using TypeSystem.PrimitiveTypes;

    public class PrimitiveTriple : PrimitiveAny
    {
        private PrimitiveTriple() : base("triple")
        {
        }

        public static PrimitiveTriple Default { get; } = new();
    }
}
