namespace Fifth.Runtime
{
    using System;
    using PrimitiveTypes;

    /// <summary>
    ///     A class providing functions that operate on the IDispatcher itself.
    /// </summary>
    /// <remarks>
    ///     This class is singled out so that the dispatcher can detect when a function from it is
    ///     placed on the stack. In that way it knows to supply the IDispatcher as the arguments and
    ///     result types
    /// </remarks>
    public static class MetaFunction
    {
        /// <summary>
        ///     <para>Performs an assignment within the stack frame</para>
        /// </summary>
        /// <param name="dispatcher">The dispatcher containing links to the env within which the assignment should be performed</param>
        /// <returns>the altered stack frame</returns>
        /// <remarks>
        ///     <para>Stack effect: [id, expression] |- []</para>
        ///     <para>Environment Effect: [] |- [(id, value)]</para>
        ///     <para>
        ///         This builtin stack function has the effect of taking an identifier from the top of the
        ///         stack, plus the result of evaluating an expression, and creates/updates an entry in the
        ///         environment with the value bound to the identifier.
        ///     </para>
        /// </remarks>
        public static IDispatcher Assign(this IDispatcher dispatcher)
        {
            var varName = dispatcher.Resolve() as string;
            var rhsValue = dispatcher.Resolve();
            dispatcher.Frame.Environment[varName] = new ValueObject(PrimitiveInteger.Default, "int", rhsValue);
            return dispatcher;
        }

        /// <summary>
        ///     <para>Declares a new unbound variable in the environment</para>
        /// </summary>
        /// <param name="dispatcher">the place where operations are performed</param>
        /// <returns>the altered stack frame</returns>
        /// <remarks>
        ///     <para>Stack effect: [id:string] |- []</para>
        ///     <para>Environment Effect: [] |- [(id, null)]</para>
        ///     <para>
        ///         This builtin function will create a new entry for the variable in the current environment
        ///     </para>
        /// </remarks>
        internal static IDispatcher DeclareVariable(IDispatcher dispatcher)
        {
            _ = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));

            var varName = dispatcher.Resolve() as string;
            var typeName = dispatcher.Resolve() as string;
            IFifthType typeReferencedByTypeName = null;
            if (string.IsNullOrWhiteSpace(typeName) || typeName == "var")
            {
                // OK no type given or implicit type name use
                // either way, we will have to use type inference to work out what type to use.
                throw new NotImplementedException("type inference not yet available");
            }

            typeReferencedByTypeName = LookupTypeDefinitionByName(typeName);
            _ = typeReferencedByTypeName ?? throw new TypeCheckingException("Unable to find type, or no type provided");

            dispatcher.Frame.Environment[varName] =
                new ValueObject<string>(typeReferencedByTypeName, varName, string.Empty);

            // create var ref in place of string, to prove its been created in env
            dispatcher.Frame.Stack.PushVariableReference(dispatcher.Frame.Environment[varName]);
            return dispatcher;
        }

        private static IFifthType LookupTypeDefinitionByName(string typeName) =>
            // TODO: need to have actual registry of types to lookup against
            PrimitiveInteger.Default;

        /// <summary>
        ///     <para>Binds a variable reference to a value</para>
        /// </summary>
        /// <param name="dispatcher">the place where operations are performed</param>
        /// <returns>the altered stack frame</returns>
        /// <remarks>
        ///     <para>Stack effect: [var:variableref, val:value] |- []</para>
        ///     <para>Environment Effect: [(id, ?)] |- [(id, val)]</para>
        ///     <para>
        ///         This builtin function will take an existing entry for a variable in the current
        ///         environment and attach a value from the top of the stack to it
        ///     </para>
        /// </remarks>
        internal static IDispatcher BindVariable(IDispatcher dispatcher)
        {
            _ = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));

            var varName = dispatcher.Resolve() as string;
            var value = dispatcher.Resolve();
            dispatcher.Frame.Environment[varName] =
                new ValueObject(PrimitiveInteger.Default, "int", value); // TODO: WARNING: not type info
            return dispatcher;
        }

        /// <summary>
        ///     Looks up the value of a variable in the environment
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <returns>the altered stack frame</returns>
        /// <remarks>
        ///     This will resolve the value of the variable and place that onto the stack instead of the reference
        /// </remarks>
        internal static IDispatcher DereferenceVariable(IDispatcher dispatcher)
        {
            var varName = dispatcher.Resolve() as string;
            var value = dispatcher.Frame.Environment[varName];
            var valueObj = value.GetValueOfValueObject();
            dispatcher.Frame.Stack.PushConstantValue(valueObj);
            return dispatcher;
        }
    }
}
