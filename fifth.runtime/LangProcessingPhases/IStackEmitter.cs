namespace Fifth.Runtime.LangProcessingPhases
{
    using System;
    using AST;

    public interface IStackEmitter
    {
        /// <summary>
        ///     Adds the wrapped function to the stack
        /// </summary>
        /// <param name="stack">the stack into which elements are emitted</param>
        /// <param name="fs">
        ///     A variable length param array of wrapped functions to be pushed onto the stack
        /// </param>
        void Emit(IRuntimeStack stack, params StackElement[] fs);

        void Operator(IRuntimeStack stack, BinaryExpression ctx);

        /// <summary>
        ///     Adds a meta function to the stack
        /// </summary>
        /// <param name="metafunc">The metafunction to be pushed</param>
        void Value(IRuntimeStack stack, object v);
        void VariableReference(IRuntimeStack stack, string v);

        void BinaryFunction<T1, T2, TR>(IRuntimeStack stack, Func<T1, T2, TR> f);
        void UnaryFunction<T1, TR>(IRuntimeStack stack, Func<T1, TR> f);
        void MetaFunction(IRuntimeStack stack, Func<IDispatcher, IDispatcher> metafunction);
    }
}
