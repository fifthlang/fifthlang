namespace Fifth.Runtime
{
    using System.Collections.Generic;

    public interface IEnvironment
    {
        int Count { get; }
        bool IsEmpty { get; }
        IEnvironment Parent { get; }
        Dictionary<string, IValueObject> Variables { get; }
        Dictionary<string, IFunctionDefinition> Definitions { get; }
        IValueObject this[string index] { get; set; }
        bool TryGetVariableValue(string index, out IValueObject value);
        void AddFunctionDefinition(IFunctionDefinition fd);
        bool TryGetFunctionDefinition(string index, out IFunctionDefinition value);
        bool TrySetVariableValue(string index, IValueObject value);
    }
}
