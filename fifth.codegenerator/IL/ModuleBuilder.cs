namespace Fifth.CodeGeneration.IL;

using System.Text;
using fifth.metamodel.metadata.il;
public partial class ModuleDeclarationBuilder : BaseBuilder<ModuleDeclarationBuilder, ModuleDeclaration>
{
    public override string Build()
    {
        var sb = new StringBuilder();
        sb.AppendLine($@".module{{ {Model.FileName} }}");

        foreach (var c in Model.Classes)
        {
            sb.AppendLine(ClassDefinitionBuilder.Create(c).Build());
        }
        foreach (var f in Model.Functions)
        {
            sb.AppendLine(MethodDefinitionBuilder.Create(f).Build());
        }

        return sb.ToString();
    }
}
