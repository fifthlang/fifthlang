namespace Fifth.CodeGeneration.IL;

using System.Text;
using fifth.metamodel.metadata.il;

public partial class PropertyDefinitionBuilder: BaseBuilder<PropertyDefinitionBuilder, PropertyDefinition>
{
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
