namespace Fifth.CodeGeneration.IL;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using fifth.metamodel.metadata.il;


public partial class IfStatementBuilder : BaseBuilder<IfStatementBuilder, IfStatement>
{
    private static int IfLabelCounter = 1;

    public override string Build()
    {
        var labelId = Interlocked.Increment(ref IfLabelCounter);
        var sb = new StringBuilder();

        sb.AppendLine(ExpressionBuilder.Create(Model.Conditional).Build());
        sb.AppendLine($"brfalse.s LBL_ELSE_{labelId:0000}");

        foreach (var statement in Model.IfBlock.Statements)
        {
            sb.AppendLine(StatementBuilder.Create(statement).Build());
        }

        sb.AppendLine($"br.s LBL_END_{labelId:0000}");

        sb.AppendLine($"LBL_ELSE_{labelId:0000}:");
        if (Model.ElseBlock.Statements.Any())
        {
            foreach (var statement in Model.ElseBlock.Statements)
            {
                sb.AppendLine(StatementBuilder.Create(statement).Build());
            }
        }

        sb.AppendLine($"LBL_END_{labelId:0000}:");
        return sb.ToString();
    }
}
