namespace Fifth.AST
{
    using System.Collections.Generic;
    using Fifth.Parser.LangProcessingPhases;

    public class Block : AstNode
    {
        public List<Expression> Expressions { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterBlock(this);
            foreach (var e in Expressions)
            {
                e.Accept(visitor);
            }
            visitor.LeaveBlock(this);
        }
    }
}
