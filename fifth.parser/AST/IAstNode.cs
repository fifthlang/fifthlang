namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public interface IAstNode
    {
        int Column { get; set; }

        string Filename { get; set; }

        int Line { get; set; }

        void Accept(IAstVisitor visitor);
    }
}
