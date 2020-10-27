namespace Fifth.Parser.LangProcessingPhases
{
    using System;
    using Fifth.AST;

    public class SymbolTableBuilderVisitor : BaseAstVisitor
    {
        public SymbolTableBuilderVisitor(AnnotatedSyntaxTree ast)
            => Ast = ast;

        public AnnotatedSyntaxTree Ast { get; set; }
        public IScope CurrentScope { get; set; }
        public IScope GlobalScope { get; set; }

        public override void EnterFifthProgram(FifthProgram ctx)
            => CurrentScope = GlobalScope = Ast.CreateNewScope(ctx);

        public override void EnterFunctionDefinition(FunctionDefinition ctx)
        {
            Declare(ctx.Name, SymbolKind.FunctionDeclaration, ctx);
            EnterScope(ctx);
        }

        public override void EnterParameterDeclaration(ParameterDeclaration ctx)
            => Declare(ctx.ParameterName, SymbolKind.FormalParameter, ctx, ("type_name", ctx.ParameterType));

        public override void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx)
            => Declare(ctx.Name, SymbolKind.VariableDeclaration, ctx);

        public override void LeaveFifthProgram(FifthProgram ctx)
            => LeaveScope();

        public override void LeaveFunctionDefinition(FunctionDefinition ctx)
            => LeaveScope();

        private void Declare(string name, SymbolKind kind, IAstNode ctx, params (string, object)[] properties)
            => CurrentScope.Declare(name, kind, ctx, properties);

        private void EnterScope(IAstNode ctx)
            => CurrentScope = Ast.CreateNewScope(ctx, CurrentScope);

        private void LeaveScope()
            => CurrentScope = CurrentScope.EnclosingScope ?? GlobalScope;
    }
}
