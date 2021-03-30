namespace Fifth.TypeSystem
{
    using System.Collections.Generic;

    public interface IFifthType
    {
        public bool IsNumeric { get; }
        public bool IsGeneric { get; }
        public string ShortName { get; }
        public TypeId TypeId { get; set; } // no more than 64K types
        bool IsPrimitive { get; }
    }

    public interface IGenericFifthType : IFifthType
    {
        public TypeId[] TypeParameters { get; }
    }

    public interface IFunctionType : IFifthType
    {
        public TypeId[] FormalParameterTypes { get; }
        TypeId ReturnType { get; }
    }
}
