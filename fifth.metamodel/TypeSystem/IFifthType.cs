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
        public IFifthType[] TypeParameters { get; }
    }

    public interface IFunctionType : IFifthType
    {
        public IFifthType[] FormalParameterTypes { get; }
        IFifthType ReturnType { get; }
    }
}
