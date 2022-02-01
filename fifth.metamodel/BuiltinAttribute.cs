namespace Fifth
{
    using System;

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class BuiltinAttribute : Attribute
    {
        public string Keyword { get; set; }
    }
}
