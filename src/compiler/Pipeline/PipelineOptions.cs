using ast;

namespace compiler.Pipeline;

/// <summary>
/// Configuration for pipeline execution.
/// </summary>
public record PipelineOptions
{
    /// <summary>Phase names to skip during execution.</summary>
    public HashSet<string> SkipPhases { get; init; } = new();

    /// <summary>Stop execution after this phase (inclusive). Null = run all.</summary>
    public string? StopAfter { get; init; }

    /// <summary>Stop on first phase failure. Default true.</summary>
    public bool StopOnError { get; init; } = true;

    /// <summary>Enable phase-level caching (future use). Default false.</summary>
    public bool EnableCaching { get; init; }

    /// <summary>Phase names after which to dump AST state.</summary>
    public HashSet<string>? DumpAfter { get; init; }

    /// <summary>Callback invoked for AST dumps. Receives (ast, phaseName).</summary>
    public Action<AstThing, string>? DumpCallback { get; init; }

    /// <summary>Default options: run all phases, stop on error, skip TailCallOptimization (currently disabled).</summary>
    public static PipelineOptions Default { get; } = new()
    {
        SkipPhases = new HashSet<string> { "TailCallOptimization" }
    };
}
