namespace fifth.metamodel.metadata.il;

public class AssemblyDeclaration
{
    public string Name { get; set; }
    public Version Version { get; set; }
    public ModuleDeclaration PrimeModule { get; set; }
    public List<AssemblyReference> AssemblyReferences { get; set; } = new();
}

public class AssemblyReference
{
    public string Name { get; set; }
    public string PublicKeyToken { get; set; }
    public Version Version { get; set; }
}

public enum ILVisibility
{
    Public,
    Private
}

public class ClassDefinition
{
    public List<FieldDefinition> Fields { get; set; } = new();
    public List<PropertyDefinition> Properties { get; set; } = new();
    public List<MethodDefinition> Methods { get; set; } = new();
    public string Name { get; set; }
    public string Namespace { get; set; }
    public List<ClassDefinition> BaseClasses { get; set; } = new();
    public AssemblyDeclaration ParentAssembly { get; set; }
    public ILVisibility Visibility { get; set; } = ILVisibility.Private;
}

public abstract class Expression
{}

[Ignore]
public class Literal<T> : Expression, ILiteralValue
{
    public Literal(T value) => Value = value;

    public T Value { get; set; }

    public string TypeName => typeof(T).Name;
}

public class UnaryExpression : Expression
{
    public UnaryExpression() { }

    public UnaryExpression(string op, Expression exp)
    {
        Op = op;
        Exp = exp;
    }

    public string Op { get; set; }
    public Expression Exp { get; set; }
}

public class BinaryExpression : Expression
{
    public BinaryExpression() { }

    public BinaryExpression(string op, Expression lhs, Expression rhs)
    {
        Op = op;
        LHS = lhs;
        RHS = rhs;
    }

    public string Op { get; set; }
    public Expression LHS { get; set; }
    public Expression RHS { get; set; }
}

public class TypeCastExpression : Expression
{
    public TypeCastExpression() { }

    public TypeCastExpression(string targetTypeName, string targetTypeCilName, Expression expression)
    {
        TargetTypeName = targetTypeName;
        TargetTypeCilName = targetTypeCilName;
        Expression = expression;
    }

    public string TargetTypeName { get; set; }
    public string TargetTypeCilName { get; set; }
    public Expression Expression { get; set; }
}

public class FuncCallExp : Expression
{
    public FuncCallExp()
    {
    }

    public FuncCallExp(string name, string typename, Expression[] args)
    {
        Name = name;
        Args = new List<Expression>(args);
        ArgTypes = new List<string>();
        ReturnType = typename;
    }

    public string Name { get; set; }
    public List<Expression> Args { get; set; }
    public string ReturnType { get; set; }
    public ClassDefinition ClassDefinition { get; set; }
    public List<string> ArgTypes { get; set; }
}

public interface ILiteralValue
{
    public string TypeName { get; }
}

public enum MemberType
{
    Unknown,
    Field,
    Property,
    Method
}

[Ignore]
public abstract class MemberDefinition
{
    public ClassDefinition ParentClass { get; set; }
    public PropertyDefinition AssociatedProperty { get; set; }
    public MemberType TypeOfMember { get; set; }
    public bool IsVirtual { get; set; }
    public bool IsStatic { get; set; }
    public ILVisibility Visibility { get; set; }

}

public class MemberAccessExpression : Expression
{
    public Expression Lhs { get; set; }
    public Expression Rhs { get; set; }
}
public class VariableReferenceExpression : Expression
{
    public string Name { get; set; }
    public object SymTabEntry { get; set; }
    public bool IsParameterReference { get; set; }
    public int Ordinal { get; set; }
}

public class FieldDefinition : MemberDefinition
{
    public string TypeName { get; set; }
    public string Name { get; set; }
}

public class IfStatement : Statement
{
    public Expression Conditional { get; set; }
    public Block IfBlock { get; set; } = new();
    public Block? ElseBlock { get; set; } = new();
}

public class MethodDefinition : MemberDefinition
{
    public string Name { get; set; }
    public string ReturnType { get; set; }
    public List<ParameterDeclaration> Parameters { get; set; } = new();
    public ILVisibility Visibility { get; set; }
    public Block Body { get; set; }
    public FunctionKind FunctionKind { get; set; }
    public bool IsStatic { get; set; }
    public bool IsEntrypoint { get; set; }
}

public class Block
{
    public List<Statement> Statements { get; set; } = new();
}

public class ModuleDeclaration
{
    public string FileName { get; set; }
    public List<ClassDefinition> Classes { get; set; } = new();
    public List<MethodDefinition> Functions { get; set; } = new();
}

public class ParameterDeclaration
{
    public string Name { get; set; }
    public string TypeName { get; set; }
    public bool IsUDTType { get; set; }
}

public class PropertyDefinition : MemberDefinition
{
    public string TypeName { get; set; }
    public string Name { get; set; }
    public FieldDefinition? FieldDefinition { get; set; }
}

[Ignore]
public abstract class Statement
{}

public class VariableAssignmentStatement : Statement
{
    public int? Ordinal { get; set; }
    public string LHS { get; set; }
    public Expression RHS { get; set; }
}

public class VariableDeclarationStatement : Statement
{
    public int? Ordinal { get; set; }
    public string Name { get; set; }
    public string TypeName { get; set; }
    public Expression? InitialisationExpression { get; set; }

}
public class ReturnStatement : Statement
{
    public Expression Exp { get; set; }
}

public class Version
{
    public Version(int Major, int? Minor, int? Build, int? Patch)
    {
        this.Major = Major;
        this.Minor = Minor;
        this.Build = Build;
        this.Patch = Patch;
    }

    public Version():this(0,0,0,0) { }

    public Version(string s)
    {
        var segs = Array.Empty<string>();
        if (s.Contains('.'))
        {
            segs = s.Split('.');
        }
        else if (s.Contains(':'))
        {
            segs = s.Split(':');
        }

        if (segs.Length > 0)
        {
            if (int.TryParse(segs[0], out var major))
            {
                Major = major;
            }
        }

        if (segs.Length > 1)
        {
            if (int.TryParse(segs[1], out var minor))
            {
                Minor = minor;
            }
        }

        if (segs.Length > 2)
        {
            if (int.TryParse(segs[2], out var build))
            {
                Build = build;
            }
        }

        if (segs.Length > 3)
        {
            if (int.TryParse(segs[3], out var patch))
            {
                Patch = patch;
            }
        }
    }

    public int Major { get; set; }
    public int? Minor { get; set; }
    public int? Build { get; set; }
    public int? Patch { get; set; }

    public void Deconstruct(out int Major, out int? Minor, out int? Build, out int? Patch)
    {
        Major = this.Major;
        Minor = this.Minor;
        Build = this.Build;
        Patch = this.Patch;
    }
}

public class WhileStatement : Statement
{
    public Expression Conditional { get; set; }
    public Block LoopBlock { get; set; } = new();
}
public class ExpressionStatement : Statement
{
    public Expression Expression { get; set; }
}
