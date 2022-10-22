namespace fifth.metamodel.metadata;

public class AstNodeSpec
{
    public AstNodeSpec()
    {
        Name = "";
        Parent = "";
        CustomCode = "";
        PostCtor = "";
        BypassCtorGeneration = false;
        Props = new PropertySpec[] { };
    }

    public bool BypassCtorGeneration { get; set; }
    public string CustomCode { get; set; }
    public string Name { get; set; }
    public string Parent { get; set; }
    public string PostCtor { get; set; }
    public PropertySpec[] Props { get; set; }

    public string[] Commentary { get; set; } = Array.Empty<string>();
}
