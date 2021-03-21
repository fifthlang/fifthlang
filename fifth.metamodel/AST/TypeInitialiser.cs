namespace Fifth.AST
{
    using Parser.LangProcessingPhases;

    public class TypeInitialiser : Expression
    {
        public TypeInitialiser(AstNode parentNode, IFifthType fifthType)
            : base(parentNode, fifthType)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterTypeInitialiser(this);
            visitor.LeaveTypeInitialiser(this);
        }
    }
}
