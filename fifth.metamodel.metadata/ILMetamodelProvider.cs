namespace fifth.metamodel.metadata;

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

[AttributeUsage(AttributeTargets.All)]
public class IgnoreAttribute : Attribute
{
}

public static class ILMetamodelProvider
{
    public static IEnumerable<Type> BuildableTypes
    {
        get
        {
            return from t in typeof(fifth.metamodel.metadata.il.Version).Assembly.ExportedTypes
                   where !t.HasAttribute(typeof(IgnoreAttribute)) && t.Namespace == "fifth.metamodel.metadata.il" &&
                         t.IsClass && !t.IsAbstract
                   select t;
        }
    }

    public static IEnumerable<PropertyInfo> BuildableProperties(this Type t)
    {
        //return t.GetFields();
        return (from pi in t.GetProperties()
        where !pi.GetCustomAttributes<IgnoreAttribute>().Any()
        select pi).ToList();
    }
}
