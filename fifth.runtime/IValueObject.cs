namespace Fifth.Runtime
{
    /// <summary>
    /// A runtime value that can be stored in an environment and shared between variable bindings
    /// </summary>
    public interface IValueObject
    {
        object Value { get; set; }

        /// <summary>
        /// Gets the type of the value
        /// </summary>
        /// <value>The type of the value.</value>
        IFifthType ValueType { get; }
    }
}
