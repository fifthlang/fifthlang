namespace Fifth.CodeGeneration.IL;

using System.Collections.Generic;
using System.Text;
using fifth.metamodel.metadata.il;

public partial class ProgramDefinitionBuilder : BaseBuilder<ProgramDefinitionBuilder, ProgramDefinition>
{
    public override string Build()
    {
        var sb = new StringBuilder();

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
