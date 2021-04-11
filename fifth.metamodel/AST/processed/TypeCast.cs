namespace Fifth.AST.Deprecated
{
    using System;
    using Visitors;

    public class TypeCast : Expression
    {
        public TypeCast(Expression subExpression, TypeId targetTid)
        {
            _ = subExpression ?? throw new ArgumentNullException(nameof(subExpression));
            _ = targetTid ?? throw new ArgumentNullException(nameof(targetTid));
            SubExpression = subExpression;
            TargetTid = targetTid;
            TypeId = targetTid;
        }
        public Expression SubExpression { get; }
        public TypeId TargetTid { get; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterTypeCast(this);
            SubExpression.Accept(visitor);
            visitor.LeaveTypeCast(this);
        }
    }
}
