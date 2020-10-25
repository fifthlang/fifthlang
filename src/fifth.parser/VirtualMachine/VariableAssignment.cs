namespace Fifth.VirtualMachine
{
    public class VariableAssignment : IVariableAssignment
    {
        public VariableAssignment(IFifthType fifthType, object value)
        {
            this.FifthType = fifthType;
            this.Value = value;
        }

        public IFifthType FifthType { get; }
        public object Value { get; set; }
    }
}