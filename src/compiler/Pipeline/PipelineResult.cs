using ast;

namespace compiler.Pipeline;

/// <summary>
/// Result of a complete pipeline execution.
/// </summary>
public record PipelineResult(
    AstThing? TransformedAst,
    IReadOnlyList<Diagnostic> Diagnostics,
    bool Success,
    IReadOnlyDictionary<string, TimeSpan> PhaseTimings
);
