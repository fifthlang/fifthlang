namespace Fifth.Parser.LangProcessingPhases;

using System.Collections.Generic;
using System.Linq;
using AST.Visitors;
using Fifth.AST;
using Fifth.AST.Builders;
using Fifth.Symbols;
using Fifth.TypeSystem;

/// <summary>
///   DestructuringPatternFlattenerVisitor is responsible for desugaring the recursive structure
///   representing a destructuring pattern definition
/// </summary>
/// <remarks>
///   See the discussion of destructuring semantics and the resolution of names in the semantics doc
///   at doc\Semantics\other\parameter_declaration.md
/// </remarks>
public class DestructuringPatternFlattenerVisitor : DefaultMutatorVisitor<DummyContext>
{
    public static (DestructuringPatternFlattenerVisitor, DummyContext) CreateVisitor()
    {
        var ctx = new DummyContext();
        var visitor = new DestructuringPatternFlattenerVisitor();
        return (visitor, ctx);
    }

    public override FunctionDefinition ProcessFunctionDefinition(FunctionDefinition node, DummyContext ctx)
    {
        var fb = FunctionDefinitionBuilder.CreateFunctionDefinition()
            .WithName(node.Name)
            .WithTypename(node.Typename)
            .WithReturnType(node.ReturnType);

        // gather the destructuring paramdecls, before they are erased
        var statementGatherer = new PropertyBindingToVariableDeclarationTransformer();
        // 1. enumerate all param decls 1.1. gather all bindings and statements to add
        node.Accept(statementGatherer);
        fb.WithParameterDeclarations((ParameterDeclarationList)Process(node.ParameterDeclarations, ctx));

        var bb = BlockBuilder.CreateBlock();
        var sl = statementGatherer.Statements.Build();
        foreach (var s in sl.Statements)
        {
            bb.AddingItemToStatements(s);
        }
        foreach (var s in node.Body.Statements)
        {
            bb.AddingItemToStatements(s);
        }
        // 3. Generate body with additional statements
        fb.WithBody(bb.Build());
        return fb.Build();
    }

    public override ParameterDeclaration ProcessParameterDeclaration(ParameterDeclaration node, DummyContext ctx)
    {
        if (node.DestructuringDecl == null)
        {
            return node;
        }

        var pdb = ParameterDeclarationBuilder.CreateParameterDeclaration()
            .WithParameterName(node.ParameterName)
            .WithTypeName(node.TypeName);
        // TODO Process the bindings and add statements
        return pdb.Build();
    }
}
public class DummyContext
{
}

public class PropertyBindingToVariableDeclarationTransformer : BaseAstVisitor
{
    public Stack<(string, ISymbolTableEntry)> ResolutionScope { get; } = new();
    public StatementListBuilder Statements { get; } = StatementListBuilder.CreateStatementList();

    /// <summary>
    ///   Takes a binding, and attempts to resolve the components of the binding. Adds a variable
    ///   declaration statement to the list to prepend to the body of the enclosing function.
    /// </summary>
    /// <param name="ctx">
    ///   the binding from which the information comes.
    /// </param>
    public override void EnterDestructuringBinding(DestructuringBinding ctx)
    {
        var b = VariableDeclarationStatementBuilder.CreateVariableDeclarationStatement()
            .WithName(ctx.Varname);
        // TODO Use symtab entry Kind to guide processing here.  it could be a class a paramdecl or something else here.

        var (scopeVarName, propertyDefinitionScope) = ResolutionScope.Peek();
        if (propertyDefinitionScope.SymbolKind == SymbolKind.PropertyDefinition)
        {
            var pd = ResolutionScope.Peek().Item2.Context as PropertyDefinition;
            b.WithUnresolvedTypeName(pd.TypeName);
            b.WithExpression(CompoundVariableReferenceBuilder.CreateCompoundVariableReference()
                .AddingItemToComponentReferences(
                    VariableReferenceBuilder.CreateVariableReference().WithName(ResolutionScope.Peek().Item1).Build())
                .AddingItemToComponentReferences(
                    VariableReferenceBuilder.CreateVariableReference().WithName(pd.Name).Build())
                .Build());
        }

        if (propertyDefinitionScope.SymbolKind == SymbolKind.ClassDeclaration)
        {
            // if we get here, the paramdecl must be a top level one, or the propdecl clashes with a type name??
            // TODO:  Need to check how this can fail.
            var c = propertyDefinitionScope.Context as ClassDefinition;
            // the propname of the ctx needs to be resolved as a propertydefinition of the class c
            var propdecl = c.Properties.FirstOrDefault(pd => pd.Name == ctx.Propname);
            if (propdecl != null)
            {
                ctx["propdecl"] = propdecl;
                // if propdecl is not null, it means we know that the RHS of the assignment is var ref to <scopeVarName>.(propdecl.Name)
                b.WithExpression(VariableReferenceBuilder.CreateVariableReference().WithName($"{scopeVarName}.{propdecl.Name}").Build());
                b.WithUnresolvedTypeName(propdecl.TypeName);
            }
        }

        Statements.AddingItemToStatements(b.Build());
    }

    public override void EnterParameterDeclaration(ParameterDeclaration ctx)
    {
        //var paramType = ctx.NearestScope().Resolve(ctx.TypeName);
        //ResolutionScope.Push((ctx.ParameterName.Value, paramType));
    }

    public override void EnterDestructuringDeclaration(DestructuringDeclaration ctx)
    {
        // if we get here, it was because we entered the destrdecl of a paramdecl or a destrbinding
        // if it's a paramdecl, then add it's type directly as the resolution scope
        if (ctx.ParentNode is ParameterDeclaration pd)
        {
            var paramType = ctx.NearestScope().Resolve(pd.TypeName);
            ResolutionScope.Push((pd.ParameterName.Value, paramType));
        }
        else if (ctx.ParentNode is DestructuringBinding db)
        {
            var propdecl = (PropertyDefinition)db["propdecl"];
            var paramType = propdecl.NearestScope().Resolve(propdecl.TypeName);
            ResolutionScope.Push((propdecl.Name, paramType));

        }
        // if it's a destr binding, then get the prop decl that it relates to from the annotations of the destrbinding.
        base.EnterDestructuringDeclaration(ctx);
    }

    public override void LeaveDestructuringDeclaration(DestructuringDeclaration ctx)
    {
        ResolutionScope.Pop();
    }

    public override void LeaveDestructuringBinding(DestructuringBinding ctx)
    {
        //ResolutionScope.Pop();
    }
}
