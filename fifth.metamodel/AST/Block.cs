namespace Fifth.AST
{
    using System.Collections.Generic;
    using Fifth.Parser.LangProcessingPhases;
    using PrimitiveTypes;
    using Symbols;
    using TypeSystem;

    public class Block : ScopeAstNode
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

        public Block(AstNode parentNode, TypeId fifthType)
            : base(parentNode, fifthType, parentNode as IScope)
        {
        }

        public Block(AstNode parentNode)
            : this(parentNode, (TypeId)null)
        {
        }

        public Block(AstNode parentNode, ExpressionList expressionList)
            : this(parentNode, expressionList?.TypeId)
        {
            Expressions = expressionList.Expressions;
        }
    }
}
