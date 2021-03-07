namespace Fifth.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public enum StackElementType
    {
        Value, Function, MetaFunction
    }

    /// <summary>A thing that can represent a function at runtime, allowing the import and export of the function</summary>
    public interface IRuntimeFunction
    {
        IEnumerable<StackElement> Export();

        void Import(IEnumerable<StackElement> function);
    }

    /// <summary>
    ///     <para>A representation of a function or block as a stack of StackElement</para>
    /// </summary>
    public class ActivationStack : Stack<StackElement>, IRuntimeFunction, IRuntimeStack
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

        public IRuntimeStack PushConstantValue<T>(T value)
        {
            Push(new ValueStackElement(value));
            return this;
        }

        public IRuntimeStack PushFunction(FuncWrapper f)
        {
            Push(new FunctionStackElement(f));
            return this;
        }

        public IRuntimeStack PushMetaFunction(FuncWrapper f)
        {
            Push(new MetaFunctionStackElement(f));
            return this;
        }

        public bool IsEmpty => Count == 0;

        public IRuntimeStack PushVariableReference(IValueObject variableEntryInEnvironment)
        {
            Push(new VariableReferenceStackElement(variableEntryInEnvironment.NamesInScope.First()));
            return this;
        }
    }

    /// <summary>contract for the runtime stack</summary>
    public interface IRuntimeStack
    {
        bool IsEmpty { get; }
        IRuntimeStack PushConstantValue<T>(T value);
        IRuntimeStack PushFunction(FuncWrapper f);
        IRuntimeStack PushMetaFunction(FuncWrapper f);
        StackElement Pop();
        void Push(StackElement element);
    }

    /// <summary>A stack element consisting of a wrapped lambda function</summary>
    [DebuggerDisplay("λ:{Function.Function}")]
    public class FunctionStackElement : StackElement
    {
        public FunctionStackElement(FuncWrapper function)
            => Function = function;

        public FuncWrapper Function { get; }

        public override bool Equals(object obj) => Equals(obj as StackElement);

        public override int GetHashCode() => Function.GetHashCode();
        public override bool Equals(StackElement other)
        {
            var x = other as FunctionStackElement;
            return x?.Function.Equals(Function) ?? false;
        }
    }

    /// <summary>A stack element consisting of a function that acts on the environment somehow</summary>
    public class MetaFunctionStackElement : StackElement
    {
        public MetaFunctionStackElement(FuncWrapper metaFunction)
            => MetaFunction = metaFunction;

        public FuncWrapper MetaFunction { get; }

        public override bool Equals(object obj) => Equals(obj as StackElement);

        public override int GetHashCode() => MetaFunction.GetHashCode();
        public override bool Equals(StackElement other)
        {
            var x = other as MetaFunctionStackElement;
            return x?.MetaFunction.Equals(MetaFunction) ?? false;
        }
        public override string ToString()
        {
            return $"\\{MetaFunction}";
        }
    }

    /// <summary>Base type of anything that can be pushed onto a stack</summary>
    public abstract class StackElement : IEquatable<StackElement>
    {
        public abstract bool Equals(StackElement other);
    }

    /// <summary>Any element that can be pushed onto a stack</summary>
    [DebuggerDisplay("val:{Value}")]
    public class ValueStackElement : StackElement
    {
        public ValueStackElement(object value)
            => Value = value;

        public object Value { get; }

        public override bool Equals(object obj)
        {
            var other = obj as ValueStackElement;
            if (other == null)
            {
                return false;
            }


            return other.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode() => Value.GetHashCode();
        public override bool Equals(StackElement other)
        {
            var x = other as ValueStackElement;
            return x?.Value.Equals(Value) ?? false;
        }
    }

    [DebuggerDisplay("var:{VariableName}")]
    public class VariableReferenceStackElement : StackElement
    {
        public VariableReferenceStackElement(string variableName)
            => VariableName = variableName;

        public string VariableName { get; }

        public override bool Equals(object obj) => Equals(obj as StackElement);

        public override int GetHashCode() => (nameof(VariableReferenceStackElement) + VariableName).GetHashCode();
        public override bool Equals(StackElement other)
        {
            var x = other as VariableReferenceStackElement;
            return x?.VariableName.Equals(VariableName) ?? false;
        }
    }
}
