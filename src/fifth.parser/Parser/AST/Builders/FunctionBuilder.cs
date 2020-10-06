using fifth.VirtualMachine;
using System.Collections.Generic;

namespace fifth.Parser.AST.Builders
{
    /// <summary>
    /// A fluent API for building the AST node definitions of Functions
    /// </summary>
    public class ExpressionListBuilder
    {
        public ExpressionListBuilder()
        {
            Expressions = new List<Expression>();
        }

        public List<Expression> Expressions { get; private set; }

        public static ExpressionListBuilder Start()
        {
            return new ExpressionListBuilder();
        }

        public List<Expression> Build()
        {
            return Expressions;
        }

        public ExpressionListBuilder WithExpression(Expression expression)
        {
            Expressions.Add(expression);
            return this;
        }
    }

    public class FunctionBuilder
    {
        public FunctionBuilder()
        {
        }

        public List<Expression> Body { get; private set; }
        public string FunctionName { get; private set; }
        public IFifthType ReturnType { get; private set; }
        internal ParameterDeclarationList ParameterDeclarations { get; private set; }

        public static FunctionBuilder Start()
        {
            return new FunctionBuilder();
        }

        public FunctionDefinition Build()
        {
            return new FunctionDefinition
            {
                Name = this.FunctionName,
                ReturnType = this.ReturnType,
                Body = this.Body
            };
        }

        public FunctionBuilder WithBody(List<Expression> expressions)
        {
            this.Body = expressions;
            return this;
        }

        public FunctionBuilder WithName(string functionName)
        {
            FunctionName = functionName;
            return this;
        }

        public FunctionBuilder WithParameters(ParameterDeclarationList parameterDeclarations)
        {
            this.ParameterDeclarations = parameterDeclarations;
            return this;
        }

        public FunctionBuilder WithReturnType(IFifthType returnType)
        {
            this.ReturnType = returnType;
            return this;
        }
    }
}