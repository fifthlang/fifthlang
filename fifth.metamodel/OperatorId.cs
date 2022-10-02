namespace Fifth
{
    using System;

    public sealed record OperatorId
    {
        public OperatorId(ulong value)
        {
            Value = value;
        }

        public ulong Value { get; }
    }
}
