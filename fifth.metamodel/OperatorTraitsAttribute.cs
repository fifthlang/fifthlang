namespace Fifth
{
    using System;

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    internal sealed class OperatorTraitsAttribute : Attribute
    {
        public string OperatorRepresentation { get; set; }
        public OperatorPosition Position { get; set; }
    }
}
