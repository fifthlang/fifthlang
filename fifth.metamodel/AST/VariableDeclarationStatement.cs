namespace Fifth.AST
{
    using TypeSystem;
    using Visitors;

    public class VariableDeclarationStatement : Statement, ITypedAstNode
    {
        public VariableDeclarationStatement(Identifier name, Expression value)
        {
            Name = name;
            Expression = value;
        }

        public Expression Expression { get; set; }
        public Identifier Name { get; set; }
        public string TypeName => TypeId.Lookup()?.Name;
        public TypeId TypeId { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterVariableDeclarationStatement(this);
            Name.Accept(visitor);

            Expression?.Accept(visitor);
            visitor.LeaveVariableDeclarationStatement(this);
        }
    }
}
