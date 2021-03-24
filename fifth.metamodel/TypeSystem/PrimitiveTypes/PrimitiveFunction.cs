namespace Fifth.PrimitiveTypes
{
    using TypeSystem;

    public class PrimitiveFunction : PrimitiveAny
    {
        public static PrimitiveFunction Default { get; } = new PrimitiveFunction();
        private PrimitiveFunction():base(true, false, "fun")
        {
        }
    }
}
