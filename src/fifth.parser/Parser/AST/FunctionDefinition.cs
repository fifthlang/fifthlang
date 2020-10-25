using Fifth.VirtualMachine;

namespace Fifth.AST
{
    public class FunctionDefinition : AstNode
    {
        public ExpressionList Body { get; set; }
        public ParameterDeclarationList ParameterDeclarations { get; set; }
        public string Name { get; set; }
        public IFifthType ReturnType { get; set; }
    }
}
