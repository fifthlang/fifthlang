namespace Fifth.AST
{
    public interface IAstNode
    {
        string Filename { get; set; }
        int Line { get; set; }
        int Column { get; set; }
    }
}
