using System;
using Antlr4.Runtime;

namespace Fifth.Parser
{
    public class Scope : IScope
    {
        public ParserRuleContext AstNode { get; set; }

        public Scope(ParserRuleContext astNode)
        {
            SymbolTable = new SymbolTable();
            AstNode = astNode;
        }
        public Scope(ParserRuleContext astNode, IScope enclosingScope) : this(astNode)
        {
            EnclosingScope = enclosingScope;
        }
        public IScope EnclosingScope { get; set; }
        public ISymbolTable SymbolTable { get; set; }
        public void Declare(string name, SymbolKind kind, ParserRuleContext ctx, params (string, object)[] properties)
        {
            var symTabEntry = new SymTabEntry
            {
                Name = name,
                SymbolKind = kind,
                Line = ctx.Start.Line,
                Context = ctx
            };
            foreach ((string x, object y) in properties)
            {
                symTabEntry.Annotations[x] = y;
            }
            this.SymbolTable[name] = symTabEntry;
        }
    }
}
