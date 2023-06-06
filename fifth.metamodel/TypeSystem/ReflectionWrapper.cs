namespace Fifth.TypeSystem;

using System.Linq;
using System.Reflection;

public class TypeReflector
{
    private readonly Type type;

    public TypeReflector(Type type) => this.type = type;

    public string Name => type.Name;
    public string? Namespace => type.Namespace;

    public IEnumerable<AttributeWrapper> Attributes =>
        from a in type.CustomAttributes
        select new AttributeWrapper(a);

    public Type UnderlyingType => type;

    public static implicit operator Type(TypeReflector tr)
    {
        return tr.UnderlyingType;
    }

    public IEnumerable<FieldReflector> Fields =>
        from f in type.GetFields()
        select new FieldReflector(f);

    public IEnumerable<PropertyReflector> Properties =>
        from f in type.GetProperties()
        select new PropertyReflector(f);

    public IEnumerable<MethodReflector> Methods =>
        from m in type.GetMethods()
        select new MethodReflector(m);
}

public class AttributeWrapper
{
    private readonly CustomAttributeData customAttributeData;

    public AttributeWrapper(CustomAttributeData customAttributeData) => this.customAttributeData = customAttributeData;

    public TypeReflector Type => new(customAttributeData.AttributeType);
}

public class FieldReflector
{
    private readonly FieldInfo fieldInfo;

    public FieldReflector(FieldInfo fieldInfo) => this.fieldInfo = fieldInfo;

    public TypeReflector FieldType => new(fieldInfo.FieldType);
    public string Name => fieldInfo.Name;
}
public class PropertyReflector
{
    private readonly PropertyInfo propertyInfo;

    public PropertyReflector(PropertyInfo propertyInfo) => this.propertyInfo = propertyInfo;

    public TypeReflector PropertyType => new(propertyInfo.PropertyType);
    public string Name => propertyInfo.Name;
}

public class MethodReflector
{
    private readonly MethodInfo methodInfo;

    public MethodReflector(MethodInfo methodInfo) => this.methodInfo = methodInfo;

    public TypeReflector ReturnType => new(methodInfo.ReturnType);
    public string Name => methodInfo.Name;

    public IEnumerable<ParameterReflector> Parameters =>
        from p in methodInfo.GetParameters()
        select new ParameterReflector(p);

    public bool IsStatic => methodInfo.IsStatic;
    public bool IsPublic => methodInfo.IsPublic;
}

public class ParameterReflector
{
    private readonly ParameterInfo parameterInfo;

    public ParameterReflector(ParameterInfo parameterInfo) => this.parameterInfo = parameterInfo;

    public TypeReflector ParamType => new(parameterInfo.ParameterType);
    public string Name => parameterInfo.Name;
}
