using System.Collections.Generic;

namespace fifth.parser.Parser.AST
{
    public class ProgramDefinition : AstNode
    {
        public List<FunctionDefinition> FunctionDefinitions { get; set; }
    }
}