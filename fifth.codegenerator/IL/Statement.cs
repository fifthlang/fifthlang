namespace Fifth.CodeGeneration.IL;

using System.Text;

public abstract class Statement { }

public class VariableAssignmentStatement : Statement
{
    public int? Ordinal { get; set; }
    public string LHS { get; set; }
    public Expression RHS { get; set; }
}
public class ReturnStatement : Statement
{
    public Expression Exp { get; set; }
}

public class StatementBuilder : BaseBuilder<StatementBuilder, Statement>
{
    public StatementBuilder()
    {
        Model = null;
    }
    public StatementBuilder WithVariableAssignment(string lhs, Expression rhs, int? ord)
    {
        Model = new VariableAssignmentStatement { LHS = lhs, RHS = rhs, Ordinal = ord};
        return this;
    }
    public StatementBuilder WithReturnStatement( Expression rhs)
    {
        Model = new ReturnStatement() { Exp = rhs };
        return this;
    }

    public override string Build()
    {
        var result = Model switch
        {
            ReturnStatement rs => BuildReturn(rs),
            VariableAssignmentStatement vas => BuildAssignment(vas),
            _ => ""
        };
        return result;
    }

    private string BuildAssignment(VariableAssignmentStatement vas)
    {
        var sb = new StringBuilder();
        sb.AppendLine(ExpressionBuilder.Create(vas.RHS).Build());
        if (vas.Ordinal.HasValue && vas.Ordinal.Value < 4)
        {
            sb.AppendLine( $"stloc.{vas.Ordinal}");
        }
        else
        {
            sb.AppendLine($"stloc.s {vas.LHS}");
        }
        return sb.ToString();
    }

    private string BuildReturn(ReturnStatement returnStatement)
    {
        var sb = new StringBuilder();
        if (returnStatement.Exp != null)
        {
            sb.AppendLine(ExpressionBuilder.Create(returnStatement.Exp).Build());
        }

        sb.AppendLine("ret");
        return sb.ToString();
    }
}
