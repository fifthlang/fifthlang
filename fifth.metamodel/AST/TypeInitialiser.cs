namespace Fifth.AST
{
    using System;
    using Visitors;

    public class TypeInitialiser : Expression
    {
        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterTypeInitialiser(this);
            visitor.LeaveTypeInitialiser(this);
        }
    }

    public class TypeDefinition : Expression
    {
        public override void Accept(IAstVisitor visitor) => throw new NotImplementedException();
    }
}
