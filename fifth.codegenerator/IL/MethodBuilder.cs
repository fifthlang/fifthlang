namespace Fifth.CodeGeneration.IL;

using System.Collections.Generic;
using System.Text;

public class MethodDefinition
{
    public string Name { get; set; }
    public string ReturnType { get; set; }
    public List<ParameterDeclaration> Parameters { get; set; } = new();
    public ILVisibility Visibility { get; set; }
    public List<Statement> Body { get; set; } = new();
}
public class MethodBuilder : BaseBuilder<MethodBuilder, MethodDefinition>
{
    public MethodBuilder()
    {
        Model = new MethodDefinition();
    }

    public MethodBuilder WithName(string name)
    {
        Model.Name = name;
        return this;
    }
    public MethodBuilder WithType(string typeName)
    {
        Model.ReturnType = typeName;
        return this;
    }
    public MethodBuilder WithParameter(ParameterDeclaration parameterDeclaration)
    {
        Model.Parameters.Add(parameterDeclaration);
        return this;
    }

    public MethodBuilder WithVisibility(ILVisibility visibility)
    {
        Model.Visibility = visibility;
        return this;
    }
    public MethodBuilder WithStatement(Statement s)
    {
        Model.Body.Add(s);
        return this;
    }
    /*public MethodBuilder With()
    {
        return this;
    }*/
    public override string Build()
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

        sb.AppendLine(")cil managed\n{");
        foreach (var statement in Model.Body)
        {
            sb.AppendLine(StatementBuilder.Create(statement).Build());
        }
        sb.AppendLine("}");
        return sb.ToString();
    }
}
