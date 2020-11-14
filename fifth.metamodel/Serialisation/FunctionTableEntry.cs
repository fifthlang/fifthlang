namespace Fifth.Serialisation
{
    using System;
    using System.Linq;

    [Serializable]
    public struct FunctionTableEntry
    {
#pragma warning disable IDE1006 // Naming Styles
        public uint functionIdentifier;
        public FunctionTableEntryType functionTableEntryType;
        public uint offset;
        public char[] fullName;
#pragma warning restore IDE1006 // Naming Styles

        public FunctionTableEntry(uint functionIdentifier, FunctionTableEntryType functionTableEntryType, char[] fullName, uint offset)
        {
            this.functionIdentifier = functionIdentifier;
            this.functionTableEntryType = functionTableEntryType;
            this.fullName = fullName;
            this.offset = offset;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            // See the full list of guidelines at http://go.microsoft.com/fwlink/?LinkID=85237 and
            // also the guidance for operator== at http://go.microsoft.com/fwlink/?LinkId=85238

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var rhs = (FunctionTableEntry)obj;

            var v1 = rhs.fullName.SequenceEqual(fullName);
            var v2 = rhs.functionIdentifier == functionIdentifier;
            var v3 = rhs.functionTableEntryType == functionTableEntryType;
            var v4 = rhs.offset == offset;
            return v1 && v2 && v3 && v4;
        }

        // override object.GetHashCode
        public override int GetHashCode() => functionIdentifier.GetHashCode();
    }
}
