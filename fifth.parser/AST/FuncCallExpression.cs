namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public class FuncCallExpression : Expression
    {
        public ExpressionList ActualParameters { get; set; }
        public string Name { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterFuncCallExpression(this);
            foreach (var e in ActualParameters.Expressions)
            {
                e.Accept(visitor);
            }
            visitor.LeaveFuncCallExpression(this);
        }
    }
}
