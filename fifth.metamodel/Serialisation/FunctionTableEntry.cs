namespace Fifth.Serialisation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public class FunctionTable
    {
        public FunctionTableEntry this[uint index]
        {
            get => table[index];
            set => table[index] = value;
        }

        private readonly Dictionary<uint, FunctionTableEntry> table = new Dictionary<uint, FunctionTableEntry>();

        // override object.Equals
        public override bool Equals(object obj)
        {
            // See the full list of guidelines at http://go.microsoft.com/fwlink/?LinkID=85237 and
            // also the guidance for operator== at http://go.microsoft.com/fwlink/?LinkId=85238

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var rhs = (FunctionTable)obj;

            return table.SequenceEqual(rhs.table);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            throw new NotImplementedException();
            return base.GetHashCode();
        }
    }

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
