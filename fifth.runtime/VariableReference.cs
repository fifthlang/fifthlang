namespace Fifth.Runtime
{
    using System;
    using System.Collections.Generic;

    public class VariableReference : IVariableReference, IEquatable<VariableReference>
    {
        public VariableReference(string name) => LocalName = name;

        public string LocalName { get; }

        public static bool operator !=(VariableReference left, VariableReference right) => !(left == right);

        public static bool operator ==(VariableReference left, VariableReference right) => EqualityComparer<VariableReference>.Default.Equals(left, right);

        public override bool Equals(object obj) => Equals(obj as VariableReference);

        public bool Equals(VariableReference other) => other != null &&
                   LocalName == other.LocalName;

        public override int GetHashCode() => LocalName.GetHashCode();
    }
}
