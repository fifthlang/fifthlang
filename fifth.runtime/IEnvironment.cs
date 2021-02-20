namespace Fifth.Runtime
{
    public interface IEnvironment
    {
        int Count { get; }
        bool IsEmpty { get; }
        IEnvironment Parent { get; }
        IValueObject this[string index] { get; set; }
        bool TryGetVariableValue(string index, out IValueObject value);
    }
}
