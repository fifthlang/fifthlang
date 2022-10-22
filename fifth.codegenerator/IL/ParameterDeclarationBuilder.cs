namespace Fifth.CodeGeneration.IL;

using fifth.metamodel.metadata.il;

public partial class ParameterDeclarationBuilder : BaseBuilder<ParameterDeclarationBuilder, ParameterDeclaration>
{
    public override string Build()
    {
        var udt = Model.IsUDTType ? "class" : "";
        return $"{udt} {Model.TypeName} {Model.Name}";
    }
}
