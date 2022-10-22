namespace Fifth.CodeGeneration.IL;

using System.Collections.Generic;
using System.Text;
using System.Threading;

using fifth.metamodel.metadata.il;

public class WhileStmtBuilder : BaseBuilder<WhileStmtBuilder, WhileStatement>
{
    public WhileStmtBuilder()
    {
        Model = new WhileStatement();
    }
    private static int LabelCounter = 0;

    public override string Build()
    {
        var labelId = Interlocked.Increment(ref LabelCounter);
        var sb = new StringBuilder();

        sb.AppendLine($"LBL_START_{labelId}:");
        sb.AppendLine(ExpressionBuilder.Create(Model.Conditional).Build());
        sb.AppendLine($"brfalse.s LBL_END_{labelId}");

        foreach (var statement in Model.LoopBlock.Statements)
        {
            sb.AppendLine(StatementBuilder.Create(statement).Build());
        }

        sb.AppendLine($"br.s LBL_START_{labelId}");

        sb.AppendLine($"LBL_END_{labelId}:");
        return sb.ToString();
    }

    public WhileStmtBuilder WithConditional(Expression cond)
    {
        Model.Conditional = cond;
        return this;
    }

    public WhileStmtBuilder WithStatement(Statement statement)
    {
        Model.LoopBlock.Statements.Add(statement);
        return this;
    }
}
