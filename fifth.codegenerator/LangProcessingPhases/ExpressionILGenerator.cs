namespace Fifth.CodeGeneration.LangProcessingPhases;

using System.Collections.Generic;
using System.IO;
using System.Text;
using AST;
using BuiltinsGeneration;
using Symbols;
using TypeSystem;
using TypeSystem.PrimitiveTypes;
using BinaryExpression = AST.BinaryExpression;
using Expression = AST.Expression;
using ILExpression = fifth.metamodel.metadata.il.Expression;
using ParameterDeclaration = AST.ParameterDeclaration;
using UnaryExpression = AST.UnaryExpression;

public class ExpressionILGenerator
{
    public string Generate(Expression e)
    {
        return e switch
        {
            IntValueExpression exp => GenerateInt(exp),
            ShortValueExpression exp => GenerateShort(exp),
            LongValueExpression exp => GenerateLong(exp),
            FloatValueExpression exp => GenerateFloat(exp),
            DoubleValueExpression exp => GenerateDouble(exp),
            DecimalValueExpression exp => GenerateDecimal(exp),
            StringValueExpression exp => GenerateString(exp),
            UnaryExpression exp => GenerateUnary(exp),
            BinaryExpression exp => GenerateBinary(exp),
            FuncCallExpression exp => GenerateFuncCall(exp),
            TypeCast exp => GenerateTypeCast(exp),
            VariableReference exp => GenerateVarRef(exp),
            _ => e.ToString()
        };
    }
    private int? GetVarOrdinal(VariableReference ctx)
    {
        if (ctx.NearestScope().TryResolve(ctx.Name, out var ste))
        {
            if (ste.Context is VariableDeclarationStatement vds)
            {
                if (vds.HasAnnotation(Constants.DeclarationOrdinal))
                {
                    return (int)vds[Constants.DeclarationOrdinal];
                }
            }
        }

        return null;
    }

    private string GenerateVarRef(VariableReference ctx)
    {
        var sb = new StringBuilder();
        if (ctx.SymTabEntry.SymbolKind == SymbolKind.FormalParameter)
        {
            var pd = ctx.SymTabEntry.Context as ParameterDeclaration;
            sb.AppendLine($"ldarg.{pd[Constants.ArgOrdinal]}");
        }
        else
        {
            var ord = GetVarOrdinal(ctx);
            if (ord.HasValue)
            {
                sb.AppendLine($"ldloc.s {ord}");
            }
            else
            {
                sb.AppendLine($"ldloc {ctx.Name}");
            }
        }

        return sb.ToString();
    }

    private string GenerateTypeCast(TypeCast e)
    {
        var sb = new StringBuilder();
        sb.AppendLine(Generate(e.SubExpression));
        if (TypeRegistry.DefaultRegistry.TryGetType(e.TargetTid, out var t))
        {
            if (t is PrimitiveAny primitiveAny)
            {
            sb.AppendLine($"conv.{primitiveAny.IlAbbreviation}");
            }
        }

        return sb.ToString();
    }

    private string GenerateFuncCall(FuncCallExpression ctx)
    {
        var sb = new StringBuilder();
        using var writer = new StringWriter(sb);
        List<string> dotnetTypes = new();
        if (ctx.ActualParameters != null && ctx.ActualParameters.Expressions != null)
        {
            foreach (var e in ctx.ActualParameters.Expressions)
            {
                if (TypeMappings.HasMapping(e.TypeId))
                {
                    dotnetTypes.Add(TypeMappings.ToDotnetType(e.TypeId));
                }
                else
                {
                    if (e.TypeId.Lookup() is UserDefinedType udt)
                    {
                        dotnetTypes.Add(udt.Definition.Name);
                    }
                }
            }
        }

        var argTypeNames = dotnetTypes.Join(t => t);
        var dotNetReturnType = "void";
        if (ctx.TypeId != null)
        {
            var return5thType = ctx.TypeId.Lookup();
            dotNetReturnType = TypeMappings.ToDotnetType(return5thType.TypeId);
        }

        if (!ctx.HasAnnotation(Constants.FunctionImplementation))
        {
            throw new Exception("No implementation found to call");
        }

        if (ctx.ActualParameters != null && ctx.ActualParameters.Expressions != null)
        {
            foreach (var e in ctx.ActualParameters.Expressions)
            {
                Generate(e);
            }
        }
        sb.AppendLine($"call {dotNetReturnType} Program::{ctx.Name}({argTypeNames})");

        return sb.ToString();
    }

    private string GenerateBinary(BinaryExpression e)
    {
        var sb = new StringBuilder();

        sb.AppendLine(Generate(e.Left));
        sb.AppendLine(Generate(e.Right));
        switch (e.Op)
        {
            case Operator.Add:
                sb.AppendLine(@"add");
                break;
            case Operator.Subtract:
                sb.AppendLine(@"sub");
                break;
            case Operator.Multiply:
                sb.AppendLine(@"mul");
                break;
            case Operator.Divide:
                sb.AppendLine(@"div");
                break;
            case Operator.Rem:
                break;
            case Operator.Mod:
                break;
            case Operator.And:
                break;
            case Operator.Or:
                break;
            case Operator.Not:
                break;
            case Operator.Nand:
                break;
            case Operator.Nor:
                break;
            case Operator.Xor:
                break;
            case Operator.Equal:
                break;
            case Operator.NotEqual:
                break;
            case Operator.LessThan:
                break;
            case Operator.GreaterThan:
                break;
            case Operator.LessThanOrEqual:
                break;
            case Operator.GreaterThanOrEqual:
                break;
            case null:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return sb.ToString();
    }

    private string GenerateUnary(UnaryExpression e)
    {
        var sb = new StringBuilder();
        sb.AppendLine(Generate(e.Operand));
        switch (e.Op)
        {
            case Operator.Subtract:
                sb.AppendLine("neg");
                break;
            case Operator.Not:
                sb.AppendLine("ldc.i4.0")
                  .AppendLine("ceq");
                break;
            default:
                throw new CodeGenerationException("Invalid operator on unary expression");
        }

        return sb.ToString();
    }

    private string GenerateString(StringValueExpression e)
    {
        return e.TheValue;
    }

    private string GenerateInt(IntValueExpression e)
    {
        return e.TheValue.ToString();
    }

    private string GenerateShort(ShortValueExpression e)
    {
        return e.TheValue.ToString();
    }

    private string GenerateLong(LongValueExpression e)
    {
        return e.TheValue.ToString();
    }

    private string GenerateFloat(FloatValueExpression e)
    {
        return e.TheValue.ToString();
    }

    private string GenerateDouble(DoubleValueExpression e)
    {
        return e.TheValue.ToString();
    }

    private string GenerateDecimal(DecimalValueExpression e)
    {
        return e.TheValue.ToString();
    }
}
