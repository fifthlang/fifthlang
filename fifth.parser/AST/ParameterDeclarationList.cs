namespace Fifth.AST
{
    using System.Collections.Generic;
    using Fifth.Parser.LangProcessingPhases;

    public class ParameterDeclarationList : AstNode
    {
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
