namespace Fifth.VirtualMachine
{
    public interface IEnvironment
    {
        IEnvironment Parent { get; }
        bool IsEmpty { get; }

        IVariableAssignment this[IVariableReference index] { get; set; }
    }
}