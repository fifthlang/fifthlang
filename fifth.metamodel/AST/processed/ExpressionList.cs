namespace Fifth.AST.Deprecated
{
    using System;
    using System.Collections.Generic;
    using Visitors;

    public class ExpressionList : TypedAstNode
    {
        public ExpressionList(List<Expression> expressions, TypeId fifthType) : base(fifthType)
        {
            _ = expressions ?? throw new ArgumentNullException(nameof(expressions));
            Expressions = expressions;
        }

        public List<Expression> Expressions { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterExpressionList(this);
            foreach (var e in Expressions)
            {
                e.Accept(visitor);
            }

            visitor.LeaveExpressionList(this);
        }
    }
}
