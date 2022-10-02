namespace Fifth.CodeGeneration.IL;

using System.Collections.Generic;

public class ProgramDefinition
{
    public List<ClassDefinition> Classes { get; set; }
    public List<MethodDefinition> Functions { get; set; }
    public string TargetAsmFileName { get; set; }
}
public class ProgramBuilder : BaseBuilder<ProgramBuilder, ProgramDefinition>
{
    private ProgramDefinition progdef;
    public ProgramBuilder()
    {
        progdef = new ProgramDefinition();
    }
    public override string Build()
    {
        throw new NotImplementedException();
    }

    public ProgramBuilder WithClass(ClassDefinition cd)
    {
        progdef.Classes.Add(cd);
        return this;
    }

    public ProgramBuilder WithAsmFileName(string ctxTargetAssemblyFileName)
    {
        progdef.TargetAsmFileName = ctxTargetAssemblyFileName;
        return this;
    }
}
