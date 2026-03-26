using ast;

namespace Fifth.LangProcessingPhases;

/// <summary>
/// Backward-compatible facade that runs focused type annotation passes in order.
/// </summary>
public class TypeAnnotationVisitor : DefaultRecursiveDescentVisitor
{
    private readonly TypeAnnotationContext _context = new();
    private readonly KnowledgeGraphTypeAnnotationVisitor _pipelineVisitor;

    public TypeAnnotationVisitor()
    {
        _pipelineVisitor = new KnowledgeGraphTypeAnnotationVisitor(_context);
    }

    public IReadOnlyList<TypeCheckingError> Errors => _context.Errors.AsReadOnly();

    public override AstThing Visit(AstThing ctx) => _pipelineVisitor.Visit(ctx);
    public override AssemblyDef VisitAssemblyDef(AssemblyDef ctx) => _pipelineVisitor.VisitAssemblyDef(ctx);
    public override Int32LiteralExp VisitInt32LiteralExp(Int32LiteralExp ctx) => _pipelineVisitor.VisitInt32LiteralExp(ctx);
    public override Int64LiteralExp VisitInt64LiteralExp(Int64LiteralExp ctx) => _pipelineVisitor.VisitInt64LiteralExp(ctx);
    public override Float8LiteralExp VisitFloat8LiteralExp(Float8LiteralExp ctx) => _pipelineVisitor.VisitFloat8LiteralExp(ctx);
    public override Float4LiteralExp VisitFloat4LiteralExp(Float4LiteralExp ctx) => _pipelineVisitor.VisitFloat4LiteralExp(ctx);
    public override BooleanLiteralExp VisitBooleanLiteralExp(BooleanLiteralExp ctx) => _pipelineVisitor.VisitBooleanLiteralExp(ctx);
    public override StringLiteralExp VisitStringLiteralExp(StringLiteralExp ctx) => _pipelineVisitor.VisitStringLiteralExp(ctx);
    public override ListComprehension VisitListComprehension(ListComprehension ctx) => _pipelineVisitor.VisitListComprehension(ctx);
    public override ListLiteral VisitListLiteral(ListLiteral ctx) => _pipelineVisitor.VisitListLiteral(ctx);
    public override BinaryExp VisitBinaryExp(BinaryExp ctx) => _pipelineVisitor.VisitBinaryExp(ctx);
    public override FuncCallExp VisitFuncCallExp(FuncCallExp ctx) => _pipelineVisitor.VisitFuncCallExp(ctx);
    public override MemberAccessExp VisitMemberAccessExp(MemberAccessExp ctx) => _pipelineVisitor.VisitMemberAccessExp(ctx);
    public override IndexerExpression VisitIndexerExpression(IndexerExpression ctx) => _pipelineVisitor.VisitIndexerExpression(ctx);
    public override ModuleDef VisitModuleDef(ModuleDef ctx) => _pipelineVisitor.VisitModuleDef(ctx);
    public override FunctionDef VisitFunctionDef(FunctionDef ctx) => _pipelineVisitor.VisitFunctionDef(ctx);
    public override ParamDef VisitParamDef(ParamDef ctx) => _pipelineVisitor.VisitParamDef(ctx);
    public override VariableDecl VisitVariableDecl(VariableDecl ctx) => _pipelineVisitor.VisitVariableDecl(ctx);
    public override PropertyDef VisitPropertyDef(PropertyDef ctx) => _pipelineVisitor.VisitPropertyDef(ctx);
    public override VarRefExp VisitVarRefExp(VarRefExp ctx) => _pipelineVisitor.VisitVarRefExp(ctx);
    public override ObjectInitializerExp VisitObjectInitializerExp(ObjectInitializerExp ctx) => _pipelineVisitor.VisitObjectInitializerExp(ctx);
    public override SparqlLiteralExpression VisitSparqlLiteralExpression(SparqlLiteralExpression ctx) => _pipelineVisitor.VisitSparqlLiteralExpression(ctx);
    public override Interpolation VisitInterpolation(Interpolation ctx) => _pipelineVisitor.VisitInterpolation(ctx);

    public void OnTypeInferred(AstThing node, FifthType type) => _context.OnTypeInferred(node, type);

    public void OnTypeMismatch(AstThing node, FifthType type1, FifthType type2) => _context.OnTypeMismatch(node, type1, type2);

    public void OnTypeNotFound(AstThing node) => _context.OnTypeNotFound(node);

    public void OnTypeNotRelevant(AstThing node) => _context.OnTypeNotRelevant(node);
}

/// <summary>
/// Represents an error that occurred during type checking.
/// </summary>
public class TypeCheckingError
{
    public string Message { get; }
    public string Filename { get; }
    public int Line { get; }
    public int Column { get; }
    public IReadOnlyList<FifthType> Types { get; }
    public TypeCheckingSeverity Severity { get; }

    public TypeCheckingError(string message, string filename, int line, int column, IEnumerable<FifthType> types, TypeCheckingSeverity severity = TypeCheckingSeverity.Error)
    {
        Message = message;
        Filename = filename;
        Line = line;
        Column = column;
        Types = types.ToList().AsReadOnly();
        Severity = severity;
    }
}

/// <summary>
/// Severity levels for type checking errors
/// </summary>
public enum TypeCheckingSeverity
{
    Info,
    Warning,
    Error
}
