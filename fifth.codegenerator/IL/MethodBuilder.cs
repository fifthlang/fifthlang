namespace Fifth.CodeGeneration.IL;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AST;
using BuiltinsGeneration;
using fifth.metamodel.metadata;
using fifth.metamodel.metadata.il;
using LangProcessingPhases;
using TypeSystem;
using Statement = fifth.metamodel.metadata.il.Statement;
using VariableDeclarationStatement = fifth.metamodel.metadata.il.VariableDeclarationStatement;

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

    private IEnumerable<VariableDeclarationStatement> GetLocalDecls()
    {
        var result = new List<VariableDeclarationStatement>();
        result.AddRange(from s in Model.Body.Statements
                        where s is VariableDeclarationStatement
                        select (VariableDeclarationStatement)s);
        return result;
    }

    protected string GenerateLocalsDecls()
    {
        var sb = new StringBuilder();
        var sep = "";

        var localDecls = GetLocalDecls();
        var declCtr = 0;
        if (localDecls.Any())
        {
            sb.AppendLine(".locals init(");
            foreach (var vd in localDecls)
            {
                vd.Ordinal = declCtr;
                sb.AppendLine($"{sep} [{declCtr}] {vd.TypeName} {vd.Name}");
                declCtr++;
                sep = ",";
            }

            sb.AppendLine(")");
        }

        return sb.ToString();
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

        sb.AppendLine(")cil managed\n")
          .AppendLine("{")
          .AppendLine(GenerateLocalsDecls())
          .AppendLine(BlockBuilder.Create(Model.Body).Build(false))
          .AppendLine("}");
        return sb.ToString();
    }

    public string MapType(TypeId tid)
    {
        if (tid == null)
        {
            return "void";
        }

        if (TypeMappings.HasMapping(tid))
        {
            return TypeMappings.ToDotnetType(tid);
        }

        var tn = tid.Lookup().Name;
        if (tid.Lookup() is UserDefinedType)
        {
            tn = $"class {tn}";
        }

        return tn;
    }
}
