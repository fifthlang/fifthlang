namespace Fifth.Runtime
{
    public interface IEnvironment
    {
        bool IsEmpty { get; }
        IEnvironment Parent { get; }
        IValueObject this[IVariableReference index] { get; set; }
    }
}
