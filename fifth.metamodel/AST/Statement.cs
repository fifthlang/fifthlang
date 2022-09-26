namespace Fifth.AST;

public abstract class Statement : AstNode
{
    public StatementType TypeOfStatement { get; set; }
    public string StatementKind => TypeOfStatement.ToString();
}

public enum StatementType
{
    IfElse,
    While,
    With,
    VarDecl,
    Assignment,
    Return
}
