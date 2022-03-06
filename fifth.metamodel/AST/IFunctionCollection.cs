namespace Fifth.AST
{
    using System.Collections.Generic;

    public interface IFunctionCollection
    {
        List<IFunctionDefinition> Functions { get; set; }

        void AddFunction(IFunctionDefinition fd);

        void RemoveFunction(IFunctionDefinition fd);
    }
}
