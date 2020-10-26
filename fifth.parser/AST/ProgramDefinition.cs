using System.Collections.Generic;

namespace Fifth.AST
{
    public class ProgramDefinition : AstNode
    {
        public List<FunctionDefinition> FunctionDefinitions { get; set; }
    }
}