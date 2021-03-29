namespace Fifth
{
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
}
