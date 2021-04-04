namespace Fifth.AST
{
    using TypeSystem;
    using Visitors;

    public class ParameterDeclaration : TypedAstNode
    {
        public ParameterDeclaration(string parameterName, string typeName, TypeId fifthType)
            : base(fifthType)
        {
            ParameterName = parameterName;
            TypeName = typeName;
        }

        public string ParameterName { get; set; }
        public string TypeName { get; }
        public TypeId ParameterType => TypeId;

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterParameterDeclaration(this);
            visitor.LeaveParameterDeclaration(this);
        }
    }
}
