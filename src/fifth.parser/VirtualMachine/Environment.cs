using System.Collections.Generic;

namespace Fifth.VirtualMachine
{
    public class Environment : IEnvironment
    {
        public Environment(IEnvironment parent)
        {
            Parent = parent;
        }

        public bool IsEmpty { get => this.Variables.Count == 0; }
        public IEnvironment Parent { get; }
        private Dictionary<IVariableReference, IVariableAssignment> Variables { get; set; } = new Dictionary<IVariableReference, IVariableAssignment>();

        public IVariableAssignment this[IVariableReference index]
        {
            get => Variables[index];
            set => Variables[index] = value;
        }
    }
}