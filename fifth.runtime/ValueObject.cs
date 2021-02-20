namespace Fifth.Runtime
{
    using System.Collections.Generic;
    using Antlr4.Runtime.Misc;

    /// <summary>
    ///     A runtime value that can be stored in an environment and shared between variable bindings
    /// </summary>
    public class ValueObject : IValueObject<object>
    {
        public ValueObject(IFifthType fifthType, string name, object value)
        {
            ValueType = fifthType;
            Value = value;
            NamesInScope.Add(name);
        }


        /// <summary>
        ///     A list of names by which this variable is known
        /// </summary>
        public List<string> NamesInScope { get; set; } = new ArrayList<string>(1);

        public object Value { get; set; }
        public IFifthType ValueType { get; }
    }

    /// <summary>
    ///     A runtime value that can be stored in an environment and shared between variable bindings
    /// </summary>
    public class ValueObject<T> : IValueObject<T>
    {
        public ValueObject(IFifthType fifthType, string name, T value)
        {
            ValueType = fifthType;
            NamesInScope.Add(name);
            Value = value;
        }

        public IFifthType ValueType { get; }

        /// <summary>
        ///     A list of names by which this variable is known
        /// </summary>
        public List<string> NamesInScope { get; set; } = new ArrayList<string>(1);

        /// <summary>
        ///     The value in the environment, shared between each of the names defined in scope.
        /// </summary>
        public T Value { get; set; }

    }
}
