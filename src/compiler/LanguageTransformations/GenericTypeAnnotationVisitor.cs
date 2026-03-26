using ast;
using ast_model.Symbols;

namespace Fifth.LangProcessingPhases;

public class GenericTypeAnnotationVisitor(TypeAnnotationContext context) : StandardTypeAnnotationVisitor(context)
{
    public override AstThing Visit(AstThing ctx) => base.Visit(ctx);

    public override FuncCallExp VisitFuncCallExp(FuncCallExp ctx)
    {
        var result = base.VisitFuncCallExp(ctx);

        if (result.FunctionDef == null)
        {
            return result;
        }

        if (result.FunctionDef.TypeParameters.Count == 0)
        {
            return result;
        }

        if (result.TypeArguments.Count > 0)
        {
            var explicitTypeArg = result.TypeArguments[0];
            Context.OnTypeInferred(result, explicitTypeArg);
            return result with { Type = explicitTypeArg };
        }

        if (result.InvocationArguments.Count > 0 && result.InvocationArguments[0].Type != null)
        {
            var inferredTypeArg = result.InvocationArguments[0].Type;
            Context.OnTypeInferred(result, inferredTypeArg);
            return result with { Type = inferredTypeArg };
        }

        if (result.Annotations.TryGetValue("FunctionName", out var funcNameObj) && funcNameObj is string funcName)
        {
            var scope = SymbolHelpers.NearestScope(result);
            if (scope != null && scope.TryResolveByName(funcName, out var entry))
            {
                if (entry.OriginatingAstThing is ParamDef paramDef && paramDef.Type is FifthType.TFunc paramFuncType)
                {
                    Context.OnTypeInferred(result, paramFuncType.OutputType);
                    return result with { Type = paramFuncType.OutputType };
                }
            }
        }

        return result;
    }
}
