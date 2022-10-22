namespace Fifth.CodeGeneration.LangProcessingPhases;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AST;
using BuiltinsGeneration;
using fifth.metamodel.metadata.il;
using IL;
using Symbols;
using TypeSystem;
using TypeSystem.PrimitiveTypes;
using BinaryExpression = AST.BinaryExpression;
using Expression = fifth.metamodel.metadata.il.Expression;
using MemberAccessExpression = AST.MemberAccessExpression;
using ParameterDeclaration = AST.ParameterDeclaration;
using UnaryExpression = AST.UnaryExpression;

/// <summary>
///     Takes over generation for the IL from the ILGenerator.  It does this because the composite pattern in use with
///     expressions prevents the use of the ILGenerator's stack scope tracking approach.
/// </summary>
public class ExpressionILBuilder
{
    public static Expression Generate(AST.Expression e)
    {
        return e switch
        {
            IntValueExpression exp => new Literal<int>(exp.TheValue),
            ShortValueExpression exp => new Literal<short>(exp.TheValue),
            LongValueExpression exp => new Literal<long>(exp.TheValue),
            FloatValueExpression exp => new Literal<float>(exp.TheValue),
            DoubleValueExpression exp => new Literal<double>(exp.TheValue),
            DecimalValueExpression exp => new Literal<decimal>(exp.TheValue),
            StringValueExpression exp => new Literal<string>(exp.TheValue),
            UnaryExpression exp => GenerateUnary(exp),
            BinaryExpression exp => GenerateBinary(exp),
            FuncCallExpression exp => GenerateFuncCall(exp),
            TypeCast exp => GenerateTypeCast(exp),
            VariableReference exp => GenerateVarRef(exp),
            MemberAccessExpression mae => GenerateMemberAccess(mae),
            _ => throw new CodeGenerationException("Unrecognised Expression type")
        };
    }

    private static Expression GenerateMemberAccess(MemberAccessExpression mae)
    {
        return MemberAccessExpressionBuilder.Create()
                                     .WithLhs(Generate(mae.LHS))
                                     .WithRhs(Generate(mae.RHS))
                                     .New();
    }

    static int? GetVarOrdinal(VariableReference ctx)
    {
        if (ctx.NearestScope().TryResolve(ctx.Name, out var ste))
        {
            if (ste.Context is AST.VariableDeclarationStatement vds)
            {
                if (vds.HasAnnotation(Constants.DeclarationOrdinal))
                {
                    return (int)vds[Constants.DeclarationOrdinal];
                }
            }
        }

        return null;
    }

    static Expression GenerateVarRef(VariableReference ctx)
    {
        var vrb = VariableReferenceExpressionBuilder.Create()
            .WithName(ctx.Name)
            .WithSymTabEntry(ctx.SymTabEntry);
        var ord = GetVarOrdinal(ctx);
        if (ord.HasValue)
        {
            vrb.WithOrdinal(ord.Value);
        }

        vrb.WithIsParameterReference(ord.HasValue);

        return vrb .New();
    }

    static Expression GenerateTypeCast(TypeCast e)
    {
        var tcb = TypeCastExpressionBuilder.Create();

        if (TypeRegistry.DefaultRegistry.TryGetType(e.TargetTid, out var t))
        {
            tcb.WithTargetTypeName(t.Name);
            if (t is PrimitiveAny primitiveAny)
            {
                tcb.WithTargetTypeCilName(primitiveAny.IlAbbreviation);
            }
        }

        tcb.WithExpression(Generate(e.SubExpression));
        return tcb.New();
    }

    static Expression GenerateFuncCall(FuncCallExpression ctx)
    {
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

        var dotNetReturnType = "void";
        if (ctx.TypeId != null)
        {
            var return5thType = ctx.TypeId.Lookup();
            dotNetReturnType = TypeMappings.ToDotnetType(return5thType.TypeId);
        }

        var fcb = FuncCallExpBuilder.Create().WithName(ctx.Name)
                                    //.WithClassDefinition(ctx.ParentNode ) // where can I get this when the builder is disconnected?
                                    .WithReturnType(dotNetReturnType);
        fcb.WithArgs(ctx.ActualParameters.Expressions.Select(e => Generate(e)).ToList());
        fcb.WithArgTypes(dotnetTypes);
        return fcb.New();
    }

    static Expression GenerateBinary(BinaryExpression e)
    {
        return BinaryExpressionBuilder.Create()
                                      .WithOp(e.Op.ToString())
                                      .WithLHS(Generate(e.Left))
                                      .WithRHS(Generate(e.Right))
                                      .New();
    }

    static Expression GenerateUnary(UnaryExpression e)
    {
        return UnaryExpressionBuilder.Create()
                                     .WithOp(e.Op.ToString())
                                     .WithExp(Generate(e.Operand))
                                     .New();
    }
}
