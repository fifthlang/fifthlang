using ast;

namespace compiler.Pipeline;

/// <summary>
/// Interface for a single compiler analysis/transformation phase.
/// Each phase declares its dependencies and capabilities, enabling
/// dependency validation and future parallel execution.
/// </summary>
public interface ICompilerPhase
{
    /// <summary>Unique human-readable name for this phase.</summary>
    string Name { get; }

    /// <summary>
    /// Capability strings that must be provided by earlier phases.
    /// Empty if this phase has no dependencies.
    /// </summary>
    IReadOnlyList<string> DependsOn { get; }

    /// <summary>
    /// Capability strings that this phase provides to subsequent phases.
    /// </summary>
    IReadOnlyList<string> ProvidedCapabilities { get; }

    /// <summary>
    /// Execute this phase's transformation on the AST.
    /// </summary>
    PhaseResult Transform(AstThing ast, PhaseContext context);
}
