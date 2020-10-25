namespace Fifth.VirtualMachine
{
    public interface IVariableAssignment
    {
        object Value { get; set; }
        IFifthType FifthType { get; }
    }
}