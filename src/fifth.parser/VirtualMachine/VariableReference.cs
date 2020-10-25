using System;
using System.Collections.Generic;

namespace Fifth.VirtualMachine
{
    public class VariableReference : IVariableReference, IEquatable<VariableReference>
    {
        public VariableReference(string name)
        {
            LocalName = name;
        }

        public string LocalName { get; }

        public static bool operator !=(VariableReference left, VariableReference right)
        {
            return !(left == right);
        }

        public static bool operator ==(VariableReference left, VariableReference right)
        {
            return EqualityComparer<VariableReference>.Default.Equals(left, right);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as VariableReference);
        }

        public bool Equals(VariableReference other)
        {
            return other != null &&
                   LocalName == other.LocalName;
        }

        public override int GetHashCode()
        {
            return LocalName.GetHashCode();
        }
    }
}