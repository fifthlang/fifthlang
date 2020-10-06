using System.Collections.Generic;

namespace fifth.Parser.AST
{
    public class ProgramDefinition : AstNode
    {
        public List<FunctionDefinition> FunctionDefinitions { get; set; }
    }
}