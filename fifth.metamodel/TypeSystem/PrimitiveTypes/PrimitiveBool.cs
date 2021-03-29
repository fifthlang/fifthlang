namespace Fifth.PrimitiveTypes
{
    using TypeSystem;

    public class PrimitiveBool : PrimitiveAny
    {
        private PrimitiveBool():base(true, false, "bool")
        {
        }

        public static PrimitiveBool Default { get; } = new PrimitiveBool();

        [Operation(Operator.And)]
        public static bool logical_and_bool_bool(bool left, bool right) => left && right;

        [Operation(Operator.Or)]
        public static bool logical_or_bool_bool(bool left, bool right) => left || right;
    }
}
