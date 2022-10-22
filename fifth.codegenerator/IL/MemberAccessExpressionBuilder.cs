namespace Fifth.CodeGeneration.IL;

public partial class MemberAccessExpressionBuilder
{
    public override string Build()
    {
        throw new NotImplementedException();
    }
}

/*
sample code from the original copde generator

    /// <summary>
    ///     Generate the expression IL for a compound variable reference.
    /// </summary>
    /// <param name="ctx">the AST node for the compound varref</param>
    /// <returns>the same ctx</returns>
    /// <remarks>
    ///     The process used by csc is to progressively access member references by getting the members onto the stack.
    ///     <example>
    ///         Here is an example of progressively accessing the members of an object graph (C#: x = p.Vitals.Height)
    ///         <code>
    /// IL_0001: ldarg.0      // p
    /// IL_0002: callvirt     instance class ConsoleApp1.VitalStatistics ConsoleApp1.Person::get_Vitals()
    /// IL_0007: callvirt     instance float64 ConsoleApp1.VitalStatistics::get_Height()
    /// IL_000c: stloc.0      // x
    /// </code>
    ///     </example>
    /// </remarks>
    public override CompoundVariableReference VisitCompoundVariableReference(CompoundVariableReference ctx)
    {
        // special treatment for the first element, since it must be a directly in-scope variable...
        var head = ctx.ComponentReferences.First();
        if (head.SymTabEntry.SymbolKind == SymbolKind.VariableDeclaration)
        {
            // good. this is a local variable. (need to check it's a variable in THIS scope). so load it simply.
            VisitVariableReference(head);
        }
        else if (head.SymTabEntry.SymbolKind == SymbolKind.FormalParameter)
        {
            // good. this is a local variable. (need to check it's a variable in THIS scope). so load it simply.
            var pd = head.SymTabEntry.Context as ParameterDeclaration;
            w($"ldarg.{pd[Constants.ArgOrdinal]}");
        }

        foreach (var vr in ctx.ComponentReferences.Skip(1))
        {
            if (vr.SymTabEntry.SymbolKind == SymbolKind.PropertyDefinition)
            {
                var udtScope =
                    vr.SymTabEntry.Context.ParentNode as ClassDefinition; // is there any other valid cast here?
                var tn = MapType(vr.TypeId);
                w($" callvirt instance {tn} {udtScope.Name}::get_{vr.Name}()");
            }
        }

        return ctx;
    }

*/
