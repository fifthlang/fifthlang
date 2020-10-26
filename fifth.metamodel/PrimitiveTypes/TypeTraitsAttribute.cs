namespace Fifth.PrimitiveTypes
{
    using System;

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class TypeTraitsAttribute : Attribute
    {
        public bool IsNumeric { get; set; }
        public bool IsPrimitive { get; set; }
        public string Keyword { get; set; }
    }
}
