namespace Fifth.AST
{
    using Visitors;

    public interface IParameterListItem : IVisitable, ITypedAstNode
    {
        Identifier ParameterName { get; set; }
        string TypeName { get; set; }
        Expression Constraint { get; set; }
    }

}
