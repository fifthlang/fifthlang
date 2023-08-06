namespace Fifth.CodeGeneration.LangProcessingPhases;

#region

using System.Collections.Generic;
using System.Linq;
using AST;
using IL;
using TypeSystem.PrimitiveTypes;
using ilmm = fifth.metamodel.metadata.il;

#endregion

/// <summary>
///     Takes over generation for the IL from the ILGenerator.  It does this because the composite pattern in use with
///     expressions prevents the use of the ILGenerator's stack scope tracking approach.
/// </summary>
public class ExpressionILBuilder
{
    public static ilmm.Expression Generate(Expression e)
    {
        return e switch
        {
            IntValueExpression exp => new IntLiteral(exp.TheValue),
            ShortValueExpression exp => new ShortLiteral(exp.TheValue),
            LongValueExpression exp => new LongLiteral(exp.TheValue),
            FloatValueExpression exp => new FloatLiteral(exp.TheValue),
            DoubleValueExpression exp => new DoubleLiteral(exp.TheValue),
            DecimalValueExpression exp => new DecimalLiteral(exp.TheValue),
            StringValueExpression exp => new StringLiteral(exp.TheValue),
            UnaryExpression exp => GenerateUnary(exp),
            BinaryExpression exp => GenerateBinary(exp),
            FuncCallExpression exp => GenerateFuncCall(exp),
            TypeCast exp => GenerateTypeCast(exp),
            VariableReference exp => GenerateVarRef(exp),
            MemberAccessExpression mae => GenerateMemberAccess(mae),
            _ => throw new CodeGenerationException("Unrecognised Expression type")
        };
    }

    private static ilmm.Expression GenerateMemberAccess(MemberAccessExpression mae)
    {
        return MemberAccessExpressionBuilder.Create()
                                            .WithLhs(Generate(mae.LHS))
                                            .WithRhs(Generate(mae.RHS))
                                            .New();
    }

    private static int? GetVarOrdinal(VariableReference ctx)
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

    private static ilmm.Expression GenerateVarRef(VariableReference ctx)
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

        return vrb.New();
    }

    private static ilmm.Expression GenerateTypeCast(TypeCast e)
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

    private static ilmm.Expression GenerateFuncCall(FuncCallExpression ctx)
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

    private static ilmm.Expression GenerateBinary(BinaryExpression e)
    {
        var opcode = ToIlOpCode(e.Op);
        return BinaryExpressionBuilder.Create()
                                      .WithOp(opcode)
                                      .WithLHS(Generate(e.Left))
                                      .WithRHS(Generate(e.Right))
                                      .New();
    }

    private static ilmm.Expression GenerateUnary(UnaryExpression e)
    {
        return UnaryExpressionBuilder.Create()
                                     .WithOp(e.Op.ToString())
                                     .WithExp(Generate(e.Operand))
                                     .New();
    }

    private static string? ToIlOpCode(Operator? op)
    {
        if (!op.HasValue)
        {
            return string.Empty;
        }

        return op.Value.ToIlOpCode();
    }
}
