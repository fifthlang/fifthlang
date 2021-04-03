namespace Fifth.PrimitiveTypes
{
    using TypeSystem;
    using TypeSystem.PrimitiveTypes;

    public class PrimitiveDate : PrimitiveAny
    {
        private PrimitiveDate():base("date")
        {
        }

        public static PrimitiveDate Default { get; } = new PrimitiveDate();
    }
}
