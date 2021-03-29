namespace Fifth.TypeSystem.PrimitiveTypes
{
    using Fifth.PrimitiveTypes;

    public class PrimitiveUri : PrimitiveAny
    {
        private PrimitiveUri() : base(true, false, "uri")
        {
        }

        public static PrimitiveUri Default { get; } = new();
    }
}
