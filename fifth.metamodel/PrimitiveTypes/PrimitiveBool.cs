namespace Fifth.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "bool")]
    public class PrimitiveBool : IFifthType
    {
        private PrimitiveBool()
        {
        }

        public static PrimitiveBool Default { get; } = new PrimitiveBool();

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "&&")]
        public static bool logical_and_bool_bool(bool left, bool right) => left && right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "||")]
        public static bool logical_or_bool_bool(bool left, bool right) => left || right;
    }
}
