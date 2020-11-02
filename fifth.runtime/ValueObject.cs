namespace Fifth.Runtime
{
    /// <summary>
    /// A runtime value that can be stored in an environment and shared between variable bindings
    /// </summary>
    public class ValueObject : IValueObject
    {
        public ValueObject(IFifthType fifthType, object value)
        {
            ValueType = fifthType;
            Value = value;
        }

        public object Value { get; set; }

        /// <summary>
        /// Gets the type of the value
        /// </summary>
        /// <value>The type of the value.</value>
        public IFifthType ValueType { get; }
    }
}
