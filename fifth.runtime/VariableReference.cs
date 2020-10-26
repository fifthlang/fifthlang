namespace Fifth.Runtime
{
    using System;
    using System.Collections.Generic;

    public class VariableReference : IVariableReference, IEquatable<VariableReference>
    {
        public VariableReference(string name) => this.LocalName = name;

        public string LocalName { get; }

        public static bool operator !=(VariableReference left, VariableReference right) => !(left == right);

        public static bool operator ==(VariableReference left, VariableReference right) => EqualityComparer<VariableReference>.Default.Equals(left, right);

        public override bool Equals(object obj) => this.Equals(obj as VariableReference);

        public bool Equals(VariableReference other) => other != null &&
                   this.LocalName == other.LocalName;

        public override int GetHashCode() => this.LocalName.GetHashCode();
    }
}
