using ast;

namespace Fifth.LangProcessingPhases;

/// <summary>
/// Type annotation entrypoint that delegates to a rewriter-based stage pipeline.
/// </summary>
public class TypeAnnotationVisitor : DefaultRecursiveDescentVisitor
{
    private readonly ITypeAnnotationPipeline _pipeline;

    public TypeAnnotationVisitor() : this(new TypeAnnotationPipeline()) { }
    public TypeAnnotationVisitor(ITypeAnnotationPipeline pipeline) => _pipeline = pipeline;

    public IReadOnlyList<TypeCheckingError> Errors => _pipeline.Errors;

    public override AstThing Visit(AstThing ctx) => _pipeline.Annotate(ctx);
    public override AssemblyDef VisitAssemblyDef(AssemblyDef ctx) => (AssemblyDef)_pipeline.Annotate(ctx);
    public override Int32LiteralExp VisitInt32LiteralExp(Int32LiteralExp ctx) => (Int32LiteralExp)_pipeline.Annotate(ctx);
    public override Int64LiteralExp VisitInt64LiteralExp(Int64LiteralExp ctx) => (Int64LiteralExp)_pipeline.Annotate(ctx);
    public override Float8LiteralExp VisitFloat8LiteralExp(Float8LiteralExp ctx) => (Float8LiteralExp)_pipeline.Annotate(ctx);
    public override Float4LiteralExp VisitFloat4LiteralExp(Float4LiteralExp ctx) => (Float4LiteralExp)_pipeline.Annotate(ctx);
    public override BooleanLiteralExp VisitBooleanLiteralExp(BooleanLiteralExp ctx) => (BooleanLiteralExp)_pipeline.Annotate(ctx);
    public override StringLiteralExp VisitStringLiteralExp(StringLiteralExp ctx) => (StringLiteralExp)_pipeline.Annotate(ctx);
    public override ListComprehension VisitListComprehension(ListComprehension ctx) => (ListComprehension)_pipeline.Annotate(ctx);
    public override ListLiteral VisitListLiteral(ListLiteral ctx) => (ListLiteral)_pipeline.Annotate(ctx);
    public override BinaryExp VisitBinaryExp(BinaryExp ctx) => (BinaryExp)_pipeline.Annotate(ctx);
    public override FuncCallExp VisitFuncCallExp(FuncCallExp ctx) => (FuncCallExp)_pipeline.Annotate(ctx);
    public override MemberAccessExp VisitMemberAccessExp(MemberAccessExp ctx) => (MemberAccessExp)_pipeline.Annotate(ctx);
    public override IndexerExpression VisitIndexerExpression(IndexerExpression ctx) => (IndexerExpression)_pipeline.Annotate(ctx);
    public override ModuleDef VisitModuleDef(ModuleDef ctx) => (ModuleDef)_pipeline.Annotate(ctx);
    public override FunctionDef VisitFunctionDef(FunctionDef ctx) => (FunctionDef)_pipeline.Annotate(ctx);
    public override ParamDef VisitParamDef(ParamDef ctx) => (ParamDef)_pipeline.Annotate(ctx);
    public override VariableDecl VisitVariableDecl(VariableDecl ctx) => (VariableDecl)_pipeline.Annotate(ctx);
    public override PropertyDef VisitPropertyDef(PropertyDef ctx) => (PropertyDef)_pipeline.Annotate(ctx);
    public override VarRefExp VisitVarRefExp(VarRefExp ctx) => (VarRefExp)_pipeline.Annotate(ctx);
    public override ObjectInitializerExp VisitObjectInitializerExp(ObjectInitializerExp ctx) => (ObjectInitializerExp)_pipeline.Annotate(ctx);
    public override SparqlLiteralExpression VisitSparqlLiteralExpression(SparqlLiteralExpression ctx) => (SparqlLiteralExpression)_pipeline.Annotate(ctx);
    public override Interpolation VisitInterpolation(Interpolation ctx) => (Interpolation)_pipeline.Annotate(ctx);
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
