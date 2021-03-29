namespace Fifth.Runtime
{
    using System.Collections.Generic;
    using System.Reflection;
    using Antlr4.Runtime.Misc;
    using TypeSystem;

    /// <summary>
    ///     The container for the runtime registration of variable values associated with some scope
    /// </summary>
    public class Environment : IEnvironment
    {
        public Environment(IEnvironment parent) => Parent = parent;

        public Dictionary<string, IValueObject> Variables { get; } = new Dictionary<string, IValueObject>();

        public Dictionary<string, IFunctionDefinition> Definitions { get; } =
            new Dictionary<string, IFunctionDefinition>();

        public int Count => Variables.Count;
        public bool IsEmpty => Count == 0;
        public IEnvironment Parent { get; set; }

        public bool TryGetVariableValue(string index, out IValueObject value)
        {
            if (Variables.TryGetValue(index, out value))
            {
                return true;
            }

            return Parent?.TryGetVariableValue(index, out value) ?? false;
        }

        public bool TrySetVariableValue(string index, IValueObject value)
        {
            if (Variables.ContainsKey(index))
            {
                Variables[index] = value;
                return true;
            }

            return Parent?.TrySetVariableValue(index, value) ?? false;
        }

        public bool TryGetFunctionDefinition(string index, out IFunctionDefinition value)
        {
            if (Definitions.TryGetValue(index, out value))
            {
                return true;
            }

            return Parent?.TryGetFunctionDefinition(index, out value) ?? false;
        }

        public void AddFunctionDefinition(IFunctionDefinition fd, string name)
        {
            if (Definitions.ContainsKey(name))
            {
                throw new TypeCheckingException("Duplication Function Definition");
            }

            Definitions[name] = fd;
        }

        public void AddFunctionDefinition(IFunctionDefinition fd) => AddFunctionDefinition(fd, fd.Name);

        public IValueObject this[string index]
        {
            get
            {
                if (!TryGetVariableValue(index, out _))
                {
                    throw new InvalidVariableReferenceException(index);
                }

                return Variables[index];
            }
            set => Variables[index] = value;
        }
    }

    public interface ITypedThing
    {
        public string Name { get; set; }
        public IFifthType Type { get; set; }
    }

    public interface IFunctionArgument : ITypedThing
    {
        public int ArgOrdinal { get; set; }
    }

    public class FunctionArgument : IFunctionArgument
    {
        public string Name { get; set; }
        public IFifthType Type { get; set; }
        public int ArgOrdinal { get; set; }
    }

    public interface IFunctionDefinition : ITypedThing
    {
        public ArrayList<IFunctionArgument> Arguments { get; }
    }

    public class BuiltinFunctionDefinition : IFunctionDefinition
    {
        public BuiltinFunctionDefinition(MethodInfo mi)
        {
            Name = mi.Name;
            var parameters = mi.GetParameters();
            Arguments = new ArrayList<IFunctionArgument>(parameters.Length);
            var ord = 0;
            foreach (var parameter in parameters)
            {
                if (!TypeHelpers.TryGetNearestFifthTypeToNativeType(parameter.ParameterType, out var fifthType))
                {
                    throw new TypeCheckingException("Unable to find type");
                }

                Arguments.Add(new FunctionArgument { ArgOrdinal = ord++, Name = parameter.Name, Type = fifthType });
            }

            if (!TypeHelpers.TryGetNearestFifthTypeToNativeType(mi.ReturnType, out var returnType))
            {
                throw new TypeCheckingException("Unable to find return type");
            }

            Type = returnType;
            Function = mi.Wrap();
        }

        public string Name { get; set; }
        public IFifthType Type { get; set; }

        public ArrayList<IFunctionArgument> Arguments { get; }

        //TODO: most of the properties can hang off of the Function's metadata
        public FuncWrapper Function { get; set; }
    }

    public class RuntimeFunctionDefinition : ActivationFrame, IFunctionDefinition
    {
        public RuntimeFunctionDefinition()
        {
        }

        public RuntimeFunctionDefinition(IActivationFrame parent)
        {
            ParentFrame = parent as ActivationFrame;
            Environment.Parent = parent.Environment;
            KnowledgeGraph.ParentGraph = parent.KnowledgeGraph;
        }

        public string Name { get; set; }
        public IFifthType Type { get; set; }
        public ArrayList<IFunctionArgument> Arguments { get; } = new ArrayList<IFunctionArgument>();
    }
}
