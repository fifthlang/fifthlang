namespace Fifth.AST
{
    using System.Collections.Generic;
    using Visitors;

    public class ParameterDeclarationList : AstNode
    {
        public ParameterDeclarationList(List<ParameterDeclaration> parameterDeclarations) : base()
        {
            ParameterDeclarations = parameterDeclarations;
        }

        public List<ParameterDeclaration> ParameterDeclarations { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterParameterDeclarationList(this);
            foreach (var pd in ParameterDeclarations)
            {
                pd.Accept(visitor);
            }

            visitor.LeaveParameterDeclarationList(this);
        }
    }
}
