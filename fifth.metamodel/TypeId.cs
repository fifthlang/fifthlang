namespace Fifth
{
    public sealed record TypeId
    {
        public TypeId(ushort id) => Value = id;
        public ushort Value { get; }
    }
}
