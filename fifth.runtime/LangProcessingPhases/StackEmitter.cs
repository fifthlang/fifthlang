namespace Fifth.Runtime.LangProcessingPhases
{
    using System;
    using AST;

    /*
    public class EmitBuilder
    {
        private readonly IRuntimeStack runtimeStack;
        internal readonly Queue<StackElement> tmpQueue;

        private EmitBuilder(IRuntimeStack runtimeStack)
        {
            this.runtimeStack = runtimeStack;
            tmpQueue = new Queue<StackElement>();
        }

        public static EmitBuilder Into(IRuntimeStack stack) => new EmitBuilder(stack);

        public EmitBuilder VariableDeclaration(VariableDeclarationStatement variableDeclarationStatement) => this;
    }*/

    public class StackEmitter : IStackEmitter
    {
        /// <summary>
        ///     Adds the wrapped function to the stack
        /// </summary>
        /// <param name="stack">the stack into which elements are emitted</param>
        /// <param name="fs">
        ///     A variable length param array of wrapped functions to be pushed onto the stack
        /// </param>
        public void Emit(IRuntimeStack stack, params StackElement[] fs)
        {
            foreach (var f in fs)
            {
                stack.Push(f);
            }
        }

        public void Operator(IRuntimeStack stack, BinaryExpression ctx)
        {
            var lhsType = LookupType(ctx.Left);
            var rhsType = LookupType(ctx.Right);

            if (!TypeHelpers.TryGetOperatorByNameAndTypes(ctx.Op, lhsType, rhsType, out var operatorFunction))
            {
                throw new TypeCheckingException(
                    $"operator {ctx.Op} not supported between {lhsType.Name} and {rhsType.Name}.");
            }

            Emit(stack, new FunctionStackElement(operatorFunction));
        }

        /// <summary>
        ///     Adds a meta function to the stack
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="v"></param>
        public void Value(IRuntimeStack stack, object v)
            => Emit(stack, new ValueStackElement(v));

        public void BinaryFunction<T1, T2, TR>(IRuntimeStack stack, Func<T1, T2, TR> f)
            => Emit(stack, new FunctionStackElement(Fun.Wrap(f)));

        public void UnaryFunction<T1, TR>(IRuntimeStack stack, Func<T1, TR> f)
            => Emit(stack, new FunctionStackElement(Fun.Wrap(f)));

        public void MetaFunction(IRuntimeStack stack, Func<IDispatcher, IDispatcher> metafunc)
            => Emit(stack, new MetaFunctionStackElement(Fun.Wrap(metafunc)));

        private Type LookupType(Expression e) => e["type"] as Type;
    }
}
