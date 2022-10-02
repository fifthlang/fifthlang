namespace Fifth.CodeGeneration.IL;

using System.Collections.Generic;
using System.Text;

public enum ILVisibility
{
    Public,
    Private
}

public class ClassDefinition
{
    public ClassDefinition()
    {
        Fields = new();
        Properties = new();
        Methods = new();
    }

    public List<FieldDefinition> Fields { get; set; }
    public List<PropertyDefinition> Properties { get; set; }
    public List<MethodDefinition> Methods { get; set; }
    public string Name { get; set; }
    public ILVisibility Visibility { get; set; }
    public string BaseClassName { get; set; }
}
public class ClassBuilder : BaseBuilder<ClassBuilder, ClassDefinition>
{
    public ClassBuilder()
    {
        Model = new ClassDefinition();
    }

    public ClassBuilder WithName(string name)
    {
        Model.Name = name;
        return this;
    }
    public ClassBuilder WithVisibility(ILVisibility visibility)
    {
        Model.Visibility = visibility;
        return this;
    }
    public ClassBuilder WithBaseClass(string baseClass)
    {
        Model.BaseClassName = baseClass;
        return this;
    }
    public ClassBuilder WithField(FieldDefinition fieldDefinition)
    {
        Model.Fields.Add(fieldDefinition);
        return this;
    }
    public ClassBuilder WithProperty(PropertyDefinition propertyDefinition)
    {
        Model.Properties.Add(propertyDefinition);
        return this;
    }
    public ClassBuilder WithMethod(MethodDefinition methodDefinition)
    {
        Model.Methods.Add(methodDefinition);
        return this;
    }
    public override string Build()
    {
        var sb = new StringBuilder();
        sb.AppendLine($".class {Model.Visibility} {Model.Name} extends [System.Runtime]System.Object {{");
        foreach (var field in Model.Fields)
        {
            sb.AppendLine(FieldBuilder.Create(field).Build());
        }
        foreach (var prop in Model.Properties)
        {
            sb.AppendLine(PropertyBuilder.Create(prop).Build());
        }
        foreach (var method in Model.Methods)
        {
            sb.AppendLine(MethodBuilder.Create(method).Build());
        }
        sb.AppendLine("}");
        return sb.ToString();
    }

}
