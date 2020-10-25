using System;
using System.Collections.Generic;

namespace Fifth.VirtualMachine
{
    /// <summary>
    /// Performs fetch, execute and store cycle on stack.
    /// </summary>
    public class Dispatcher : IDispatcher
    {
        /// <summary>
        /// Initializes a new instance of the <a onclick="return false;" href="Dispatcher"
        /// originaltag="see">Dispatcher</a> class.
        /// </summary>
        /// <param name="stack">The stack.</param>
        public Dispatcher(IFuncStack stack) => this.Stack = stack;

        /// <summary>
        /// Gets or sets the instruction stack.
        /// </summary>
        /// <value>The stack.</value>
        public IFuncStack Stack { get; }

        /// <summary>
        /// Dispatch takes a function from the top of the stack, and then attempts to invoke it with
        /// arguments gathered from the stack below
        /// </summary>
        public void Dispatch()
        {
            // if stack empty do nothing
            if (this.Stack.IsEmpty)
            {
                return;
            }

            // pop stack into x
            var x = this.Stack.Pop();
            // if x is a constant then return to stack
            if (x.IsValue)
            {
                this.Stack.Push(x);
            }
            else
            {
                // if x is a func requiring (m) params: resolve m values into values
                var args = new List<object>();
                foreach (var t in x.ArgTypes)
                {
                    object o = this.Resolve();
                    // check that types of values match type requirements of x
                    if (!t.IsInstanceOfType(o))
                    {
                        throw new Exception("Invalid Parameter Type"); /// TODO need better error message
                    }
                    args.Add(o);
                }
                // pass values to x as args
                args.Reverse(); // return to same order they were passsed onto the stack
                var result = x.Invoke(args.ToArray());
                // push result onto stack
                this.Stack.Push(result.AsFun());
            }
        }

        /// <summary>
        /// Resolve, attempts to convert functions at the top of the stack into actual values. If
        /// they are real functions, then it recurses into those functions till it is able to get a
        /// number off the stack to return.
        /// </summary>
        /// <returns>
        /// an object. Either the value on the top of the stack, or the result of dispatching the
        /// function on top of the stack.
        /// </returns>
        private object Resolve()
        {
            if (this.Stack.IsEmpty)
            {
                return null;
            }

            var x = this.Stack.Pop();
            if (x.IsValue)
            {
                return x.Invoke();
            }
            else
            {
                // we can't resolve this value directly, we need to recurse via dispatch
                this.Stack.Push(x);
                this.Dispatch();
                x = this.Stack.Pop();
                return x.Invoke();
            }
        }
    }
}