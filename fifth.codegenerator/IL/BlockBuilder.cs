namespace Fifth.CodeGeneration.IL;

using System.Text;

public partial class BlockBuilder
{
    public override string Build()
    {
        var sb = new StringBuilder();
        sb.AppendLine("{");
        foreach (var statement in Model.Statements)
        {
            sb.AppendLine(StatementBuilder.Create(statement).Build());
        }
        sb.AppendLine("}");
        return sb.ToString();
    }
}
