using Fifth.VirtualMachine;

namespace Fifth.AST
{
    public class ParameterDeclaration :AstNode
    {
        public string ParameterName { get; set; }
        public IFifthType ParameterType { get; set; }
    }
}
