namespace Fifth.CodeGeneration;

using System.Collections.Generic;
using fifth.metamodel.metadata;

public abstract class BaseTemplatePage<T> : TemplatePage<T>
{
    public string MapType(TypeId tid)
    {
        if (tid == null)
        {
            return "void";
        }

        if (TypeMappings.HasMapping(tid))
        {
            return TypeMappings.ToDotnetType(tid);
        }

        var tn = tid.Lookup().Name;
        if (tid.Lookup() is UserDefinedType)
        {
            tn = $"class {tn}";
        }

        return tn;
    }
    protected IEnumerable<VariableDeclarationStatement> GetVarDecls(Block b)
    {
        foreach (var s in b.Statements)
        {
            if (s is VariableDeclarationStatement x)
            {
                yield return x;
            }
            else if (s is IfStatement ifs)
            {
                foreach (var substatement in GetVarDecls(ifs.IfBlock))
                {
                    yield return substatement;
                }
                foreach (var substatement in GetVarDecls(ifs.ElseBlock))
                {
                    yield return substatement;
                }
            }
            else if (s is WhileStatement ws)
            {
                foreach (var substatement in GetVarDecls(ws.LoopBlock))
                {
                    yield return substatement;
                }
            }
        }
    }

    protected string RenderTypeReference(TypeReference tr)
    {
        if(tr is null) return String.Empty;
        return !string.IsNullOrWhiteSpace(tr.Namespace) ? $"{tr.Namespace}.{tr.Name}" : tr.Name;
    }
}
