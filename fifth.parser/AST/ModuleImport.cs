namespace Fifth.AST
{
    using Fifth.Parser.LangProcessingPhases;

    public class ModuleImport : AstNode
    {
        public string ModuleName { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterModuleImport(this);
            visitor.LeaveModuleImport(this);
        }
    }
}
