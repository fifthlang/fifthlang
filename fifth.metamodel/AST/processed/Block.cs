namespace Fifth.AST
{
    using System.Collections.Generic;
    using Visitors;

    public class Block : ScopeAstNode
    {
        public Block(StatementList statements) =>
            Statements = statements.Statements;

        public List<Statement> Statements { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterBlock(this);
            foreach (var e in Statements)
            {
                e.Accept(visitor);
            }

            visitor.LeaveBlock(this);
        }
    }
}
