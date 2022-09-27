namespace Fifth.CodeGeneration.ILGeneration;

using System.Text;

public class FieldDefinition
{
    public string TypeName { get; set; }
    public string Name { get; set; }
    public ILVisibility Visibility { get; set; }
}
public class FieldBuilder: BaseBuilder<FieldBuilder, FieldDefinition>
{
    public FieldBuilder()
    {
        Model = new FieldDefinition();
    }


    public FieldBuilder WithName(string name)
    {
        Model.Name = name;
        return this;
    }
    public FieldBuilder WithType(string type)
    {
        Model.TypeName = type;
        return this;
    }
    public override string Build()
    {
        var sb = new StringBuilder();

        sb.Append($".field {Model.Visibility} {Model.TypeName} '{Model.Name}'");

        return sb.ToString();
    }
}
