using ast;
using ast_generated;
using ast_model.Symbols;

namespace Fifth.LangProcessingPhases;

public class GenericTypeAnnotationRewriter(TypeAnnotationContext context) : TypeAnnotationRewriterStageBase(context)
{
    public override RewriteResult VisitFuncCallExp(FuncCallExp ctx)
    {
        var baseResult = base.VisitFuncCallExp(ctx);
        var result = (FuncCallExp)baseResult.Node;

        if (result.FunctionDef == null || result.FunctionDef.TypeParameters.Count == 0)
        {
            return baseResult;
        }

        if (TryResolveGenericReturnType(result, out var resolvedType))
        {
            Context.OnTypeInferred(result, resolvedType);
            return new RewriteResult(result with { Type = resolvedType }, baseResult.Prologue);
        }

        if (result.Annotations.TryGetValue("FunctionName", out var funcNameObj) && funcNameObj is string funcName)
        {
            var scope = SymbolHelpers.NearestScope(result);
            if (scope != null && scope.TryResolveByName(funcName, out var entry) &&
                entry.OriginatingAstThing is ParamDef paramDef && paramDef.Type is FifthType.TFunc paramFuncType)
            {
                Context.OnTypeInferred(result, paramFuncType.OutputType);
                return new RewriteResult(result with { Type = paramFuncType.OutputType }, baseResult.Prologue);
            }
        }

        return baseResult;
    }

    private static bool TryResolveGenericReturnType(FuncCallExp call, out FifthType resolvedType)
    {
        resolvedType = call.Type;
        var funcDef = call.FunctionDef!;
        var typeParameters = funcDef.TypeParameters;
        if (typeParameters.Count == 0)
        {
            return false;
        }

        var typeMap = new Dictionary<string, FifthType>(StringComparer.Ordinal);

        for (var i = 0; i < typeParameters.Count; i++)
        {
            if (i < call.TypeArguments.Count)
            {
                typeMap[typeParameters[i].Name.Value] = call.TypeArguments[i];
                continue;
            }

            if (i < call.InvocationArguments.Count && call.InvocationArguments[i]?.Type != null)
            {
                typeMap[typeParameters[i].Name.Value] = call.InvocationArguments[i].Type;
            }
        }

        if (funcDef.ReturnType is FifthType.TType returnType &&
            typeMap.TryGetValue(returnType.Name.Value, out var mappedType))
        {
            resolvedType = mappedType;
            return true;
        }

        return false;
    }
}
