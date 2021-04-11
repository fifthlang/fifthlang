namespace Fifth.AST.Deprecated
{
    using Visitors;

    public class FuncCallExpression : Expression
    {
        public FuncCallExpression(string name, ExpressionList actualParameters)
        {
            Name = name;
            ActualParameters = actualParameters;
        }

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
