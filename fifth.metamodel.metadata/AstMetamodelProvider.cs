namespace fifth.metamodel.metadata;

using System.Reflection;

public static class AstMetamodelProvider
{
    public static IEnumerable<Type> AllAstTypes
    {
        get
        {
            return from t in typeof(fifth.metamodel.metadata.il.Version).Assembly.ExportedTypes
                   where  t.Namespace == "fifth.metamodel.metadata.AST"
                   orderby t.Name
                   select t;
        }
    }

    public static IEnumerable<Type> NonIgnoredTypes
    {
        get
        {
            return from t in AllAstTypes
                   where !t.GetCustomAttributes<IgnoreAttribute>().Any()
                   select t;
        }
    }


    public static IEnumerable<Type> ConcreteTypes
    {
        get
        {
            return from t in NonIgnoredTypes
                   where  t.IsClass && !t.IsAbstract // && !t.IsGenericType //&& !t.IsGenericTypeParameter
                   select t;
        }
    }

    public static IEnumerable<Type> TypeParameters(this Type type)
    {
        return type.GenericTypeArguments;
    }


}
