namespace Fifth.AST.Deprecated
{
    using Visitors;

    public interface IVisitable
    {
        void Accept(IAstVisitor visitor);
    }
}
