using System.Collections.Generic;

namespace Fifth.Runtime
{
    public class Environment : IEnvironment
    {
        public Environment(IEnvironment parent) => this.Parent = parent;

        public bool IsEmpty => this.Variables.Count == 0;
        public IEnvironment Parent { get; }
        private Dictionary<IVariableReference, IVariableAssignment> Variables { get; set; } = new Dictionary<IVariableReference, IVariableAssignment>();

        public IVariableAssignment this[IVariableReference index]
        {
            get => this.Variables[index];
            set => this.Variables[index] = value;
        }
    }
}
