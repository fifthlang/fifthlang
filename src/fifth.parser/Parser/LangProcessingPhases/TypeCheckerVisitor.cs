using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using fifth.Parser.AST;
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
            var ctxId = context.Start.Text;
            symtab[ctxId] = new SymTabEntry{
                Name = ctxId,
                SymbolKind = SymbolKind.Function,
                Line = context.Start.Line,
                Context = context
            };
            return true;
        }

        public override bool VisitFunction_call([NotNull] Function_callContext context)
        {
            Console.WriteLine("VisitFunction_call");
            return true;
        }
    }
}
