namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;
    using TypeSystem;

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

        public FuncCallExpression(string name, ExpressionList actualParameters, AstNode parentNode, IFifthType fifthType)
            : base(parentNode, fifthType)
        {
            Name = name;
            ActualParameters = actualParameters;
        }

        public FuncCallExpression(string name, ExpressionList actualParameters, IFifthType fifthType)
            : base(fifthType)
        {
            Name = name;
            ActualParameters = actualParameters;
        }

        public FuncCallExpression(string name, ExpressionList actualParameters)
            : base(null)
        {
            Name = name;
            ActualParameters = actualParameters;
        }
    }
}
