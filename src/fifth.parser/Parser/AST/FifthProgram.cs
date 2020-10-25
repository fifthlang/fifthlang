using System.Collections;
using System.Collections.Generic;

namespace Fifth.AST
{
    public class FifthProgram : AstNode
    {
        public IList<AliasDeclaration> Aliases { get; set; }
        public IList<FunctionDefinition> Functions { get; set; }
    }
}
