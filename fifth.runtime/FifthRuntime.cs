namespace Fifth.Runtime
{
    using System;
    using Antlr4.Runtime;
    using AST;
    using Fifth.LangProcessingPhases;
    using LangProcessingPhases;
    using Parser.LangProcessingPhases;
    using TypeSystem;

    /// <summary>
    ///     Class that co-ordinates the execution of a Fifth Program
    /// </summary>
    public class FifthRuntime
    {
        public string Execute(string fifthProgram, params object[] args)
        {
            var result = string.Empty;

            if (FifthParserManager.TryParse<FifthProgram>(fifthProgram, out var ast, out var errors))
            {
                var rootActivationFrame = new ActivationFrame();
                var fpe = new FifthProgramEmitter(ast as FifthProgram);
                fpe.Emit(new StackEmitter(), rootActivationFrame);
                BuiltinFunctions.loadBuiltins(rootActivationFrame.Environment);
 
                if (rootActivationFrame.Environment.TryGetFunctionDefinition("main", out var fd))
                {
                    var frame = fd as ActivationFrame;
                    frame.ParentFrame = rootActivationFrame;
                    frame.Environment.Parent = rootActivationFrame.Environment;
                    if (args != null && args.Length > 0)
                    {
                        LoadArgs(frame.Environment, fd, args);
                    }

                    var d = new Dispatcher(frame);
                    d.DispatchWhileOperationIsAtTopOfStack();
                    if (!d.Stack.IsEmpty)
                    {
                        var tmp = d.Stack.Pop() as ValueStackElement;
                        // whatever the result may be, we have to try to convert it to string, since that is the default return type of main
                        result = Convert.ToString(tmp?.GetValueOfValueObject());
                    }
                }

            }
            return result;
        }

        internal void LoadArgs(Environment e, IFunctionDefinition fd, object[] args)
        {
            for (var i = 0; i < fd.Arguments.Count; i++)
            {
                var argType = args[i].GetType();
                if (TypeHelpers.TryGetNearestFifthTypeToNativeType(argType, out var ft))
                {
                    var name = fd.Arguments[i].Name;
                    e[name] = new ValueObject(ft, name, args[i]);
                }
            }
        }

        public string Execute(string fifthProgram) => Execute(fifthProgram, null);
    }
}
