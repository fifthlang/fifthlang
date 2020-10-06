namespace fifth.VirtualMachine.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "string")]
    public class PrimitiveString : IFifthType
    {
        public static readonly PrimitiveString Default = new PrimitiveString();

        private PrimitiveString()
        {
        }

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "+")]
        public static string Add(string left, string right) => left + right;
    }
}