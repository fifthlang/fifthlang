namespace Fifth.CodeGeneration.ILGeneration;

public class AssemblyReference
{
    public string Name { get; set; }
    public string PublicKeyToken { get; set; }
    public Version Version { get; set; }
}
public class AssemblyReferenceBuilder:BaseBuilder<AssemblyReferenceBuilder, AssemblyReference>
{
    public AssemblyReferenceBuilder()
    {
        Model = new AssemblyReference();
    }

    public AssemblyReferenceBuilder WithName(string name)
    {
        Model.Name = name;
        return this;
    }
    public AssemblyReferenceBuilder WithPublicKeyToken(string token)
    {
        Model.PublicKeyToken = token;
        return this;
    }
    public AssemblyReferenceBuilder WithVersion(Version v)
    {
        Model.Version = v;
        return this;
    }
    public override string Build()
    {
        var result = $@".assembly extern {Model.Name}
            {{
              .publickeytoken = ( {Model.PublicKeyToken} )
              .ver {WriteVersion(Model.Version)}
            }}";
        return result;
    }
}
