namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public interface IVisitable
    {
        void Accept(IAstVisitor visitor);
    }
}
