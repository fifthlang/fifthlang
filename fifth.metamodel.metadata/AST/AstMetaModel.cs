namespace fifth.metamodel.metadata.AST;

#region

//using Fifth;
//using il;

#endregion

#region Core Abstractions

public enum SymbolKind
{
    Assembly, AssemblyRef, AssertionStmt, AssignmentStmt, Atom, BinaryExp, BlockStmt, CastExp, ClassDef, Expression,
    ExpStmt, FieldDef, ForeachStmt, ForStmt, FuncCallExp, Graph, GraphNamespaceAlias, GuardStmt, IfElseStmt,
    InferenceRule, InferenceRuleDef, KnowledgeMgmtStmt, LambdaDef, List, LiteralExp, MemberAccessExp, MemberDef,
    MemberRef, MethodDef, ObjectInstantiationExp, ParamDef, ParamDestructureDef, PropertyBindingDef, PropertyDef,
    RetractionStmt, ReturnStmt, StructDef, Triple, TypeDef, TypeRef, UnaryExp, VarDeclStmt, VarRef, VarRefExp,
    WhileStmt, WithScopeStmt
}

public record struct Symbol(string Name, SourceLocationMetadata Location, SymbolKind Kind);

public record struct SourceLocationMetadata(
    int Column,
    string Filename,
    int Line,
    string OriginalText);

public record struct TypeMetadata(TypeId TypeId, Symbol Symbol);

public abstract class AstThing
{
    public TypeMetadata Type { get; set; }
    public string Name { get; set; }
    public AstThing Parent { get; set; }
}

#endregion

#region Definitions

public abstract class Definition : AstThing
{
}

public class AssemblyDef : Definition
{
    public string PublicKeyToken { get; set; }
    public string Version { get; set; }
    public LinkedList<AssemblyRef> AssemblyRefs { get; set; }
    public LinkedList<ClassDef> ClassDefs { get; set; }
}

public class MemberDef : Definition
{
}

public class FieldDef : Definition
{
}

public class PropertyDef : Definition
{
}

public class MethodDef : Definition
{
}

public class InferenceRuleDef : Definition
{
}

public class ParamDef : Definition
{
}

public class ParamDestructureDef : Definition
{
}

public class PropertyBindingDef : Definition
{
}

public class TypeDef : Definition
{
}

public class ClassDef : Definition
{
    public LinkedList<MemberDef> MemberDefs { get; set; }
}

public class StructDef : Definition
{
}

public class LambdaDef : Definition
{
}

public class InferenceRule : Definition
{
}

#endregion

#region References

public abstract class Reference : AstThing
{
}

public class AssemblyRef : Reference
{
    public string PublicKeyToken{get;set;}
    public string Version{get;set;}

}

public class MemberRef : Reference
{
}

public class TypeRef : Reference
{
}

public class VarRef : Reference
{
}

public class GraphNamespaceAlias : Reference
{
}

#endregion

#region Statements

public abstract class Statement : AstThing
{
}

public class AssignmentStmt : Statement
{
}

public class BlockStmt : Statement
{
}

public class ExpStmt : Statement
{
}

public class ForStmt : Statement
{
}

public class ForeachStmt : Statement
{
}

public class GuardStmt : Statement
{
}

public class IfElseStmt : Statement
{
}

public class ReturnStmt : Statement
{
}

public class VarDeclStmt : Statement
{
}

public class WhileStmt : Statement
{
}

public abstract class KnowledgeMgmtStmt : Statement
{
}

public class AssertionStmt : Statement
{
}

public class RetractionStmt : Statement
{
}

public class WithScopeStmt : Statement
{
}

#endregion

#region Expressions

public abstract class Expression : AstThing
{
}

public class BinaryExp : Expression
{
}

public class CastExp : Expression
{
}

public class FuncCallExp : Expression
{
}

public class LiteralExp : Expression
{
}

public class MemberAccessExp : Expression
{
}

public class ObjectInstantiationExp : Expression
{
}

public class UnaryExp : Expression
{
}

public class VarRefExp : Expression
{
}

public class List : Expression
{
}

public class Atom : Expression
{
}

public class Triple : Expression
{
}

public class Graph : Expression
{
}

#endregion
