namespace Fifth;

using TypeSystem;

public enum Operator : ushort
{
    // ArithmeticOperator
    [Op("add", "+")]Add,
    [Op("subtract", "-")] Subtract,
    [Op("multiply", "*")] Multiply,
    [Op("divide", "/")] Divide,
    [Op("remainder", "%%")] Rem,
    [Op("modulo", "%")] Mod,

    // LogicalOperators
    [Op("logical_and", "&&")] And,
    [Op("logical_or", "||")] Or,
    [Op("logical_not", "!", OperatorPosition.Prefix)] Not,
    [Op("logical_nand", "!&&")] Nand,
    [Op("logical_nor", "!||")] Nor,
    [Op("logical_xor", "!!||")] Xor,

    // RelationalOperators
    [Op("equals", "==")] Equal,
    [Op("not_equals", "!=")] NotEqual,
    [Op("less_than", "<")] LessThan,
    [Op("greater_than", ">")] GreaterThan,
    [Op("less_than_or_equal", "<=")] LessThanOrEqual,
    [Op("greater_than_or_equal", ">=")] GreaterThanOrEqual
}

public static class OpHelpers
{
    public static string ToIlOpCode(this Operator op)
    {
        return op switch
        {
            Operator.Add => "add",
            Operator.Subtract => "sub",
            Operator.Multiply => "mul",
            Operator.Divide => "div",
            Operator.Rem => "rem",
            Operator.Mod => "mod",
            Operator.And => "and",
            Operator.Or => "or",
            Operator.Not => "not",
            Operator.Nand => "nand",
            Operator.Nor => "nor",
            Operator.Xor => "xor",
            Operator.Equal => "eq",
            Operator.NotEqual => "neq",
            Operator.LessThan => "lt",
            Operator.GreaterThan => "gt",
            Operator.LessThanOrEqual => "leq",
            Operator.GreaterThanOrEqual => "geq",
            _ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
        };
    }
}
