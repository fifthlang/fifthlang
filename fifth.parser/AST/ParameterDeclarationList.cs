using System.Collections.Generic;

namespace Fifth.AST
{
    public class ParameterDeclarationList : AstNode
    {
        public List<ParameterDeclaration> ParameterDeclarations { get; set; }
    }
}
