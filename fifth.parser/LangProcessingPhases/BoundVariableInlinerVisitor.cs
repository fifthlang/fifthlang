namespace Fifth.Parser.LangProcessingPhases;

using System.Collections.Generic;
using AST.Visitors;
using Fifth.AST;

/// <summary>
/// Responsible for replacing bound variables with the fully qualified bound properties they are bound to.
/// </summary>
/// <remarks>
/// The purpose being to remove the possibility of there being different
/// bindings with the same name in different overloadings of the same
/// function.  The constraints on these clauses are combined during
/// the desugaring phase, and any clashes in variable definitions would
/// cause untold issues.
/// </remarks>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0021:Use expression body for constructors", Justification = "temporarily cleaning up display")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0022:Use expression body for methods", Justification = "temporarily cleaning up display")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1132:Remove redundant overriding member.", Justification = "temporarily cleaning up display")]
public class BoundVariableInlinerVisitor : BaseAstVisitor
{
    private Stack<Dictionary<string, CompoundVariableReference>> mappingsStack;

    public BoundVariableInlinerVisitor()
    {
        mappingsStack = new Stack<Dictionary<string, CompoundVariableReference>>();
    }

    public Dictionary<string, CompoundVariableReference> Mappings
        => mappingsStack.Peek();

    public override void EnterCompoundVariableReference(CompoundVariableReference ctx)
    {
        base.EnterCompoundVariableReference(ctx);
    }

    public override void EnterDestructuringParamDecl(DestructuringParamDecl ctx)
    {
        Mappings[ctx.ParameterName.Value] = new CompoundVariableReference(new List<VariableReference> { new VariableReference(ctx.ParameterName.Value) });
    }

    public override void EnterFunctionDefinition(FunctionDefinition ctx)
    {
        mappingsStack.Push(new Dictionary<string, CompoundVariableReference>());
    }

    public override void EnterPropertyBinding(PropertyBinding ctx)
    {
        Mappings[ctx.BoundVariableName] = ComputeBoundPropertyFullName(ctx);
    }

    public override void EnterVariableReference(VariableReference ctx)
    {
        base.EnterVariableReference(ctx);
    }

    public override void LeaveCompoundVariableReference(CompoundVariableReference ctx)
    {
        base.LeaveCompoundVariableReference(ctx);
    }

    public override void LeaveDestructuringParamDecl(DestructuringParamDecl ctx)
    {
        base.LeaveDestructuringParamDecl(ctx);
    }

    public override void LeaveFunctionDefinition(FunctionDefinition ctx)
    {
        // perform variable substitution
        mappingsStack.Pop();
    }

    public override void LeavePropertyBinding(PropertyBinding ctx)
    {
        base.LeavePropertyBinding(ctx);
    }

    public override void LeaveVariableReference(VariableReference ctx)
    {
        base.LeaveVariableReference(ctx);
    }

    private CompoundVariableReference ComputeBoundPropertyFullName(PropertyBinding ctx)
    {
        var components = new List<VariableReference>();
        var tmp = ctx as IAstNode;
        while (tmp is not ParameterDeclarationList)
        {
            components.Add(new VariableReference(GetNameFromEligibleAstNode(tmp)));
            tmp = tmp.ParentNode;
        }
        components.Reverse();
        return new CompoundVariableReference(components);
    }

    private string GetNameFromEligibleAstNode(IAstNode n)
    {
        return n switch
        {
            PropertyBinding pb => pb.BoundPropertyName,
            VariableReference vr => vr.Name,
            DestructuringParamDecl dpd => dpd.ParameterName.Value,
            _ => string.Empty
        };
    }

}
