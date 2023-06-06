namespace fifth.metamodel.metadata;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;

[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
public class IgnoreAttribute : Attribute
{
}

public static class ILMetamodelProvider
{
    public static IEnumerable<Type> AllILTypes
    {
        get
        {
            return from t in typeof(fifth.metamodel.metadata.il.Version).Assembly.ExportedTypes
                   where  t.Namespace == "fifth.metamodel.metadata.il"
                   orderby t.Name
                   select t;
        }
    }

    public static IEnumerable<Type> NonIgnoredTypes
    {
        get
        {
            return from t in AllILTypes
                   where !t.GetCustomAttributes<IgnoreAttribute>().Any()
                   //where t.GetCustomAttribute<IgnoreAttribute>() != null
                   select t;
        }
    }


    public static IEnumerable<Type> BuildableTypes
    {
        get
        {
            return from t in NonIgnoredTypes
                   where  t.IsClass && !t.IsAbstract && !t.IsGenericType //&& !t.IsGenericTypeParameter
                   select t;
        }
    }
    public static IEnumerable<PropertyInfo> BuildableProperties(this Type t)
    {
        _ = t ?? throw new ArgumentNullException(nameof(t));
        //return t.GetFields();
        return (from pi in t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy  )
        where !(pi?.GetCustomAttributes<IgnoreAttribute>().Any() ?? true)
        select pi).ToList();
    }
}
