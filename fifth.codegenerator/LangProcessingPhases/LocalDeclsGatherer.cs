namespace Fifth.CodeGeneration.LangProcessingPhases
{
    using System.Collections.Generic;
    using AST;
    using AST.Visitors;

    public class LocalDeclsGatherer : BaseAstVisitor
    {
        public List<VariableDeclarationStatement> Decls { get; } = new();

        public override void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx)
        {
            Decls.Add(ctx);
        }
    }
}
