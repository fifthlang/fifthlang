namespace Fifth.TypeSystem
{
    using System;

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class TypeTraitsIgnoreAttribute : Attribute
    {
        public bool IsNumeric { get; set; }
        public bool IsPrimitive { get; set; }
        public bool IsGeneric { get; set; }
        public string Keyword { get; set; }
    }
}
