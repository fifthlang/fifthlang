namespace fifth.VirtualMachine
{
    public interface IVariableAssignment
    {
        object Value { get; set; }
        IFifthType FifthType { get; }
    }
}