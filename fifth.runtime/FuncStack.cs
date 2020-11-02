using System.Collections.Generic;

namespace Fifth
{
    /// <summary>
    /// A stack of functions
    /// </summary>
    public class FuncStack : IFuncStack
    {
        /// <summary>
        /// Initializes a new instance of the <a onclick="return false;" href="FuncStack"
        /// originaltag="see">FuncStack</a> class.
        /// </summary>
        public FuncStack() => Stack = new Stack<FuncWrapper>();

        /// <summary>
        /// Gets a value indicating whether the stack is empty.
        /// </summary>
        /// <value><c>true</c> if this stack is empty; otherwise, <c>false</c>.</value>
        bool IFuncStack.IsEmpty => Stack.Count == 0;

        /// <summary>
        /// Gets the stack.
        /// </summary>
        /// <value>The stack.</value>
        public Stack<FuncWrapper> Stack { get; private set; }

        /// <summary>
        /// Pops a function from the top of the stack.
        /// </summary>
        /// <returns>The function at the top of the stack</returns>
        public FuncWrapper Pop() => Stack.Pop();

        /// <summary>
        /// Pushes a function onto the stack.
        /// </summary>
        /// <param name="funcWrapper">The function wrapper.</param>
        public void Push(FuncWrapper funcWrapper) => Stack.Push(funcWrapper);
    }
}
