namespace Fifth.AST
{
    using System;
    using Parser.LangProcessingPhases;
    using TypeSystem;

    public class TypeInitialiser : Expression
    {
        public TypeInitialiser(AstNode parentNode, TypeId fifthType)
            : base(parentNode, fifthType)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterTypeInitialiser(this);
            visitor.LeaveTypeInitialiser(this);
        }
    }
    public class TypeDefinition : Expression
    {
        public TypeDefinition(AstNode parentNode, TypeId fifthType)
            : base(parentNode, fifthType)
        {
        }

        public override void Accept(IAstVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
