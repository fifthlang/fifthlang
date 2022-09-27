namespace Fifth.CodeGeneration.ILGeneration;

using System.Text;

public class PropertyDefinition
{
    public string TypeName { get; set; }
    public string Name { get; set; }
    public ILVisibility Visibility { get; set; }
    public ClassDefinition OwningClass { get; set; }
}
public class PropertyBuilder: BaseBuilder<PropertyBuilder, PropertyDefinition>
{
    public PropertyBuilder()
    {
        Model = new PropertyDefinition();
    }

    public PropertyBuilder WithName(string name)
    {
        Model.Name = name;
        return this;
    }
    public PropertyBuilder WithType(string type)
    {
        Model.TypeName = type;
        return this;
    }

    public PropertyBuilder WithOwningClass(ClassDefinition classdef)
    {
        Model.OwningClass = classdef;
        return this;
    }
    public override string Build()
    {
        var sb = new StringBuilder();

            var className = Model.OwningClass?.Name ?? "Program";
            sb.AppendLine($"  .property instance {Model.TypeName} {Model.Name}(){{");
            sb.AppendLine($"      .get instance {Model.TypeName} {className}::get_{Model.Name}()");
            sb.AppendLine($"      .set instance void {className}::set_{Model.Name}({Model.TypeName})");
            sb.AppendLine("  }");

        return sb.ToString();
    }
}
