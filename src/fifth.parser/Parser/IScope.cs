namespace Fifth.Parser
{
    using Antlr4.Runtime;

    public interface IScope
    {
        IScope EnclosingScope { get; set; }
        ISymbolTable SymbolTable { get; set; }
        ParserRuleContext AstNode { get; set; }
        void Declare(string name, SymbolKind kind, ParserRuleContext ctx, params (string, object)[] properties);
    }
}
