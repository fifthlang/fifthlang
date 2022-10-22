namespace Fifth.CodeGeneration.IL;

using fifth.metamodel.metadata.il;

public partial class AssemblyReferenceBuilder:BaseBuilder<AssemblyReferenceBuilder, AssemblyReference>
{
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
