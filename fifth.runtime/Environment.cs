using System.Collections.Generic;

namespace Fifth.Runtime
{
    public class Environment : IEnvironment
    {
        public Environment(IEnvironment parent) => Parent = parent;

        public int Count => Variables.Count;
        public bool IsEmpty => Count == 0;
        public IEnvironment Parent { get; set; }
        private Dictionary<string, IValueObject> Variables { get; set; } = new Dictionary<string, IValueObject>();

        public IValueObject this[string index]
        {
            get => Variables[index];
            set => Variables[index] = value;
        }
    }
}
