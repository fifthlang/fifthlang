namespace Fifth.CodeGeneration.IL;

using System.Text;
using System.Threading;
using fifth.metamodel.metadata.il;

public class StatementBuilder : BaseBuilder<StatementBuilder, Statement>
{
    public StatementBuilder()
    {
        Model = null;
    }

    public StatementBuilder WithVariableAssignment(string lhs, Expression rhs, int? ord)
    {
        Model = new VariableAssignmentStatement { LHS = lhs, RHS = rhs, Ordinal = ord };
        return this;
    }

    public StatementBuilder WithReturnStatement(Expression rhs)
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
            VariableDeclarationStatement vds => BuildVariableDeclaration(vds),
            _ => ""
        };
        return result;
    }

    private string BuildVariableDeclaration(VariableDeclarationStatement vds)
    {
        var sb = new StringBuilder();
        // there's not really anything to do here, because the locals section needs to be custom build in the function body
        return sb.ToString();
    }

    private string BuildAssignment(VariableAssignmentStatement vas)
    {
        var sb = new StringBuilder();
        sb.AppendLine(ExpressionBuilder.Create(vas.RHS).Build());
        if (vas.Ordinal.HasValue && vas.Ordinal.Value < 4)
        {
            sb.AppendLine($"stloc.{vas.Ordinal}");
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

public static class StatementBuilderFactory
{    public static IBuilder<Statement> Create<T>() where T : Statement
    {
        var type = typeof(T);
        if(type == typeof(VariableAssignmentStatement))
                return (IBuilder<Statement>)VariableAssignmentStatementBuilder.Create();
        else if (type == typeof(ReturnStatement))
            return (IBuilder<Statement>)ReturnStatementBuilder.Create();
        else if (type == typeof(WhileStatement))
            return (IBuilder<Statement>)WhileStatementBuilder.Create();
        else throw new TypeCheckingException("unknown Expression type");
    }

    public static IBuilder<T> Create<T>(T model) where T : Statement
    {
        return model switch
        {
            VariableAssignmentStatement vas => (IBuilder<T>)VariableAssignmentStatementBuilder.Create(vas),
            ReturnStatement rs => (IBuilder<T>)ReturnStatementBuilder.Create(rs),
            WhileStatement ws => (IBuilder<T>)WhileStatementBuilder.Create(ws),
            _ => (IBuilder<T>)StatementBuilder.Create(model)
        };
    }
}

public partial class VariableAssignmentStatementBuilder
{
    public override string Build()
    {
        var sb = new StringBuilder();
        sb.AppendLine(ExpressionBuilder.Create(Model.RHS).Build());
        if (Model.Ordinal.HasValue && Model.Ordinal.Value < 4)
        {
            sb.AppendLine($"stloc.{Model.Ordinal}");
        }
        else
        {
            sb.AppendLine($"stloc.s {Model.LHS}");
        }

        return sb.ToString();
    }
}

public partial class ReturnStatementBuilder
{
    public override string Build()
    {
        var sb = new StringBuilder();
        if (Model.Exp != null)
        {
            sb.AppendLine(ExpressionBuilderFactory.Create(Model.Exp).Build());
        }

        sb.AppendLine("ret");
        return sb.ToString();
    }
}

public partial class WhileStatementBuilder
{
    private static int labelCounter = 0;

    public override string Build()
    {
        var sb = new StringBuilder();
        var ctr = Interlocked.Increment(ref labelCounter);
        sb.AppendLine($"LBL_WHSTART{ctr:0000}:");
        sb.AppendLine(ExpressionBuilderFactory.Create(Model.Conditional).Build());
        sb.AppendLine($"brfalse.s LBL_WHEND{ctr:0000}");
        foreach (var statement in Model.LoopBlock.Statements)
        {
            sb.AppendLine(StatementBuilder.Create(statement).Build());
        }
        sb.AppendLine($"br.s LBL_WHSTART{ctr:0000}");
        sb.AppendLine($"LBL_WHEND{ctr:0000}:");
        return sb.ToString();
    }
}
