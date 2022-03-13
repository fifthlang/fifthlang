namespace fifth.metamodel.metadata;

public class PropertySpec
{
    public PropertySpec(string name, string type, bool isCollection = false, bool ignoreDuringVisit = false, string? interfaceName = null)
    {
        Name = name;
        Type = type;
        IsCollection = isCollection;
        IgnoreDuringVisit = ignoreDuringVisit;
        InterfaceName = interfaceName;
    }

    public bool IgnoreDuringVisit { get; set; }
    public string? InterfaceName { get; set; }
    public bool IsCollection { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
}
