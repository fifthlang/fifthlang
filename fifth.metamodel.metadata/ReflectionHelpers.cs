namespace fifth.metamodel.metadata;

using System.Linq;
using System.Reflection;

public static class ReflectionHelpers
{
    public static bool HasAttribute(this Type type, Type attrType)
    {
        return type.GetCustomAttributes().Any(t => t.GetType().IsAssignableTo(attrType));
    }
}
