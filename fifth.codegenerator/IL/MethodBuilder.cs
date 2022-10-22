namespace Fifth.CodeGeneration.IL;

using System.Collections.Generic;
using System.IO;
using System.Text;
using BuiltinsGeneration;
using fifth.metamodel.metadata;
using fifth.metamodel.metadata.il;

public partial class MethodDefinitionBuilder : BaseBuilder<MethodDefinitionBuilder, MethodDefinition>
{
    public override string Build()
    {
        return Model.FunctionKind switch
            {
                FunctionKind.Normal => GenerateNormalFunction(),
                FunctionKind.Ctor => GenerateDefaultCtorFunction(),
                FunctionKind.Getter => GenerateGetterFunction(),
                FunctionKind.Setter => GenerateSetterFunction()
            }
            ;
    }

    private string GenerateSetterFunction()
    {
        throw new NotImplementedException();
    }

    private string GenerateGetterFunction()
    {
        throw new NotImplementedException();
    }

    private string GenerateDefaultCtorFunction()
    {
        throw new NotImplementedException();
    }



    private string GenerateNormalFunction()
    {
        var sb = new StringBuilder();
        sb.Append($".method {Model.Visibility} {Model.ReturnType} {Model.Name}(");
        var sep = "";
        foreach (var pd in Model.Parameters)
        {
            sb.Append(sep);
            sb.Append(ParameterDeclarationBuilder.Create(pd).Build());
            sep = ", ";
        }

        sb.AppendLine(")cil managed\n");
        sb.AppendLine(BlockBuilder.Create(Model.Body).Build());
        sb.AppendLine("");
        return sb.ToString();
    }
    /*
        switch (ctx.FunctionKind)
        {
            case FunctionKind.BuiltIn:
                GenerateBuiltinFunction(ctx);
                break;
            case FunctionKind.Normal:
                GenerateNormalFunction(ctx);
                break;
            case FunctionKind.Ctor:
                GenerateDefaultCtorFunction(ctx);
                break;
            case FunctionKind.Getter:
                GenerateGetterFunction(ctx);
                break;
            case FunctionKind.Setter:
                GenerateSetterFunction(ctx);
                break;
        }*/
}
