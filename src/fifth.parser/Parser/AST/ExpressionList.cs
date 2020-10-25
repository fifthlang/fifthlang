using System.Collections.Generic;

namespace Fifth.AST
{
    public class ExpressionList : AstNode
    {
        public List<Expression> Expressions { get; set; }
    }
}
