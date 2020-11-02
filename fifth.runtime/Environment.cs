using System.Collections.Generic;

namespace Fifth.Runtime
{
    public class Environment : IEnvironment
    {
        public Environment(IEnvironment parent) => Parent = parent;

        public bool IsEmpty => Variables.Count == 0;
        public IEnvironment Parent { get; set; }
        private Dictionary<IVariableReference, IValueObject> Variables { get; set; } = new Dictionary<IVariableReference, IValueObject>();

        public IValueObject this[IVariableReference index]
        {
            get => Variables[index];
            set => Variables[index] = value;
        }
    }
}
