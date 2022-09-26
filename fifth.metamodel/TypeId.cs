namespace Fifth
{
    public sealed record TypeId
    {
        public TypeId(ushort id) => Value = id;
        public ushort Value { get; }
        /// <summary>
        /// The 5th type here, will eventually become a .NET type.  When that is done, this property will hold the type of the generated UDT
        /// </summary>
        public Type? MaterialisedType { get; set; }
    }
}
