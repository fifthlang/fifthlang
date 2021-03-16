namespace Fifth.Runtime
{
    using Antlr4.Runtime;
    using AST;
    using LangProcessingPhases;
    using Parser.LangProcessingPhases;

    /// <summary>
    ///     Class that co-ordinates the execution of a Fifth Program
    /// </summary>
    public class FifthRuntime
    {
        public int Execute(string fifthProgram)
        {
            var astNode = ParseAndAnnotateProgram(fifthProgram, out var rootActivationFrame);
            var fpe = new FifthProgramEmitter(astNode as FifthProgram);
            fpe.Emit(new StackEmitter(), rootActivationFrame);
            _ = BuiltinFunctions.loadBuiltins(rootActivationFrame.Environment);

            var result = 0;

            if (rootActivationFrame.Environment.TryGetFunctionDefinition("main", out var fd))
            {
                var frame = fd as ActivationFrame;
                frame.ParentFrame = rootActivationFrame;
                frame.Environment.Parent = rootActivationFrame.Environment;
                var d = new Dispatcher(frame);
                d.DispatchWhileOperationIsAtTopOfStack();
                if (!d.Stack.IsEmpty)
                {
                    var tmp = d.Stack.Pop() as ValueStackElement;
                    result = (int)tmp?.GetValueOfValueObject();
                }
            }

            return result;
        }

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
            var annotatedAst = new AstScopeAnnotations(astNode);
            astNode.Accept(new SymbolTableBuilderVisitor(annotatedAst));
            astNode.Accept(new TypeAnnotatorVisitor());
            return astNode;
        }
    }
}
