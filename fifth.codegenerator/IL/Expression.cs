namespace Fifth.CodeGeneration.IL;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using fifth.metamodel.metadata.il;
using PrimitiveTypes;
using TypeSystem.PrimitiveTypes;

public partial class ExpressionBuilder : BaseBuilder<ExpressionBuilder, Expression>
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
            VariableReferenceExpression vre => BuildVarRef(vre),
            _ => ""
        };
        return result;
    }

    private string BuildVarRef(VariableReferenceExpression vre)
    {
        var sb = new StringBuilder();
        // TODO:  there probably needs to be lots more here about identifying the class, etc
        sb.Append("ldloc.s ");
        sb.AppendLine(vre.Name);
        return sb.ToString();
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

public static class ExpressionBuilderFactory
{
    public static IBuilder<Expression> Create<T>() where T : Expression
    {
        var type = typeof(T);
        if(type == typeof(UnaryExpression))
                return (IBuilder<Expression>)UnaryExpressionBuilder.Create();
        else if (type == typeof(BinaryExpression))
            return (IBuilder<Expression>)BinaryExpressionBuilder.Create();
        else if (type == typeof(FuncCallExp))
            return (IBuilder<Expression>)FuncCallExpBuilder.Create();
        else if (type == typeof(TypeCastExpression))
            return (IBuilder<Expression>)TypeCastExpressionBuilder.Create();
        else throw new TypeCheckingException("unknown Expression type");
    }
    public static IBuilder<T> Create<T>(T model) where T : Expression
    {
        return model switch
        {
            UnaryExpression ue => (IBuilder<T>)UnaryExpressionBuilder.Create(ue),
            BinaryExpression be => (IBuilder<T>)BinaryExpressionBuilder.Create(be),
            FuncCallExp fce => (IBuilder<T>)FuncCallExpBuilder.Create(fce),
            TypeCastExpression tce => (IBuilder<T>)TypeCastExpressionBuilder.Create(tce),
            _ => (IBuilder<T>)ExpressionBuilder.Create(model)
        };
    }
}

public partial class UnaryExpressionBuilder
{
    public override string Build()
    {
        var sb = new StringBuilder();
        sb.AppendLine(ExpressionBuilder.Create(Model.Exp).Build());
        sb.AppendLine(Model.Op);
        return sb.ToString();
    }
}

public partial class BinaryExpressionBuilder
{
    public override string Build()
    {
        var sb = new StringBuilder();
        sb.AppendLine(ExpressionBuilder.Create(Model.LHS).Build());
        sb.AppendLine(ExpressionBuilder.Create(Model.RHS).Build());
        sb.AppendLine(Model.Op);
        return sb.ToString();
    }
}

public partial class FuncCallExpBuilder
{
    public override string Build()
    {
        var sb = new StringBuilder();
        sb.Append(Join(Model.Args, "\n", e => ExpressionBuilderFactory.Create(e).Build()));
        sb.Append($"call {Model.ReturnType} Program::{Model.Name}(");
        sb.Append(Join(Model.ArgTypes, ", ", e => e));
        sb.Append(")");
        return sb.ToString();
    }
}

public partial class TypeCastExpressionBuilder
{
    public override string Build()
    {
        /*
        var l = new PrimitiveAny[]
        {
            PrimitiveBool.Default, PrimitiveChar.Default, PrimitiveDate.Default, PrimitiveDecimal.Default,
            PrimitiveDouble.Default, PrimitiveFloat.Default, PrimitiveFunction.Default, PrimitiveInteger.Default,
            PrimitiveLong.Default, PrimitiveShort.Default, PrimitiveString.Default
        };

        var lut = l.ToDictionary(pt => pt.TypeId, pt => pt.IlAbbreviation);
        if (lut.ContainsKey(ctx.TargetTid))
        {
            sb.AppendLine($"conv.{lut[ctx.TargetTid]}");
        }
        else
        {
            sb.AppendLine($"conv {ctx.TargetTid.Lookup().Name}");
        }
         */
        var sb = new StringBuilder();
        sb.AppendLine(ExpressionBuilderFactory.Create(Model.Expression).Build());
        if (!string.IsNullOrWhiteSpace(Model.TargetTypeCilName))
        {
            sb.AppendLine($"conv.{Model.TargetTypeCilName}");
        }

        return sb.ToString();
    }
}

public partial class VariableReferenceExpressionBuilder
{
    public override string Build() => throw new NotImplementedException();
}

public partial class VariableDeclarationStatementBuilder
{
    public override string Build() => throw new NotImplementedException();
}

public partial class ExpressionStatementBuilder
{
    public override string Build()
    {
        var sb = new StringBuilder();
        sb.Append("_ = ");
        sb.Append(ExpressionBuilder.Create(Model.Expression).Build());
        return sb.ToString();
    }
}
