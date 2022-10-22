namespace Fifth.CodeGeneration.IL;

using System.Collections.Generic;
using System.Text;

using fifth.metamodel.metadata.il;

public partial class ClassDefinitionBuilder
{
    public override string Build()
    {
        var sb = new StringBuilder();
        sb.AppendLine($".class {Model.Visibility} auto ansi {Model.Name} extends [System.Runtime]System.Object {{");
        foreach (var field in Model.Fields)
        {
            sb.AppendLine(FieldDefinitionBuilder.Create(field).Build());
        }
        foreach (var prop in Model.Properties)
        {
            sb.AppendLine(PropertyDefinitionBuilder.Create(prop).Build());
        }
        foreach (var method in Model.Methods)
        {
            sb.AppendLine(MethodDefinitionBuilder.Create(method).Build());
        }
        sb.AppendLine("}");
        return sb.ToString();
    }

}
