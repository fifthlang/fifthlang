namespace Fifth.Parser.LangProcessingPhases
{
    using Antlr4.Runtime;
    using Antlr4.Runtime.Misc;
    using static FifthParser;

    public class SymbolTableBuilderVisitor : FifthBaseListener
    {
        public SymbolTableBuilderVisitor(AnnotatedSyntaxTree ast) => this.Ast = ast;

        public AnnotatedSyntaxTree Ast { get; set; }
        public IScope CurrentScope { get; set; }
        public IScope GlobalScope { get; set; }

        public override void EnterFifth(FifthContext context) =>
            // this is the top level entrypoint into the AST. So this is where root scopes etc get
            // set up
            this.CurrentScope = this.GlobalScope = this.Ast.CreateNewScope(context);

        public override void EnterFunction_declaration([NotNull] FifthParser.Function_declarationContext context)
        {
            this.CurrentScope.Declare(context.Start.Text, SymbolKind.FunctionDeclaration, context);
            this.CurrentScope = this.Ast.CreateNewScope(context, this.CurrentScope);
        }

        public override void EnterParameter_declaration([NotNull] Parameter_declarationContext context)
        {
            var varType = context.GetChild<Parameter_typeContext>(0);
            var varName = context.children[1];
            this.CurrentScope.Declare(varName.GetText(), SymbolKind.FormalParameter, context, ("type_name", varType.GetText()));
            base.EnterParameter_declaration(context);
        }

        public override void EnterVarDeclStmt([NotNull] VarDeclStmtContext context) => this.CurrentScope.Declare(context.Start.Text, SymbolKind.VariableDeclaration, context);

        public override void ExitFifth([NotNull] FifthContext context)
        => this.LeaveNode(context);

        public override void ExitFunction_declaration([NotNull] Function_declarationContext context)
        => this.LeaveNode(context);

        public override void ExitVarDeclStmt([NotNull] VarDeclStmtContext context)
        => this.LeaveNode(context);

        private void LeaveNode(ParserRuleContext ctx) => this.CurrentScope = this.CurrentScope.EnclosingScope ?? this.GlobalScope;
    }
}
