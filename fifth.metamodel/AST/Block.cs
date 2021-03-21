namespace Fifth.AST
{
    using System.Collections.Generic;
    using Fifth.Parser.LangProcessingPhases;
    using PrimitiveTypes;
    using Symbols;

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

        public Block(AstNode parentNode, IFifthType fifthType)
            : base(parentNode, fifthType, parentNode as IScope)
        {
        }

        public Block(AstNode parentNode)
            : this(parentNode, PrimitiveVoid.Default)
        {
        }

        public Block(AstNode parentNode, ExpressionList expressionList)
            : this(parentNode, expressionList?.FifthType)
        {
            Expressions = expressionList.Expressions;
        }
    }
}
