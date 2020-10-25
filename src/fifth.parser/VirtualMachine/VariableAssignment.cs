namespace Fifth.VirtualMachine
{
    public class VariableAssignment : IVariableAssignment
    {
        public VariableAssignment(IFifthType fifthType, object value)
        {
            FifthType = fifthType;
            Value = value;
        }

        public IFifthType FifthType { get; }
        public object Value { get; set; }
    }
}