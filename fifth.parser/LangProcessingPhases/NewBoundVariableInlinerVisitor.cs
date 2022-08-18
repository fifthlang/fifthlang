namespace Fifth.Parser.LangProcessingPhases;

using System.Collections.Generic;
using AST.Visitors;
using Fifth.AST;
using Fifth.AST.Builders;

/// <summary>
/// DestructuringPatternFlattenerVisitor is responsible for desugaring the recursive strucure representing a destructuring pattern definition
/// </summary>
public class DestructuringPatternFlattenerVisitor : DefaultMutatorVisitor<BoundVariablesContext>
{
    public static (DestructuringPatternFlattenerVisitor, BoundVariablesContext) CreateVisitor()
    {
        var ctx = new BoundVariablesContext();
        var visitor = new DestructuringPatternFlattenerVisitor();
        return (visitor, ctx);
    }

    public override DestructuringParamDecl ProcessDestructuringParamDecl(DestructuringParamDecl node, BoundVariablesContext ctx)
    {
        var builder = DestructuringParamDeclBuilder.CreateDestructuringParamDecl()
            .WithParameterName(node.ParameterName)
            .WithTypeName(node.TypeName);
        foreach (var pb in node.PropertyBindings)
        {
            if (ctx.SubstitutionsStack.ContainsKey(pb.BoundVariableName))
            {
                var b2 = PropertyBindingBuilder.CreatePropertyBinding()
                    .WithBoundVariableName(pb.BoundVariableName)
                    .WithBoundPropertyName(ComputeBoundPropertyFullName(pb).ToString())
                    .WithConstraint(pb.Constraint);
                builder.AddingItemToPropertyBindings(b2.Build());
            }
        }
        ctx.Add(node.ParameterName.Value, new CompoundVariableReference(new List<VariableReference> { new VariableReference(node.ParameterName.Value) }));
        return builder.Build();
    }

    public override PropertyBinding ProcessPropertyBinding(PropertyBinding node, BoundVariablesContext ctx)
    {
        ctx.Add(node.BoundVariableName, ComputeBoundPropertyFullName(node));
        return node;
    }

    /// <summary>
    /// Builds a list of element components by walking the tree of property bindings up to the topmost paramter declaration. using the list to form a compound variable reference
    /// </summary>
    /// <param name="node">The AST node of the property binding</param>
    /// <returns>a compound variable reference comprised of all the property names from which the bindings were matched</returns>
    private CompoundVariableReference ComputeBoundPropertyFullName(PropertyBinding node)
    {
        var components = new List<VariableReference>();
        var tmp = node as IAstNode;
        while (tmp is not ParameterDeclarationList)
        {
            components.Add(new VariableReference(GetNameFromEligibleAstNode(tmp)));
            tmp = tmp.ParentNode;
        }
        components.Reverse();
        return new CompoundVariableReference(components);
    }

    private string GetNameFromEligibleAstNode(IAstNode n) => n switch
    {
        PropertyBinding pb => pb.BoundPropertyName,
        VariableReference vr => vr.Name,
        DestructuringParamDecl dpd => dpd.ParameterName.Value,
        _ => string.Empty
    };
}
