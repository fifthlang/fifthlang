using System;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using static FifthParser;

namespace fifth.Parser
{
    public class SymbolTableBuilderVisitor : FifthParserBaseVisitor<bool>
    {
        private readonly ISymbolTable symtab;

        public SymbolTableBuilderVisitor(ISymbolTable symtab)
        {
            this.symtab = symtab;
        }
        public override bool VisitFunction_declaration([NotNull] FifthParser.Function_declarationContext context) {
            Declare(context.Start.Text, SymbolKind.FunctionDeclaration, context);
            return base.VisitFunction_declaration(context);
        }
        public override bool VisitVarDeclStmt([NotNull] VarDeclStmtContext context)
        {
            return base.VisitVarDeclStmt(context);
        }
        public override bool VisitFunction_call([NotNull] Function_callContext context)
        {
            // var id = String.Join(".", context.children.Select(c => c.GetText()).ToArray());
            // Declare(id, SymbolKind.FunctionReference, context);
            return base.VisitFunction_call(context);
        }

        public void Declare(string name, SymbolKind kind, ParserRuleContext ctx){
            symtab[name] = new SymTabEntry{
                Name = name,
                SymbolKind = kind,
                Line = ctx.Start.Line,
                Context = ctx
            };

        }
    }
}
