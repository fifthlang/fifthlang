namespace fifth.VirtualMachine.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "char")]
    public class PrimitiveChar : IFifthType
    {
        public static PrimitiveChar Default = new PrimitiveChar();

        private PrimitiveChar()
        {
        }

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "+")]
        public static string Add(char left, char right) => $"{left}{right}";
    }
}