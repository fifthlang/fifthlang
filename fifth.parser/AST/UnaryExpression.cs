namespace Fifth.AST
{
    using Fifth;
    using Fifth.Parser.LangProcessingPhases;

    public class UnaryExpression : Expression
    {
        public Operator Op { get; set; }
        public Expression Operand { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterUnaryExpression(this);
            Operand.Accept(visitor);
            visitor.LeaveUnaryExpression(this);
        }
    }
}
