namespace Fifth.Runtime
{
    public interface IEnvironment
    {
        bool IsEmpty { get; }
        IEnvironment Parent { get; }
        IVariableAssignment this[IVariableReference index] { get; set; }
    }
}
