using ast;
using ast_model.Symbols;

namespace Fifth.LangProcessingPhases;

public class StandardTypeAnnotationVisitor(TypeAnnotationContext context) : DefaultRecursiveDescentVisitor
{
    protected readonly TypeAnnotationContext Context = context;

    public override AssemblyDef VisitAssemblyDef(AssemblyDef ctx)
    {
        var result = base.VisitAssemblyDef(ctx);
        var voidType = new FifthType.TVoidType { Name = TypeName.From("void") };
        return result with { Type = voidType };
    }

    public override Int32LiteralExp VisitInt32LiteralExp(Int32LiteralExp ctx)
    {
        var result = base.VisitInt32LiteralExp(ctx);
        var intType = Context.GetLanguageFriendlyType(typeof(int)) ??
                      new FifthType.TDotnetType(typeof(int)) { Name = TypeName.From("int") };
        Context.OnTypeInferred(result, intType);
        return result with { Type = intType };
    }

    public override Int64LiteralExp VisitInt64LiteralExp(Int64LiteralExp ctx)
    {
        var result = base.VisitInt64LiteralExp(ctx);
        var longType = Context.GetLanguageFriendlyType(typeof(long)) ??
                       new FifthType.TDotnetType(typeof(long)) { Name = TypeName.From("long") };
        Context.OnTypeInferred(result, longType);
        return result with { Type = longType };
    }

    public override Float8LiteralExp VisitFloat8LiteralExp(Float8LiteralExp ctx)
    {
        var result = base.VisitFloat8LiteralExp(ctx);
        var doubleType = Context.GetLanguageFriendlyType(typeof(double)) ??
                         new FifthType.TDotnetType(typeof(double)) { Name = TypeName.From("double") };
        Context.OnTypeInferred(result, doubleType);
        return result with { Type = doubleType };
    }

    public override Float4LiteralExp VisitFloat4LiteralExp(Float4LiteralExp ctx)
    {
        var result = base.VisitFloat4LiteralExp(ctx);
        var floatType = Context.GetLanguageFriendlyType(typeof(float)) ??
                        new FifthType.TDotnetType(typeof(float)) { Name = TypeName.From("float") };
        Context.OnTypeInferred(result, floatType);
        return result with { Type = floatType };
    }

    public override BooleanLiteralExp VisitBooleanLiteralExp(BooleanLiteralExp ctx)
    {
        var result = base.VisitBooleanLiteralExp(ctx);
        var boolType = Context.GetLanguageFriendlyType(typeof(bool)) ??
                       new FifthType.TDotnetType(typeof(bool)) { Name = TypeName.From("bool") };
        Context.OnTypeInferred(result, boolType);
        return result with { Type = boolType };
    }

    public override StringLiteralExp VisitStringLiteralExp(StringLiteralExp ctx)
    {
        var result = base.VisitStringLiteralExp(ctx);
        var stringType = Context.GetLanguageFriendlyType(typeof(string)) ??
                         new FifthType.TDotnetType(typeof(string)) { Name = TypeName.From("string") };
        Context.OnTypeInferred(result, stringType);
        return result with { Type = stringType };
    }

    public override ListComprehension VisitListComprehension(ListComprehension ctx)
    {
        var result = base.VisitListComprehension(ctx);
        var elementType = result.Projection?.Type ?? new FifthType.UnknownType { Name = TypeName.From("unknown") };
        var listTypeResult = new FifthType.TListOf(elementType)
        {
            Name = TypeName.From($"List<{TypeAnnotationContext.GetTypeName(elementType)}>")
        };
        Context.OnTypeInferred(result, listTypeResult);
        return result with { Type = listTypeResult };
    }

    public override ListLiteral VisitListLiteral(ListLiteral ctx)
    {
        var result = base.VisitListLiteral(ctx);
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
        return result with { Type = listTypeResult };
    }

    public override BinaryExp VisitBinaryExp(BinaryExp ctx)
    {
        var result = base.VisitBinaryExp(ctx);
        var leftType = result.LHS?.Type;
        var rightType = result.RHS?.Type;

        if (leftType != null && rightType != null)
        {
            var inferred = Context.InferBinaryResultType(leftType, rightType, result.Operator);
            if (inferred != null)
            {
                Context.OnTypeInferred(result, inferred);
                return result with { Type = inferred };
            }
        }

        Context.OnTypeNotFound(result);
        return result with { Type = new FifthType.UnknownType { Name = TypeName.From("unknown") } };
    }

    public override FuncCallExp VisitFuncCallExp(FuncCallExp ctx)
    {
        var result = base.VisitFuncCallExp(ctx);

        if (result.FunctionDef != null)
        {
            Context.OnTypeInferred(result, result.FunctionDef.ReturnType);
            return result with { Type = result.FunctionDef.ReturnType };
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
                    return result with { Type = paramFuncType.OutputType };
                }

                if (resolvedThing is VariableDecl varDecl && varDecl.Type is FifthType.TFunc varFuncType)
                {
                    Context.OnTypeInferred(result, varFuncType.OutputType);
                    return result with { Type = varFuncType.OutputType };
                }
            }
        }

        Context.OnTypeNotFound(result);
        return result with { Type = new FifthType.UnknownType { Name = TypeName.From("unknown") } };
    }

    public override MemberAccessExp VisitMemberAccessExp(MemberAccessExp ctx)
    {
        var result = base.VisitMemberAccessExp(ctx);

        if (result.LHS?.Type == null)
        {
            return result;
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

            return result with { Type = new FifthType.UnknownType { Name = TypeName.From("unknown") } };
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
                    return result with { Type = memberType };
                }
            }
        }

        return result;
    }

    public override IndexerExpression VisitIndexerExpression(IndexerExpression ctx)
    {
        var result = base.VisitIndexerExpression(ctx);
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
                return result with { Type = elementType };
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
        return result with { Type = new FifthType.UnknownType { Name = TypeName.From("unknown") } };
    }

    public override ModuleDef VisitModuleDef(ModuleDef ctx)
    {
        Context.CurrentModule = ctx;
        var result = base.VisitModuleDef(ctx);
        Context.CurrentModule = result;
        return result;
    }

    public override FunctionDef VisitFunctionDef(FunctionDef ctx)
    {
        var result = base.VisitFunctionDef(ctx);
        Context.OnTypeInferred(result, result.ReturnType);
        return result with { Type = result.ReturnType };
    }

    public override ParamDef VisitParamDef(ParamDef ctx)
    {
        var result = base.VisitParamDef(ctx);
        var fifthType = Context.CreateFifthType(result.TypeName, result.CollectionType);
        Context.OnTypeInferred(result, fifthType);
        return result with { Type = fifthType };
    }

    public override VariableDecl VisitVariableDecl(VariableDecl ctx)
    {
        var result = base.VisitVariableDecl(ctx);
        var fifthType = Context.CreateFifthType(result.TypeName, result.CollectionType);
        Context.OnTypeInferred(result, fifthType);
        return result with { Type = fifthType };
    }

    public override PropertyDef VisitPropertyDef(PropertyDef ctx)
    {
        var result = base.VisitPropertyDef(ctx);
        var fifthType = Context.CreateFifthType(result.TypeName, result.CollectionType);
        Context.OnTypeInferred(result, fifthType);
        return result with { Type = fifthType };
    }

    public override VarRefExp VisitVarRefExp(VarRefExp ctx)
    {
        var result = base.VisitVarRefExp(ctx);
        if (result.VariableDecl?.Type != null)
        {
            Context.OnTypeInferred(result, result.VariableDecl.Type);
            return result with { Type = result.VariableDecl.Type };
        }

        Context.OnTypeNotFound(result);
        return result with { Type = new FifthType.UnknownType { Name = TypeName.From("unknown") } };
    }

    public override ObjectInitializerExp VisitObjectInitializerExp(ObjectInitializerExp ctx)
    {
        var result = base.VisitObjectInitializerExp(ctx);
        if (result.TypeToInitialize != null)
        {
            Context.OnTypeInferred(result, result.TypeToInitialize);
            return result with { Type = result.TypeToInitialize };
        }

        Context.OnTypeNotFound(result);
        return result with { Type = new FifthType.UnknownType { Name = TypeName.From("unknown") } };
    }
}
