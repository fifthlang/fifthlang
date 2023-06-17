// ReSharper disable once CheckNamespace
namespace fifth.metamodel.metadata.il;

#region Flags

public enum MemberType
{
    Unknown,
    Field,
    Property,
    Method
}

// II.23.1.10
public enum MemberAccessability
{
    CompilerControlled, // 0x0000 Member not referenceable
    Private, // 0x0001 Accessible only by the parent type
    FamANDAssem, // 0x0002 Accessible by sub-types only in this Assembly
    Assem, // 0x0003 Accessibly by anyone in the Assembly
    Family, // 0x0004 Accessible only by type and sub-types
    FamORAssem, // 0x0005 Accessibly by sub-types anywhere, plus anyone in assembly
    Public, // 0x0006 Accessibly by anyone who has visibility to this scope

}

public enum VtableLayoutMask
{
    ReuseSlot, // 0x0000 Method reuses existing slot in vtable
    NewSlot, // 0x0100 Method always gets a new slot in the vtable
}



public enum InOutFlag
{
    In, Out, Opt
}

public enum CodeTypeFlag : int
{
    cil,
    native,
    optil,
    runtime,
}



public enum ImplementationFlag : int
{
    forwardRef,
    preserveSig,
    internalCall,
    synchronized,
    oninlining
}


public enum MethodCallingConvention
{
    Default /*i.e. static */,
    Vararg,
    Instance,
    InstanceVararg,
    Property
}
#endregion

#region Assemblies


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




public class ModuleDeclaration
{
    public string FileName { get; set; }
    public List<ClassDefinition> Classes { get; set; } = new();
    public List<MethodDefinition> Functions { get; set; } = new();
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


#endregion

#region Classes


public class ClassDefinition
{
    public List<FieldDefinition> Fields { get; set; } = new();
    public List<PropertyDefinition> Properties { get; set; } = new();
    public List<MethodDefinition> Methods { get; set; } = new();
    public string Name { get; set; }
    public string Namespace { get; set; }
    public List<ClassDefinition> BaseClasses { get; set; } = new();
    public AssemblyDeclaration ParentAssembly { get; set; }
    public MemberAccessability Visibility { get; set; } = MemberAccessability.Private;
}


#endregion

#region Members

[Ignore]
public abstract class MemberDefinition
{
    public string Name { get; set; }
    public TypeReference TheType { get; set; }
    public ClassDefinition ParentClass { get; set; }
    public PropertyDefinition AssociatedProperty { get; set; }
    public MemberType TypeOfMember { get; set; }
    public bool IsStatic { get; set; }
    public bool IsFinal { get; set; }
    public bool IsVirtual { get; set; }

    public bool IsStrict { get; set; }
    public bool IsAbstract { get; set; }

    public bool IsSpecialName { get; set; }
    // Method hides by name+sig, else just by name
    public bool HideBySig { get; set; }
    public MemberAccessability Visibility { get; set; }

}

public class MemberAccessExpression : Expression
{
    public Expression Lhs { get; set; }
    public Expression Rhs { get; set; }
}

public class MemberRef
{
    public MemberTarget Target { get; set; }
    public ClassDefinition ClassDefinition { get; set; }
    public string Name { get; set; }
    public MethodSignature Sig { get; set; }
    public FieldDefinition Field { get; set; }
}

public enum MemberTarget
{
    Method, Field
}
#endregion

#region Methods

public class ParameterDeclaration
{
    public string Name { get; set; }
    public string TypeName { get; set; }
    public bool IsUDTType { get; set; }
}
public class ParameterSignature
{
    public InOutFlag InOut { get; set; }
    public string Name { get; set; }
    public TypeReference TypeReference { get; set; }
    public bool IsUDTType { get; set; } // internal use
}


//Two method signatures are said to match if and only if:
//     the calling conventions are identical; 
//     both signatures are either static or instance; 
//     the number of generic parameters is identical, if the method is generic; 
//     for instance signatures the type of the this pointer of the overriding/hiding
//signature is assignable-to (§I.8.7) the type of the this pointer of the
//overridden/hidden signature; 
//     the number and type signatures of the parameters are identical; and 
//     the type signatures for the result are identical. [Note: This includes void 
//(§II.23.2.11) if no value is returned.end note]
public class MethodSignature
{
    public MethodCallingConvention CallingConvention{get;set;}
    public ushort NumberOfParameters{get;set;}
    public List<ParameterSignature> ParameterSignatures { get; set; } = new();
    public TypeReference ReturnTypeSignature {get;set;}

}

public class MethodHeader
{
    public FunctionKind FunctionKind { get; set; }
    public bool IsEntrypoint { get; set; }
    
}

public class MethodRef : MemberRef
{
    
}

public class MethodImpl
{
    public ImplementationFlag ImplementationFlags { get; set; }
    public bool IsManaged { get; set; }

    public Block Body { get; set; }
}

public class MethodDefinition : MemberDefinition
{
    public MethodHeader Header { get; set; }
    public MethodSignature Signature { get; set; }
    public MethodImpl Impl { get; set; }
    public CodeTypeFlag CodeTypeFlags { get; set; }
}

public class TypeReference
{
    public string Namespace { get; set; }
    public string Name { get; set; }
    public string ModuleName { get; set; }
}

#endregion

#region Fields

public class FieldDefinition : MemberDefinition
{
}

#endregion

#region Properties


public class PropertyDefinition : MemberDefinition
{
    public string TypeName { get; set; }
    public string Name { get; set; }
    public FieldDefinition? FieldDefinition { get; set; }
}


#endregion

#region Statements
[Ignore]
public abstract class Statement
{}

public class Block
{
    public List<Statement> Statements { get; set; } = new();
}


public class IfStatement : Statement
{
    public Expression Conditional { get; set; }
    public Block IfBlock { get; set; } = new();
    public Block? ElseBlock { get; set; } = new();
}

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


public class WhileStatement : Statement
{
    public Expression Conditional { get; set; }
    public Block LoopBlock { get; set; } = new();
}
public class ExpressionStatement : Statement
{
    public Expression Expression { get; set; }
}



#endregion

#region Expressions

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

public class VariableReferenceExpression : Expression
{
    public string Name { get; set; }
    public object SymTabEntry { get; set; }
    public bool IsParameterReference { get; set; }
    public int Ordinal { get; set; }
}
public interface ILiteralValue
{
    public string TypeName { get; }
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

#endregion
