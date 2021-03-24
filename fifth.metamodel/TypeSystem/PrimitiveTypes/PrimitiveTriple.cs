namespace Fifth.PrimitiveTypes
{
    using TypeSystem;

    public class PrimitiveTriple : PrimitiveAny
    {
        private PrimitiveTriple():base(true, false, "triple")
        {
        }

        public static PrimitiveTriple Default { get; } = new PrimitiveTriple();
    }
}
