namespace Fifth.CodeGeneration.ILGeneration;

public class ModuleDeclaration
{
    public string FileName { get; set; }
}
public class ModuleBuilder : BaseBuilder<ModuleBuilder, ModuleDeclaration>
{
    public ModuleBuilder()
    {
        Model = new();
    }

    public ModuleBuilder WithFileName(string filename)
    {
        Model.FileName = filename;
        return this;
    }
    public override string Build()
    {
        var result = $@".module{{ {Model.FileName} }}";
        return result;
    }
}
