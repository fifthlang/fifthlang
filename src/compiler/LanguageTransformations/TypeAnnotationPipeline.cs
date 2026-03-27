using ast;
using ast_generated;

namespace Fifth.LangProcessingPhases;

public interface ITypeAnnotationStage
{
    AstThing Apply(AstThing ast, TypeAnnotationContext context);
}

public interface ITypeAnnotationPipeline
{
    AstThing Annotate(AstThing ast);
    IReadOnlyList<TypeCheckingError> Errors { get; }
}

public abstract class TypeAnnotationRewriterStageBase(TypeAnnotationContext context) : DefaultAstRewriter
{
    protected readonly TypeAnnotationContext Context = context;

    public AstThing Apply(AstThing ast) => (AstThing)Rewrite(ast).Node;
}

public sealed class TypeAnnotationPipeline : ITypeAnnotationPipeline
{
    private readonly TypeAnnotationContext _context;
    private readonly IReadOnlyList<ITypeAnnotationStage> _stages;

    public TypeAnnotationPipeline(
        IEnumerable<ITypeAnnotationStage>? stages = null,
        TypeAnnotationContext? context = null)
    {
        _context = context ?? new TypeAnnotationContext();
        _stages = (stages?.ToList() ?? [
            new StandardTypeAnnotationStage(),
            new GenericTypeAnnotationStage(),
            new KnowledgeGraphTypeAnnotationStage()
        ]).AsReadOnly();
    }

    public AstThing Annotate(AstThing ast)
    {
        var current = ast;
        foreach (var stage in _stages)
        {
            current = stage.Apply(current, _context);
        }

        return current;
    }

    public IReadOnlyList<TypeCheckingError> Errors => _context.Errors.AsReadOnly();
}

public sealed class StandardTypeAnnotationStage : ITypeAnnotationStage
{
    public AstThing Apply(AstThing ast, TypeAnnotationContext context)
        => new StandardTypeAnnotationRewriter(context).Apply(ast);
}

public sealed class GenericTypeAnnotationStage : ITypeAnnotationStage
{
    public AstThing Apply(AstThing ast, TypeAnnotationContext context)
        => new GenericTypeAnnotationRewriter(context).Apply(ast);
}

public sealed class KnowledgeGraphTypeAnnotationStage : ITypeAnnotationStage
{
    public AstThing Apply(AstThing ast, TypeAnnotationContext context)
        => new KnowledgeGraphTypeAnnotationRewriter(context).Apply(ast);
}
