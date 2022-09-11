namespace fifth.metamodel.metadata;

public class PropertySpec
{
    public PropertySpec(string name, string type, bool isCollection = false, bool ignoreDuringVisit = false, string? interfaceName = null, bool? isNullable = false)
    {
        Name = name;
        Type = type;
        IsCollection = isCollection;
        IgnoreDuringVisit = ignoreDuringVisit;
        InterfaceName = interfaceName;
        IsNullable = IsNullable;
    }

    public bool IgnoreDuringVisit { get; set; }
    public string? InterfaceName { get; set; }
    public bool IsCollection { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public bool IsNullable { get; set; }
}
