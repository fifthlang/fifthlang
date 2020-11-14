namespace Fifth.Runtime
{
    using System.Collections.Generic;

    public enum StackElementType
    {
        Value, Function, MetaFunction
    }

    public interface IRuntimeFunction
    {
        IEnumerable<StackElement> Export();

        void Import(IEnumerable<StackElement> function);
    }

    public class ActivationStack : Stack<StackElement>, IRuntimeFunction
    {
        public IEnumerable<StackElement> Export()
            => new List<StackElement>(this);

        public void Import(IEnumerable<StackElement> function)
        {
            foreach (var i in function)
            {
                Push(i);
            }
        }
    }

    public class FunctionStackElement : StackElement
    {
        public FunctionStackElement(FuncWrapper function)
            => Function = function;

        public FuncWrapper Function { get; }
    }

    public class MetaFunctionStackElement : StackElement
    {
        public MetaFunctionStackElement(FuncWrapper metaFunction)
            => MetaFunction = metaFunction;

        public FuncWrapper MetaFunction { get; }
    }

    public abstract class StackElement
    {
    }

    public class ValueStackElement : StackElement
    {
        public ValueStackElement(object value)
            => Value = value;

        public object Value { get; }
    }
}
