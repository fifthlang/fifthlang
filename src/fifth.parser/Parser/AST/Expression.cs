using fifth.VirtualMachine;
using System.Collections.Generic;

namespace fifth.parser.Parser.AST
{
    public abstract class Expression : AstNode
    {
        public IFifthType FifthType { get; set; }
    }

    public class ExpressionList : AstNode
    {
        public List<Expression> Expressions { get; set; }
    }

    public class IntValueExpression : LiteralExpression<int>
    {
        public IntValueExpression(int value) : base(value)
        {
        }
    }

    public class LiteralExpression<T> : Expression
    {
        public LiteralExpression(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }

    public class StringValueExpression : LiteralExpression<string>
    {
        public StringValueExpression(string value) : base(value)
        {
        }
    }
}