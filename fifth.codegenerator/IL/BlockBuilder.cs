namespace Fifth.CodeGeneration.IL;

using System.Text;

public partial class BlockBuilder
{
    public override string Build() => Build(true);
    public string Build(bool encloseInBraces)
    {
        var sb = new StringBuilder();
        if(encloseInBraces) sb.AppendLine("{");
        foreach (var statement in Model.Statements)
        {
            sb.AppendLine(StatementBuilder.Create(statement).Build());
        }
        if(encloseInBraces) sb.AppendLine("}");
        return sb.ToString();
    }
}
