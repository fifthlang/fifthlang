namespace Fifth.AST.Deprecated
{
    using System.Collections.Generic;
    using System.Linq;
    using Visitors;

    public abstract class Statement : AstNode
    {
    }

    public class ExpressionStatement : Statement
    {
        public ExpressionStatement(Expression expression)
        {
            Expression = expression;
        }
        public Expression Expression { get; set; }
        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterExpressionStatement(this);
            Expression.Accept(visitor);
            visitor.LeaveExpressionStatement(this);
        }
    }
    public class StatementList : AstNode
    {
        public List<Statement> Statements { get; }

        public StatementList(IEnumerable<Statement> statements)
            => Statements = statements as List<Statement> ?? statements.ToList();

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterStatementList(this);
            foreach (var e in Statements)
            {
                e.Accept(visitor);
            }

            visitor.LeaveStatementList(this);
        }
    }
}
