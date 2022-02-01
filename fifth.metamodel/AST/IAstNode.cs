namespace Fifth.AST
{
    public interface IAstNode : IAnnotated, IVisitable, IFromSourceFile
    {
        AstNode ParentNode { get; set; }
    }
}
