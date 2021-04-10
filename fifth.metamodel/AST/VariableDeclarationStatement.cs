namespace Fifth.AST
{
    using TypeSystem;
    using Visitors;

    public class VariableDeclarationStatement : Statement, ITypedAstNode
    {
        private string typeName;

        public VariableDeclarationStatement(Identifier name, Expression value)
        {
            Name = name;
            Expression = value;
        }

        public Expression Expression { get; set; }
        public Identifier Name { get; set; }

        public string TypeName
        {
            get
            {
                if (TypeId != null)
                {
                    return TypeId.Lookup().Name;
                }
                return typeName;
            }
            set
            {
                if (!TypeRegistry.DefaultRegistry.TryGetTypeByName(value, out var type))
                {
                    throw new TypeCheckingException("Setting unrecognised type for variable");
                }

                typeName = type.Name; // in case we want to use some sort of mapping onto a "canonical name"
                TypeId = type.TypeId;
            }
        }

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
