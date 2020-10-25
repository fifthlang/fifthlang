namespace Fifth.AST
{
    public abstract class AstNode : IAstNode
    {
        public string Filename { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
    }
}
