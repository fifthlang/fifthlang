using ast;
using ast_generated;
using ast_model.Symbols;

namespace Fifth.LangProcessingPhases;

public class StandardTypeAnnotationRewriter(TypeAnnotationContext context) : TypeAnnotationRewriterStageBase(context)
{
    public override RewriteResult VisitAssemblyDef(AssemblyDef ctx)
    {
        var baseResult = base.VisitAssemblyDef(ctx);
        var result = (AssemblyDef)baseResult.Node;
        var voidType = new FifthType.TVoidType { Name = TypeName.From("void") };
        return new RewriteResult(result with { Type = voidType }, baseResult.Prologue);
    }

    public override RewriteResult VisitInt32LiteralExp(Int32LiteralExp ctx)
    {
        var baseResult = base.VisitInt32LiteralExp(ctx);
        var result = (Int32LiteralExp)baseResult.Node;
        var intType = Context.GetLanguageFriendlyType(typeof(int)) ??
                      new FifthType.TDotnetType(typeof(int)) { Name = TypeName.From("int") };
        Context.OnTypeInferred(result, intType);
        return new RewriteResult(result with { Type = intType }, baseResult.Prologue);
    }

    public override RewriteResult VisitInt64LiteralExp(Int64LiteralExp ctx)
    {
        var baseResult = base.VisitInt64LiteralExp(ctx);
        var result = (Int64LiteralExp)baseResult.Node;
        var longType = Context.GetLanguageFriendlyType(typeof(long)) ??
                       new FifthType.TDotnetType(typeof(long)) { Name = TypeName.From("long") };
        Context.OnTypeInferred(result, longType);
        return new RewriteResult(result with { Type = longType }, baseResult.Prologue);
    }

    public override RewriteResult VisitFloat8LiteralExp(Float8LiteralExp ctx)
    {
        var baseResult = base.VisitFloat8LiteralExp(ctx);
        var result = (Float8LiteralExp)baseResult.Node;
        var doubleType = Context.GetLanguageFriendlyType(typeof(double)) ??
                         new FifthType.TDotnetType(typeof(double)) { Name = TypeName.From("double") };
        Context.OnTypeInferred(result, doubleType);
        return new RewriteResult(result with { Type = doubleType }, baseResult.Prologue);
    }

    public override RewriteResult VisitFloat4LiteralExp(Float4LiteralExp ctx)
    {
        var baseResult = base.VisitFloat4LiteralExp(ctx);
        var result = (Float4LiteralExp)baseResult.Node;
        var floatType = Context.GetLanguageFriendlyType(typeof(float)) ??
                        new FifthType.TDotnetType(typeof(float)) { Name = TypeName.From("float") };
        Context.OnTypeInferred(result, floatType);
        return new RewriteResult(result with { Type = floatType }, baseResult.Prologue);
    }

    public override RewriteResult VisitBooleanLiteralExp(BooleanLiteralExp ctx)
    {
        var baseResult = base.VisitBooleanLiteralExp(ctx);
        var result = (BooleanLiteralExp)baseResult.Node;
        var boolType = Context.GetLanguageFriendlyType(typeof(bool)) ??
                       new FifthType.TDotnetType(typeof(bool)) { Name = TypeName.From("bool") };
        Context.OnTypeInferred(result, boolType);
        return new RewriteResult(result with { Type = boolType }, baseResult.Prologue);
    }

    public override RewriteResult VisitStringLiteralExp(StringLiteralExp ctx)
    {
        var baseResult = base.VisitStringLiteralExp(ctx);
        var result = (StringLiteralExp)baseResult.Node;
        var stringType = Context.GetLanguageFriendlyType(typeof(string)) ??
                         new FifthType.TDotnetType(typeof(string)) { Name = TypeName.From("string") };
        Context.OnTypeInferred(result, stringType);
        return new RewriteResult(result with { Type = stringType }, baseResult.Prologue);
    }

    public override RewriteResult VisitListComprehension(ListComprehension ctx)
    {
        var baseResult = base.VisitListComprehension(ctx);
        var result = (ListComprehension)baseResult.Node;
        var elementType = result.Projection?.Type ?? new FifthType.UnknownType { Name = TypeName.From("unknown") };
        var listTypeResult = new FifthType.TListOf(elementType)
        {
            Name = TypeName.From($"List<{TypeAnnotationContext.GetTypeName(elementType)}>")
        };
        Context.OnTypeInferred(result, listTypeResult);
        return new RewriteResult(result with { Type = listTypeResult }, baseResult.Prologue);
    }

    public override RewriteResult VisitListLiteral(ListLiteral ctx)
    {
        var baseResult = base.VisitListLiteral(ctx);
        var result = (ListLiteral)baseResult.Node;
        FifthType? elementType = null;

        if (result.ElementExpressions?.Count > 0)
        {
            elementType = result.ElementExpressions[0]?.Type;
        }

        if (elementType == null && result.Type is FifthType.TArrayOf arrayType)
        {
            elementType = arrayType.ElementType;
        }
        else if (elementType == null && result.Type is FifthType.TListOf listType)
        {
            elementType = listType.ElementType;
        }

        if (elementType == null)
        {
            elementType = Context.GetLanguageFriendlyType(typeof(int)) ??
                          new FifthType.TDotnetType(typeof(int)) { Name = TypeName.From("int") };
        }

        var listTypeResult = new FifthType.TListOf(elementType)
        {
            Name = TypeName.From($"List<{TypeAnnotationContext.GetTypeName(elementType)}>")
        };

        Context.OnTypeInferred(result, listTypeResult);
        return new RewriteResult(result with { Type = listTypeResult }, baseResult.Prologue);
    }

    public override RewriteResult VisitBinaryExp(BinaryExp ctx)
    {
        var baseResult = base.VisitBinaryExp(ctx);
        var result = (BinaryExp)baseResult.Node;
        var leftType = result.LHS?.Type;
        var rightType = result.RHS?.Type;

        if (leftType != null && rightType != null)
        {
            var inferred = Context.InferBinaryResultType(leftType, rightType, result.Operator);
            if (inferred != null)
            {
                Context.OnTypeInferred(result, inferred);
                return new RewriteResult(result with { Type = inferred }, baseResult.Prologue);
            }
        }

        Context.OnTypeNotFound(result);
        return new RewriteResult(
            result with { Type = new FifthType.UnknownType { Name = TypeName.From("unknown") } },
            baseResult.Prologue);
    }

    public override RewriteResult VisitFuncCallExp(FuncCallExp ctx)
    {
        var baseResult = base.VisitFuncCallExp(ctx);
        var result = (FuncCallExp)baseResult.Node;

        if (result.FunctionDef != null)
        {
            Context.OnTypeInferred(result, result.FunctionDef.ReturnType);
            return new RewriteResult(result with { Type = result.FunctionDef.ReturnType }, baseResult.Prologue);
        }

        if (result.Annotations.TryGetValue("FunctionName", out var funcNameObj) && funcNameObj is string funcName)
        {
            var scope = SymbolHelpers.NearestScope(result);
            if (scope != null && scope.TryResolveByName(funcName, out var entry))
            {
                var resolvedThing = entry.OriginatingAstThing;
                if (resolvedThing is ParamDef paramDef && paramDef.Type is FifthType.TFunc paramFuncType)
                {
                    Context.OnTypeInferred(result, paramFuncType.OutputType);
                    return new RewriteResult(result with { Type = paramFuncType.OutputType }, baseResult.Prologue);
                }

                if (resolvedThing is VariableDecl varDecl && varDecl.Type is FifthType.TFunc varFuncType)
                {
                    Context.OnTypeInferred(result, varFuncType.OutputType);
                    return new RewriteResult(result with { Type = varFuncType.OutputType }, baseResult.Prologue);
                }
            }
        }

        Context.OnTypeNotFound(result);
        return new RewriteResult(
            result with { Type = new FifthType.UnknownType { Name = TypeName.From("unknown") } },
            baseResult.Prologue);
    }

    public override RewriteResult VisitMemberAccessExp(MemberAccessExp ctx)
    {
        var baseResult = base.VisitMemberAccessExp(ctx);
        var result = (MemberAccessExp)baseResult.Node;

        if (result.LHS?.Type == null)
        {
            return baseResult;
        }

        var lhsType = result.LHS.Type;
        if (Context.IsPrimitiveType(lhsType))
        {
            Context.Errors.Add(new TypeCheckingError(
                $"Cannot access member on primitive type '{TypeAnnotationContext.GetTypeName(lhsType)}'",
                result.Location?.Filename ?? "",
                result.Location?.Line ?? 0,
                result.Location?.Column ?? 0,
                [lhsType],
                TypeCheckingSeverity.Error));

            return new RewriteResult(
                result with { Type = new FifthType.UnknownType { Name = TypeName.From("unknown") } },
                baseResult.Prologue);
        }

        if (result.RHS is VarRefExp memberRef && lhsType is FifthType.TType userType)
        {
            var className = userType.Name.Value;
            ClassDef? classDef = null;
            if (Context.CurrentModule != null)
            {
                classDef = Context.CurrentModule.Classes.FirstOrDefault(c => c.Name.Value == className);
            }

            if (classDef != null)
            {
                var member = classDef.MemberDefs.FirstOrDefault(m => m.Name.Value == memberRef.VarName);
                if (member != null)
                {
                    var memberType = Context.CreateFifthType(member.TypeName, member.CollectionType);
                    Context.OnTypeInferred(result, memberType);
                    return new RewriteResult(result with { Type = memberType }, baseResult.Prologue);
                }
            }
        }

        return baseResult;
    }

    public override RewriteResult VisitIndexerExpression(IndexerExpression ctx)
    {
        var baseResult = base.VisitIndexerExpression(ctx);
        var result = (IndexerExpression)baseResult.Node;
        if (result.IndexExpression?.Type != null)
        {
            var indexedType = result.IndexExpression.Type;
            var elementType = indexedType switch
            {
                FifthType.TArrayOf arrayType => arrayType.ElementType,
                FifthType.TListOf listType => listType.ElementType,
                _ => null
            };

            if (elementType != null)
            {
                Context.OnTypeInferred(result, elementType);
                return new RewriteResult(result with { Type = elementType }, baseResult.Prologue);
            }

            Context.Errors.Add(new TypeCheckingError(
                $"Cannot index type '{TypeAnnotationContext.GetTypeName(indexedType)}' - only arrays and lists support indexing",
                result.Location?.Filename ?? "",
                result.Location?.Line ?? 0,
                result.Location?.Column ?? 0,
                [indexedType],
                TypeCheckingSeverity.Error));
        }

        Context.OnTypeNotFound(result);
        return new RewriteResult(
            result with { Type = new FifthType.UnknownType { Name = TypeName.From("unknown") } },
            baseResult.Prologue);
    }

    public override RewriteResult VisitModuleDef(ModuleDef ctx)
    {
        Context.CurrentModule = ctx;
        var baseResult = base.VisitModuleDef(ctx);
        var result = (ModuleDef)baseResult.Node;
        Context.CurrentModule = result;
        return baseResult;
    }

    public override RewriteResult VisitFunctionDef(FunctionDef ctx)
    {
        var baseResult = base.VisitFunctionDef(ctx);
        var result = (FunctionDef)baseResult.Node;
        Context.OnTypeInferred(result, result.ReturnType);
        return new RewriteResult(result with { Type = result.ReturnType }, baseResult.Prologue);
    }

    public override RewriteResult VisitParamDef(ParamDef ctx)
    {
        var baseResult = base.VisitParamDef(ctx);
        var result = (ParamDef)baseResult.Node;
        var fifthType = Context.CreateFifthType(result.TypeName, result.CollectionType);
        Context.OnTypeInferred(result, fifthType);
        return new RewriteResult(result with { Type = fifthType }, baseResult.Prologue);
    }

    public override RewriteResult VisitVariableDecl(VariableDecl ctx)
    {
        var baseResult = base.VisitVariableDecl(ctx);
        var result = (VariableDecl)baseResult.Node;
        var fifthType = Context.CreateFifthType(result.TypeName, result.CollectionType);
        Context.OnTypeInferred(result, fifthType);
        return new RewriteResult(result with { Type = fifthType }, baseResult.Prologue);
    }

    public override RewriteResult VisitPropertyDef(PropertyDef ctx)
    {
        var baseResult = base.VisitPropertyDef(ctx);
        var result = (PropertyDef)baseResult.Node;
        var fifthType = Context.CreateFifthType(result.TypeName, result.CollectionType);
        Context.OnTypeInferred(result, fifthType);
        return new RewriteResult(result with { Type = fifthType }, baseResult.Prologue);
    }

    public override RewriteResult VisitVarRefExp(VarRefExp ctx)
    {
        var baseResult = base.VisitVarRefExp(ctx);
        var result = (VarRefExp)baseResult.Node;
        if (result.VariableDecl?.Type != null)
        {
            Context.OnTypeInferred(result, result.VariableDecl.Type);
            return new RewriteResult(result with { Type = result.VariableDecl.Type }, baseResult.Prologue);
        }

        Context.OnTypeNotFound(result);
        return new RewriteResult(
            result with { Type = new FifthType.UnknownType { Name = TypeName.From("unknown") } },
            baseResult.Prologue);
    }

    public override RewriteResult VisitObjectInitializerExp(ObjectInitializerExp ctx)
    {
        var baseResult = base.VisitObjectInitializerExp(ctx);
        var result = (ObjectInitializerExp)baseResult.Node;
        if (result.TypeToInitialize != null)
        {
            Context.OnTypeInferred(result, result.TypeToInitialize);
            return new RewriteResult(result with { Type = result.TypeToInitialize }, baseResult.Prologue);
        }

        Context.OnTypeNotFound(result);
        return new RewriteResult(
            result with { Type = new FifthType.UnknownType { Name = TypeName.From("unknown") } },
            baseResult.Prologue);
    }
}
