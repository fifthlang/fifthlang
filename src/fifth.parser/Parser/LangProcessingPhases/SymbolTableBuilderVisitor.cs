using System;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using static FifthParser;

namespace fifth.parser.Parser
{
    public class SymbolTableBuilderVisitor : FifthBaseListener
    {
        public IScope GlobalScope { get; set; }
        public IScope CurrentScope { get; set; }

        void LeaveNode(ParserRuleContext ctx){
            CurrentScope = CurrentScope.EnclosingScope ?? GlobalScope;
        }
        public override void EnterFifth(FifthContext context)
        {
            // this is the top level entrypoint into the AST. So this is where root scopes etc get set up
            CurrentScope = GlobalScope = new Scope(context);
        }

        public override void ExitFifth([NotNull] FifthContext context)
        {
            LeaveNode(context);
        }

        public override void EnterFunction_declaration([NotNull] FifthParser.Function_declarationContext context) {
            CurrentScope.Declare(context.Start.Text, SymbolKind.FunctionDeclaration, context);
            CurrentScope = new Scope(context, CurrentScope);
        }

        public override void ExitFunction_declaration([NotNull] Function_declarationContext context)
        {
            LeaveNode(context);
        }
        public override void EnterVarDeclStmt([NotNull] VarDeclStmtContext context)
        {
            CurrentScope.Declare(context.Start.Text, SymbolKind.VariableDeclaration, context);
        }
        public override void ExitVarDeclStmt([NotNull] VarDeclStmtContext context)
        {
            LeaveNode(context);
        }
    }
}
