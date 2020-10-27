namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public abstract class AstNode : IAstNode
    {
        public int Column { get; set; }
        public string Filename { get; set; }
        public int Line { get; set; }

        public abstract void Accept(IAstVisitor visitor);
    }
}
