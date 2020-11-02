using System;

namespace Fifth.Runtime
{
    /// <summary>
    /// A class providing functions that operate on the ActivationFrame itself.
    /// </summary>
    /// <remarks>
    /// This class is singled out so that the dispatcher can detect when a function from it is
    /// placed on the stack. In that way it knows to supply the ActivationFrame as the arguments and
    /// result types
    /// </remarks>
    public static class MetaFunction
    {
        /// <summary>
        /// <para>Performs an assignment within the stack frame</para>
        /// </summary>
        /// <param name="frame">The frame within which the assignment should be performed</param>
        /// <returns>the altered stack frame</returns>
        /// <remarks>
        /// <para>Stack effect: [id, expression] |- []</para>
        /// <para>Environment Effect: [] |- [(id, value)]</para>
        /// <para>
        /// This builtin stack function has the effect of taking an identifier from the top of the
        /// stack, plus the result of evaluating an expression, and creates/updates an entry in the
        /// environment with the value bound to the identifier.
        /// </para>
        /// </remarks>
        public static ActivationFrame Assign(this ActivationFrame frame)
        {
            return frame;
        }

        /// <summary>
        /// <para>Creates a new variable in the environment</para>
        /// </summary>
        /// <param name="frame">The frame within which the assignment should be performed</param>
        /// <returns>the altered stack frame</returns>
        /// <remarks>
        /// <para>Stack effect: [id, expression] |- []</para>
        /// <para>Environment Effect: [] |- [(id, value)]</para>
        /// <para>
        /// This builtin function will create a new entry for the variable in the current environment
        /// </para>
        /// </remarks>
        internal static ActivationFrame CreateVariable(ActivationFrame frame)
        {
            return frame;
        }

        /// <summary>
        /// Looks up the value of a variable in the environment
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <returns>the altered stack frame</returns>
        /// <remarks>
        /// This will resolve the value of the variable and place that onto the stack instead of the reference
        /// </remarks>
        internal static ActivationFrame DereferenceVariable(ActivationFrame frame)
        {
            return frame;
        }
    }
}
