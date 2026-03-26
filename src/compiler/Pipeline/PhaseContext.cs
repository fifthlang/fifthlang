namespace compiler.Pipeline;

/// <summary>
/// Shared context passed through all pipeline phases.
/// Carries diagnostics, configuration, and extensible shared data.
/// </summary>
public class PhaseContext
{
    /// <summary>Accumulated diagnostics from all phases.</summary>
    public List<Diagnostic> Diagnostics { get; } = new();

    /// <summary>Target framework for external call validation.</summary>
    public string? TargetFramework { get; init; }

    /// <summary>
    /// Extensible shared data dictionary for inter-phase communication.
    /// Phases store symbol tables, type registries, etc. using well-known keys.
    /// </summary>
    public Dictionary<string, object> SharedData { get; } = new();

    /// <summary>Whether phase-level caching is enabled (future use).</summary>
    public bool EnableCaching { get; init; }
}
