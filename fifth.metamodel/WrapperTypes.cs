namespace Fifth
{
    public sealed record TypeId
    {
        public TypeId(ushort id) => Value = id;
        public ushort Value { get; }
    }

    public sealed record OperatorId
    {
        public OperatorId(ulong value) => Value = value;
        public ulong Value { get; }
    }
}
