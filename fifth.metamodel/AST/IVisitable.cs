namespace Fifth.AST
{
    using Visitors;

    public interface IVisitable
    {
        void Accept(IAstVisitor visitor);
    }
}
