using ast;
using compiler.LanguageTransformations;
using Fifth;

namespace compiler.Pipeline.Phases;

/// <summary>
/// Phase 12: Expands property definitions into backing fields and accessor functions.
/// Uses specialised error handling: logs to DebugLog and re-throws WITHOUT adding a diagnostic.
/// </summary>
public class PropertyToFieldPhase : ICompilerPhase
{
    public string Name => "PropertyToField";
    public IReadOnlyList<string> DependsOn => new[] { "TreeStructure" };
    public IReadOnlyList<string> ProvidedCapabilities => new[] { "FieldExpansion" };

    public PhaseResult Transform(AstThing ast, PhaseContext context)
    {
        try
        {
            var result = new PropertyToFieldExpander().Visit(ast);
            return PhaseResult.Ok(result);
        }
        catch (System.Exception ex)
        {
            if (DebugHelpers.DebugEnabled)
            {
                DebugHelpers.DebugLog($"PropertyToFieldExpander failed with: {ex.Message}");
                DebugHelpers.DebugLog($"Stack trace: {ex.StackTrace}");
            }
            throw;
        }
    }
}
