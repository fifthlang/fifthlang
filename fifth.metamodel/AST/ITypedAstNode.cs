namespace Fifth.AST
{
    using System.Collections.Generic;

    public interface ITypedAstNode
    {
        TypeId TypeId { get; set; }
    }

    public interface IFunctionCollection
    {
        List<IFunctionDefinition> Functions { get; set; }
        void RemoveFunction(IFunctionDefinition fd);
        void AddFunction(IFunctionDefinition fd);
    }
}
