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
        return Model.Header.FunctionKind switch
            {
                FunctionKind.Normal => GenerateNormalFunction(),
                FunctionKind.Ctor => GenerateDefaultCtorFunction(),
                FunctionKind.Getter => GenerateGetterFunction(),
                FunctionKind.Setter => GenerateSetterFunction()
            };
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
        result.AddRange(from s in Model.Impl.Body.Statements
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
        sb.Append($".method {Model.Visibility} {Model.Signature.ReturnTypeSignature.Name} {Model.Name}(");
        var sep = "";
        foreach (var pd in Model.Signature.ParameterSignatures)
        {
            sb.Append(sep);
            sb.Append(ParameterSignatureBuilder.Create(pd).Build());
            sep = ", ";
        }

        sb.AppendLine(")cil managed\n")
          .AppendLine("{")
          .AppendLine(GenerateLocalsDecls())
          .AppendLine(BlockBuilder.Create(Model.Impl.Body).Build(false))
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

public partial class MemberRefBuilder
{
    public override string Build()
    {
        throw new NotImplementedException();
    }
}
public partial class MethodRefBuilder
{
    public override string Build()
    {
        throw new NotImplementedException();
    }
}

public partial class MethodSignatureBuilder
{
    public override string Build()
    {
        throw new NotImplementedException();
    }
}
public partial class MethodHeaderBuilder
{
    public override string Build()
    {
        throw new NotImplementedException();
    }
}
public partial class TypeReferenceBuilder
{
    public override string Build()
    {
        throw new NotImplementedException();
    }
}
public partial class MethodImplBuilder
{
    public override string Build()
    {
        throw new NotImplementedException();
    }
}
public partial class ParameterSignatureBuilder
{
    public override string Build()
    {
        throw new NotImplementedException();
    }
}

