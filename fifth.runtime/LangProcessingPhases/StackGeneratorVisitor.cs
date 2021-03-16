namespace Fifth.Runtime.LangProcessingPhases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AST;
    using Parser.LangProcessingPhases;

    public class StackGeneratorVisitor : BaseAstVisitor
    {
        public IActivationFrame Frame { get; set; }
        public IStackEmitter Emitter { get; set; }

        public override void EnterFifthProgram(FifthProgram ctx)
        {
            var fpe = new FifthProgramEmitter(ctx);
            fpe.Emit(Emitter, Frame);
        }
    }

}
