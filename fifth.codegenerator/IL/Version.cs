namespace Fifth.CodeGeneration.IL;

using System.Text;

public partial class VersionBuilder
{
    public char RenderingDelimiter { get; set; } = ':';

    public override string Build()
    {
        var sb = new StringBuilder();
        sb.Append(Model.Major);
        sb.Append(RenderingDelimiter);
        if (Model.Minor.HasValue)
        {
            sb.Append(Model.Minor);
            sb.Append(RenderingDelimiter);
        }
        if (Model.Build.HasValue)
        {
            sb.Append(Model.Build);
            sb.Append(RenderingDelimiter);
        }
        if (Model.Patch.HasValue)
        {
            sb.Append(Model.Patch);
            sb.Append(RenderingDelimiter);
        }

        return sb.ToString();
    }
}
