using Fifth.AST;

namespace Fifth.Parser
{
    public interface ISymbolTableEntry : IAnnotated, IFromSourceFile
    {
        string Name { get; set; }
        SymbolKind SymbolKind { get; set; }
        IAstNode Context { get; set; }
    }
}
