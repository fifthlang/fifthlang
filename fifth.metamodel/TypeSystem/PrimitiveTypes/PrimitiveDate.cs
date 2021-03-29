namespace Fifth.PrimitiveTypes
{
    using TypeSystem;

    public class PrimitiveDate : PrimitiveAny
    {
        private PrimitiveDate():base(true, false, "date")
        {
        }

        public static PrimitiveDate Default { get; } = new PrimitiveDate();
    }
}
