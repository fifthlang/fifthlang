namespace Fifth
{
    using System;

    public struct TypeId : IComparable<TypeId>
    {
        public ushort Value { get; }
        public TypeId(ushort id) => Value = id;

        public int CompareTo(TypeId other) => Value.CompareTo(other.Value);

        public override bool Equals(object obj) =>
            obj switch
            {
                TypeId t => t.Value == Value,
                { } => false
            };

        public override int GetHashCode()
            => Value.GetHashCode();
    }

    public sealed class OperatorId : IComparable<OperatorId>
    {
        public OperatorId(ulong value) => Value = value;
        public ulong Value { get; }
        public int CompareTo(OperatorId other) => Value.CompareTo(other.Value);

        public override bool Equals(object obj) =>
            obj switch
            {
                OperatorId o => o.Value == Value,
                { } => false
            };

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}
