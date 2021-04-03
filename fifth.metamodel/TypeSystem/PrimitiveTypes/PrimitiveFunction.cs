namespace Fifth.PrimitiveTypes
{
    using TypeSystem;
    using TypeSystem.PrimitiveTypes;

    public class PrimitiveFunction : PrimitiveAny
    {
        public static PrimitiveFunction Default { get; } = new PrimitiveFunction();
        private PrimitiveFunction():base("fun")
        {
        }
    }
}
