namespace Fifth.Runtime
{
    using System;
    using Antlr4.Runtime;
    using AST;
    using Fifth.LangProcessingPhases;
    using LangProcessingPhases;
    using Parser.LangProcessingPhases;
    using PrimitiveTypes;
    using TypeSystem;

    /// <summary>
    ///     Class that co-ordinates the execution of a Fifth Program
    /// </summary>
    public class FifthRuntime
    {
        public string Execute(string fifthProgram, params object[] args)
        {
            TypeRegistry.DefaultRegistry.LoadPrimitiveTypes();
            InbuiltOperatorRegistry.DefaultRegistry.LoadBuiltinOperators();
            var astNode = ParseAndAnnotateProgram(fifthProgram, out var rootActivationFrame);
            var fpe = new FifthProgramEmitter(astNode as FifthProgram);
            fpe.Emit(new StackEmitter(), rootActivationFrame);
            BuiltinFunctions.loadBuiltins(rootActivationFrame.Environment);

            var result = String.Empty;

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

        protected static FifthParser GetParserFor(string fragment)
        {
            var lexer = new FifthLexer(new AntlrInputStream(fragment));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new ThrowingErrorListener<int>());

            var parser = new FifthParser(new CommonTokenStream(lexer));
            parser.RemoveErrorListeners();
            parser.AddErrorListener(new ThrowingErrorListener<IToken>());
            return parser;
        }

        private static IAstNode ParseAndAnnotateProgram(string fifthProgram, out ActivationFrame rootActivationFrame)
        {
            var parseTree = GetParserFor(fifthProgram).fifth();
            var visitor = new AstBuilderVisitor();
            var astNode = visitor.Visit(parseTree);
            rootActivationFrame = new ActivationFrame();
            astNode.Accept(new VerticalLinkageVisitor());
            astNode.Accept(new DesugaringVisitor());
            astNode.Accept(new SymbolTableBuilderVisitor());
            astNode.Accept(new TypeAnnotatorVisitor());
            return astNode;
        }
    }
}
