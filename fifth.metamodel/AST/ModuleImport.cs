namespace Fifth.AST
{
    using Parser.LangProcessingPhases;

    public class ModuleImport : AstNode
    {
        public ModuleImport(AstNode parentNode) : base(parentNode)
        {
        }

        public string ModuleName { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterModuleImport(this);
            visitor.LeaveModuleImport(this);
        }
    }
}
