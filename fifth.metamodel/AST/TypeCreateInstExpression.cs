namespace Fifth.AST
{
    using Parser.LangProcessingPhases;
    using TypeSystem;

    public class TypeCreateInstExpression : Expression
    {
        public TypeCreateInstExpression(AstNode parentNode, TypeId fifthType)
            : base(parentNode, fifthType)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterTypeCreateInstExpression(this);
            visitor.LeaveTypeCreateInstExpression(this);
        }
    }
}
