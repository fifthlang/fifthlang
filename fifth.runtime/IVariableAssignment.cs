namespace Fifth.Runtime
{
    public interface IVariableAssignment
    {
        IFifthType FifthType { get; }
        object Value { get; set; }
    }
}
