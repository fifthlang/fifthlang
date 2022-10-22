namespace Fifth.CodeGeneration.IL;

using fifth.metamodel.metadata.il;
public partial class ModuleDeclarationBuilder : BaseBuilder<ModuleDeclarationBuilder, ModuleDeclaration>
{
    public override string Build()
    {
        var result = $@".module{{ {Model.FileName} }}";
        return result;
    }
}
