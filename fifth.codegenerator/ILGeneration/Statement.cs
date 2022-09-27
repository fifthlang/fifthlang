namespace Fifth.CodeGeneration.ILGeneration;

using System.Collections.Specialized;
using System.Text;

public abstract class Statement { }

public class VariableAssignmentStatement : Statement
{
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
    public StatementBuilder WithVariableAssignment(string lhs, Expression rhs)
    {
        Model = new VariableAssignmentStatement { LHS = lhs, RHS = rhs };
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

    private string BuildAssignment(VariableAssignmentStatement variableAssignmentStatement)
    {
        var sb = new StringBuilder();
        sb.AppendLine(ExpressionBuilder.Create(variableAssignmentStatement.RHS).Build());
        sb.AppendLine($"stloc {variableAssignmentStatement.LHS}");
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
