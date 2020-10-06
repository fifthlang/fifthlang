using System;

namespace fifth.VirtualMachine.PrimitiveTypes
{
    [System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    internal sealed class OperatorTraitsAttribute : Attribute
    {
        public string OperatorRepresentation { get; set; }
        public OperatorPosition Position { get; set; }
    }

    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class TypeTraitsAttribute : Attribute
    {
        public bool IsNumeric { get; set; }
        public bool IsPrimitive { get; set; }
        public string Keyword { get; set; }
    }
}