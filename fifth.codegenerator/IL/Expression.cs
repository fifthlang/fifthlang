namespace Fifth.CodeGeneration.IL;

using System.Collections.Generic;
using System.Text;

public abstract class Expression { }

public  class Literal<T> : Expression, ILiteralValue
{
    public Literal(T value)
    {
        Value = value;
    }
    public string TypeName
    {
        get
        {
            return typeof(T).Name;
        }
    }

    public T Value { get; set; }
}

public class UnaryExpression : Expression
{
    public UnaryExpression(string op, Expression exp)
    {
        Op = op;
        Exp = exp;
    }

    public string Op { get; set; }
    public Expression Exp { get; set; }
}
public class BinaryExpression : Expression
{
    public BinaryExpression(string op, Expression lhs, Expression rhs)
    {
        Op = op;
        LHS = lhs;
        RHS = rhs;
    }

    public string Op { get; set; }
    public Expression LHS { get; set; }
    public Expression RHS { get; set; }
}

public class FuncCallExp : Expression
{
    public FuncCallExp(string name, string typename, Expression[] args)
    {
        Name = name;
        Args = new(args);
        ArgTypes = new();
        ReturnType = typename;
    }

    public string Name { get; set; }
    public List<Expression> Args { get; set; }
    public string ReturnType { get; set; }
    public ClassDefinition ClassDefinition { get; set; }
    public List<string> ArgTypes { get; set; }
}

public interface ILiteralValue
{
    public string TypeName { get; }
}

public class ExpressionBuilder : BaseBuilder<ExpressionBuilder, Expression>
{
    public ExpressionBuilder WithLiteral<T>(T value)
    {
        Model = new Literal<T>(value);
        return this;
    }

    public ExpressionBuilder WithUnaryExp(string op, Expression expression)
    {
        Model = new UnaryExpression(op, expression);
        return this;
    }
    public ExpressionBuilder WithBinaryExp(string op, Expression lhs, Expression rhs)
    {
        Model = new BinaryExpression(op, lhs, rhs);
        return this;
    }
    public ExpressionBuilder WithFunctionCall(string name, string typename, params Expression[] args)
    {
        Model = new FuncCallExp(name, typename, args);
        return this;
    }
    public override string Build()
    {
        var result = Model switch
        {
            ILiteralValue lv => BuildLiteral(lv),
            UnaryExpression ue => BuildUnaryExp(ue),
            BinaryExpression be => BuildBinaryExp(be),
            FuncCallExp fce => BuildFuncCall(fce),
            _ => ""
        };
        return result;
    }

    private string BuildFuncCall(FuncCallExp funcCallExp)
    {
        var sb = new StringBuilder();
        foreach (var arg in funcCallExp.Args)
        {
            sb.AppendLine(Create(arg).Build());
        }

        sb.Append($"call {funcCallExp.ReturnType} Program::{funcCallExp.Name}(");
        var sep = "";
        foreach (var arg in funcCallExp.ArgTypes)
        {
            sb.Append(sep);
            sep = ", ";
            sb.Append(arg);
        }

        sb.Append(")");
        return sb.ToString();
    }

    private string BuildBinaryExp(BinaryExpression binaryExpression)
    {
        var sb = new StringBuilder();
        sb.AppendLine(Create(binaryExpression.LHS).Build());
        sb.AppendLine(Create(binaryExpression.RHS).Build());
        sb.AppendLine(binaryExpression.Op);
        return sb.ToString();
    }

    private string BuildUnaryExp(UnaryExpression unaryExpression)
    {
        var sb = new StringBuilder();
        sb.AppendLine(Create(unaryExpression.Exp).Build());
        sb.AppendLine(unaryExpression.Op);
        return sb.ToString();
    }

    private string BuildLiteral(ILiteralValue literalValue)
    {
        switch (literalValue.TypeName)
        {
            case "Int32":
                var lv = (Literal<int>)literalValue;
                return $"ldc {lv.Value}";
            case "Boolean":
                var blv = (Literal<bool>)literalValue;
                return $"ldc {blv.Value.ToString().ToLowerInvariant()}";
            case "Double":
                var dlv = (Literal<double>)literalValue;
                return $"ldc {dlv.Value}";
            default:
                return "unknown";
        }
    }
}
