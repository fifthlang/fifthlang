using System.Collections.Generic;

namespace Fifth.AST.Builders
{
    /// <summary>
    /// A fluent API for building the AST node definitions of Functions
    /// </summary>
    public class ExpressionListBuilder : IBuilder<List<Expression>>
    {
        public ExpressionListBuilder() => this.Expressions = new List<Expression>();

        public List<Expression> Expressions { get; private set; }

        public static ExpressionListBuilder Start() => new ExpressionListBuilder();

        public List<Expression> Build() => this.Expressions;

        public bool IsValid() => throw new System.NotImplementedException();

        public ExpressionListBuilder WithExpression(Expression expression)
        {
            this.Expressions.Add(expression);
            return this;
        }
    }
}
