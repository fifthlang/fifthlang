namespace Fifth.CodeGeneration.IL;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class IfStatement : Statement
{
    public Expression Conditional { get; set; }
    public List<Statement> IfBlock { get; set; } = new();
    public List<Statement> ElseBlock { get; set; } = new();
}

public class IfStmtBuilder : BaseBuilder<IfStmtBuilder, IfStatement>
{
    public IfStmtBuilder()
    {
        Model = new IfStatement();
    }
    private static int IfLabelCounter = 1;

    public override string Build()
    {
        var labelId = Interlocked.Increment(ref IfLabelCounter);
        var sb = new StringBuilder();

        sb.AppendLine(ExpressionBuilder.Create(Model.Conditional).Build());
        sb.AppendLine($"brfalse.s LBL_ELSE_{labelId}");

        foreach (var statement in Model.IfBlock)
        {
            sb.AppendLine(StatementBuilder.Create(statement).Build());
        }

        sb.AppendLine($"br.s LBL_END_{labelId}");

        sb.AppendLine($"LBL_ELSE_{labelId}:");
        if (Model.ElseBlock.Any())
        {
            foreach (var statement in Model.ElseBlock)
            {
                sb.AppendLine(StatementBuilder.Create(statement).Build());
            }
        }

        sb.AppendLine($"LBL_END_{labelId}:");
        return sb.ToString();
    }

    public IfStmtBuilder WithConditional(Expression cond)
    {
        Model.Conditional = cond;
        return this;
    }

    public IfStmtBuilder WithIfStatement(Statement statement)
    {
        Model.IfBlock.Add(statement);
        return this;
    }
    public IfStmtBuilder WithElseStatement(Statement statement)
    {
        Model.ElseBlock.Add(statement);
        return this;
    }
}
