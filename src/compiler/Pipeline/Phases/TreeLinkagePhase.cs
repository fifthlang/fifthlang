using ast;
using compiler.LanguageTransformations;
using Fifth;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 1: Links parent-child relationships in the AST.
/// Uses specialised error handling: logs to DebugLog and adds diagnostic before re-throwing.
/// </summary>
public class TreeLinkagePhase : ICompilerPhase
{
    public string Name => "TreeLinkage";
    public IReadOnlyList<string> DependsOn => Array.Empty<string>();
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "TreeStructure" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        try
        {
            var result = new TreeLinkageVisitor().Visit(ast);
            return PhaseResult.Ok(result);
        }
        catch (System.Exception ex)
        {
            if (DebugHelpers.DebugEnabled)
            {
                DebugHelpers.DebugLog($"TreeLinkageVisitor failed with: {ex.Message}");
                DebugHelpers.DebugLog($"Stack trace: {ex.StackTrace}");
            }
            context.Diagnostics.Add(new Diagnostic(DiagnosticLevel.Error,
                $"TreeLinkageVisitor failed: {ex.Message}\nStack: {ex.StackTrace}"));
            throw;
        }
    }
}
