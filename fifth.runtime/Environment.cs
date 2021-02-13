namespace Fifth.Runtime
{
    using System.Collections.Generic;

    /// <summary>
    ///     The container for the runtime registration of variable values associated with some scope
    /// </summary>
    public class Environment : IEnvironment
    {
        public Environment(IEnvironment parent) => Parent = parent;
        private Dictionary<string, IValueObject> Variables { get; } = new Dictionary<string, IValueObject>();

        public int Count => Variables.Count;
        public bool IsEmpty => Count == 0;
        public IEnvironment Parent { get; set; }

        public IValueObject this[string index]
        {
            get => Variables[index];
            set => Variables[index] = value;
        }
    }
}
