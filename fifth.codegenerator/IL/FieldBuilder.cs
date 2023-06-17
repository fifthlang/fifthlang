namespace Fifth.CodeGeneration.IL;

using System.Text;
using fifth.metamodel.metadata.il;

public partial class FieldDefinitionBuilder: BaseBuilder<FieldDefinitionBuilder, FieldDefinition>
{
    public override string Build()
    {
        var sb = new StringBuilder();

        sb.Append($".field {Model.Visibility} {Model.TheType.Name} '{Model.Name}'");

        return sb.ToString();
    }
}
