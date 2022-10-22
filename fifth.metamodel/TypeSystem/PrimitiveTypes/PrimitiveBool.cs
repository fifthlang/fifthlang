namespace Fifth.PrimitiveTypes
{
    using TypeSystem;
    using TypeSystem.PrimitiveTypes;

    public class PrimitiveBool : PrimitiveAny
    {
        private PrimitiveBool() : base("bool")
        {
        }

        public static PrimitiveBool Default { get; } = new();

        [Operation(Operator.And)]
        public static bool logical_and_bool_bool(bool left, bool right)
        {
            return left && right;
        }

        [Operation(Operator.Or)]
        public static bool logical_or_bool_bool(bool left, bool right)
        {
            return left || right;
        }

        public override string IlAbbreviation => "bool";
    }
}
