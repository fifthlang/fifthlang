namespace Fifth.CodeGeneration.ILGeneration;
public class ParameterDeclaration
{
    public string Name { get; set; }
    public string TypeName { get; set; }
    public bool IsUDTType { get; set; }
}

public class ParameterDeclarationBuilder : BaseBuilder<ParameterDeclarationBuilder, ParameterDeclaration>
{
    public ParameterDeclarationBuilder WithName(string name)
    {
        Model.Name = name;
        return this;
    }
    public ParameterDeclarationBuilder WithTypeName(string typeName)
    {
        Model.TypeName = typeName;
        return this;
    }
    public ParameterDeclarationBuilder WithUDTType()
    {
        Model.IsUDTType = true;
        return this;
    }

    public ParameterDeclarationBuilder()
    {
        Model = new();
    }
    public override string Build()
    {
        var udt = Model.IsUDTType ? "class" : "";
        return $"{udt} {Model.TypeName} {Model.Name}";
    }
}
