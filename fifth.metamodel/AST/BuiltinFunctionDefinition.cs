namespace Fifth.AST
{
    using System.Diagnostics;

    [DebuggerDisplay("{Name}/{ParameterDeclarations.ParameterDeclarations.Count}")]
    public partial class BuiltinFunctionDefinition
    {
        public Block Body { get; set; }
    }
}
