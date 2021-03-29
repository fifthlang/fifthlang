namespace Fifth.PrimitiveTypes
{
    using System.Collections.Generic;
    using TypeSystem;

    public abstract class PrimitiveAny : IFifthType
    {
        public bool IsPrimitive { get; }
        protected PrimitiveAny(bool isPrimitive, bool isNumeric, string shortName)
        {
            IsPrimitive = isPrimitive;
            IsNumeric = isNumeric;
            ShortName = shortName;
        }

        public bool IsNumeric { get; }
        public bool IsGeneric { get; }
        public string ShortName { get; }
        public TypeId TypeId { get; set; }
    }
}
