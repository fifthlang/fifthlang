using System;
using System.Collections.Generic;

namespace Fifth
{
    /// <summary>
    /// A wrapper around any sort of function, to make it easier to extract and perform type
    /// checking on its type parameters and result type.
    /// </summary>
    /// <remarks>
    /// This becomes important when type checking is performed on functions that have been pushed
    /// onto the stack. The interpreter needs to know whether the types of the items below the
    /// function on the stack are compatible with the function. It could make use of reflection, but
    /// this is much simpler, and probably faster.
    /// </remarks>
    public class FuncWrapper
    {
        /// <summary>
        /// Initializes a new instance of the <a onclick="return false;" href="FuncWrapper"
        /// originaltag="see">FuncWrapper</a> class.
        /// </summary>
        /// <param name="argTypes">The types of the arguments that the function accepts.</param>
        /// <param name="resultType">Type of the result the function returns.</param>
        /// <param name="f">The function itself.</param>
        public FuncWrapper(List<Type> argTypes, Type resultType, Delegate f)
        {
            this.ArgTypes = argTypes;
            this.ResultType = resultType;
            Function = f;
        }

        /// <summary>
        /// Gets the argument types accepted by the function.
        /// </summary>
        /// <value>A <see cref="List{T}"/> of <see cref="Type"/>.</value>
        public List<Type> ArgTypes { get; }

        /// <summary>
        /// Gets the function, in the form of a Delegate.
        /// </summary>
        /// <value>The function.</value>
        public Delegate Function { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is a function wrapping a constant literal.
        /// </summary>
        /// <value><c>true</c> if this instance is a value; otherwise, <c>false</c>.</value>
        public bool IsValue => ArgTypes.Count == 0;

        /// <summary>
        /// Gets the <see cref="Type"/> of the result returned by the Function.
        /// </summary>
        /// <value>The result type.</value>
        public Type ResultType { get; }

        /// <summary>
        /// Invokes the Function with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments to pass to the function.</param>
        /// <returns>an object of the same type as ResultType</returns>
        public object Invoke(params object[] args)
        {
            return this.Function.DynamicInvoke(args);
        }
    }
}