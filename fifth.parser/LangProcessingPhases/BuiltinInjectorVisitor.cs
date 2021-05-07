namespace Fifth.LangProcessingPhases
{
    using System.Collections.Generic;
    using AST;
    using AST.Visitors;
    using PrimitiveTypes;
    using TypeSystem.PrimitiveTypes;

    /// <summary>
    /// A visitor that injects builtin things into the AST.
    /// </summary>
    public class BuiltinInjectorVisitor : BaseAstVisitor
    {
        public override void EnterFifthProgram(FifthProgram ctx)
        {
            ctx.Functions.Add(new BuiltinFunctionDefinition("print", "void", ("value", "string")));
            ctx.Functions.Add(new BuiltinFunctionDefinition("write", "void", ("value", "string")));
        }
    }
}
