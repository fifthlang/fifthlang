using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using static FifthParser;

namespace Fifth.Parser
{
    public class SymbolTableBuilderVisitor : FifthBaseListener
    {
        public AnnotatedSyntaxTree Ast { get; set; }
        public SymbolTableBuilderVisitor(AnnotatedSyntaxTree ast)
        {
            this.Ast = ast;
        }
        public IScope GlobalScope { get; set; }
        public IScope CurrentScope { get; set; }

        void LeaveNode(ParserRuleContext ctx)
        {
            CurrentScope = CurrentScope.EnclosingScope ?? GlobalScope;
        }
        public override void EnterFifth(FifthContext context)
        {
            // this is the top level entrypoint into the AST. So this is where root scopes etc get set up
            CurrentScope = GlobalScope = Ast.CreateNewScope(context);
        }

        public override void ExitFifth([NotNull] FifthContext context)
        => LeaveNode(context);

        public override void EnterFunction_declaration([NotNull] FifthParser.Function_declarationContext context)
        {
            CurrentScope.Declare(context.Start.Text, SymbolKind.FunctionDeclaration, context);
            CurrentScope = Ast.CreateNewScope(context, CurrentScope);
        }

        public override void ExitFunction_declaration([NotNull] Function_declarationContext context)
        => LeaveNode(context);
        public override void EnterVarDeclStmt([NotNull] VarDeclStmtContext context)
        {
            CurrentScope.Declare(context.Start.Text, SymbolKind.VariableDeclaration, context);
        }
        public override void ExitVarDeclStmt([NotNull] VarDeclStmtContext context)
        => LeaveNode(context);

        public override void EnterParameter_declaration([NotNull] Parameter_declarationContext context)
        {
            var varType = context.GetChild<Parameter_typeContext>(0);
            var varName = context.children[1];
            CurrentScope.Declare(varName.GetText(), SymbolKind.FormalParameter, context, ("type_name", (object)varType.GetText()));
            base.EnterParameter_declaration(context);
        }
    }
}
