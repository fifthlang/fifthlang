namespace Fifth.TypeSystem.PrimitiveTypes
{
    public class PrimitiveUri : PrimitiveAny
    {
        private PrimitiveUri() : base("uri")
        {
        }

        public static PrimitiveUri Default { get; } = new();
    }
}
