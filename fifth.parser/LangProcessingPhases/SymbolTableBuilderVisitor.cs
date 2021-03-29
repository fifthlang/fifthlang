namespace Fifth.Parser.LangProcessingPhases
{
    using Fifth.AST;
    using Symbols;
    using TypeSystem;

    public class SymbolTableBuilderVisitor : BaseAstVisitor
    {
        public override void EnterFunctionDefinition(FunctionDefinition ctx)
        {
            // Declare(ctx.Name, SymbolKind.FunctionDeclaration, ctx);
            var enclosingScope = ctx.ParentNode.NearestScope();
            enclosingScope.Declare(ctx.Name, SymbolKind.FunctionDeclaration, ctx);
            if (TypeRegistry.DefaultRegistry.TryGetTypeByName(ctx.Typename, out var t))
            {
                ctx.FifthType = t;
            }
        }

        public override void EnterParameterDeclaration(ParameterDeclaration ctx)
            => Declare(ctx.ParameterName, SymbolKind.FormalParameter, ctx);

        public override void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx)
            => Declare(ctx.Name.Value, SymbolKind.VariableDeclaration, ctx);

        private void Declare<T>(string name, SymbolKind kind, T ctx, params (string, object)[] properties)
        where T : AstNode
        {
            var enclosingScope = ctx.NearestScope();
            enclosingScope.Declare(name, kind, ctx, properties);
        }
    }
}
