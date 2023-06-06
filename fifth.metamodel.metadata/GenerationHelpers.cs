namespace fifth.metamodel.metadata;

using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class PropStatus
{
    public bool IsInherited { get; set; } = false;
    public PropertySpec Prop { get; set; }
}

public static class GenerationHelpers
{
    public static string DisplayProps(AstNodeSpec ast)
    {
        var sb = new StringBuilder();
        foreach (var p in GetPropertyListInherited(ast))
        {
            sb.AppendFormat("{0} : {1}\n", p.Prop.Name, p.IsInherited);
        }

        return sb.ToString();
    }

    public static AstNodeSpec? GetNodeByName(string name)
    {
        return ASTModel.AstNodeSpecs.FirstOrDefault(spec => spec.Name == name);
    }

    public static List<PropStatus> GetOnlyInheritedProperties(AstNodeSpec ast)
    {
        return GetPropertyListInherited(ast).Where(p => p.IsInherited).ToList();
    }

    public static List<PropStatus> GetPropertyListInherited(AstNodeSpec ast)
    {
        var result = new List<PropStatus>();
        if (ast == null)
        {
            return result;
        }

        var parent = GetSuperclassName(ast);
        if (IsDerivedFromNonBaseAstType(ast))
        {
            var inheritedProps = GetPropertyListInherited(GetNodeByName(parent)!)
                                 .Select(p => new PropStatus { Prop = p.Prop, IsInherited = true })
                                 .ToList();
            result.AddRange(inheritedProps);
        }

        result.AddRange(ast.Props.Select(p => new PropStatus { Prop = p, IsInherited = false }));
        return result;
    }

    public static string GetSuperclassName(AstNodeSpec ast)
    {
        return ast.Parent.Split(',', ' ').ElementAt(0);
    }

    public static bool IsDerivedFromNonBaseAstType(AstNodeSpec ast)
    {
        if (string.IsNullOrEmpty(ast.Parent))
        {
            return false;
        }

        var parent = GetSuperclassName(ast);
        return parent != "AstNode" && parent != "TypeAstNode" && parent != "ScopedAstNode";
    }

    public static string PluralType(PropertySpec astMetadata)
    {
        if (astMetadata?.IsCollection ?? false)
        {
            return $"List<{astMetadata.InterfaceName ?? astMetadata!.Type}>";
        }

        return astMetadata!.Type;
    }

    public static string BuildTypeName(this Type type)
    {
        //Console.WriteLine($"Building type name for {type}");
        var sb = new StringBuilder();
        if (type.IsGenericType)
        {
            if (type.GetGenericTypeDefinition() == typeof(List<>))
            {
                sb.Append("List<");
                var sep = "";
                foreach (var typeArgument in type.GenericTypeArguments)
                {
                    sb.Append(sep);
                    sb.Append(BuildTypeName(typeArgument));
                    sep = ", ";
                }

                sb.Append(">");
            }

            if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                sb.Append(BuildTypeName(type.GenericTypeArguments.First()));
                sb.Append("?");
            }

            return sb.ToString();
        }

        return type.FullName;
    }
    public static string BuildInstanceTypeName(this Type type)
    {
        if ( !type.IsCollectionType())
        {
            return BuildTypeName(type);
        }

        if (type.IsGenericType)
        {
            return BuildTypeName(type.GetGenericArguments().First());
        }

        return type.FullName;
    }

    public static bool IsCollectionType(this Type t)
    {
        if (t.IsGenericType)
        {
            return t.GetGenericTypeDefinition() == typeof(List<>);
        }

        return false;
    }

    public static string PluralTypeInit(PropertySpec astMetadata)
    {
        if (astMetadata?.IsCollection ?? false)
        {
            return $"new List<{astMetadata.InterfaceName ?? astMetadata!.Type}>()";
        }

        return $"new {astMetadata!.Type}()";
    }
}
