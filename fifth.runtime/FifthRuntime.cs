namespace Fifth.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Antlr4.Runtime;
    using AST;
    using LangProcessingPhases;
    using Parser.LangProcessingPhases;

    /// <summary>
    /// Class that co-ordinates the execution of a Fifth Program
    /// </summary>
    public class FifthRuntime
    {
        public int Execute(string fifthProgram)
        {
            var parseTree = GetParserFor(fifthProgram).fifth();
            var visitor = new AstBuilderVisitor();
            var astNode = visitor.Visit(parseTree);
            var rootActivationFrame = new ActivationFrame();
            astNode.Accept(new TypeAnnotatorVisitor());
            var fpe = new FifthProgramEmitter(astNode as FifthProgram);
            fpe.Emit(new StackEmitter(), rootActivationFrame);
            BuiltinFunctions.loadBuiltins(rootActivationFrame.Environment);
            if (rootActivationFrame.Environment.TryGetFunctionDefinition("main", out var fd))
            {
                var frame = fd as ActivationFrame;
                frame.ParentFrame = rootActivationFrame;
                frame.Environment.Parent = rootActivationFrame.Environment;
                var d = new Dispatcher(frame);
                d.Dispatch();
            }

            return 0;
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

    }
}
